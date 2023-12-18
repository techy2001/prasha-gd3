using Cinemachine;
using Code.Scripts.Data;
using Code.Scripts.Managers;
using Code.Scripts.Util;
using UnityEngine;

namespace Code.Scripts.Player {
	[RequireComponent(typeof(CharacterController))]
	public class PlayerController : MonoBehaviour {
		private GameController gameController;
		[SerializeField] public PlayerData playerData;
		[SerializeField] public PlayerSoundData soundData;
		[SerializeField] private CinemachineVirtualCamera virtualCamera;
		[SerializeField] private GameObject cameraTarget;
		[SerializeField] private AnimationController AnimationController;
		private CharacterController characterController { get; set; }
		public PlayerHealth playerHealth { get; private set; }
		public Vector3 velocity;
		private Vector3 inputDirection;
		private bool jumpHeld;
		private bool wasJumpHeld;
		private bool isGrounded;
		private int stepSoundCooldown = 6;

		private void Awake() {
			this.characterController = this.GetComponent<CharacterController>();
			this.playerHealth = this.GetComponent<PlayerHealth>();
			this.gameController = FindObjectOfType<GameController>();
			this.gameController.player = this;
		}

		private void Update() {
			var cameraRotation = this.virtualCamera.transform.rotation.eulerAngles;
			var inputRotation = Quaternion.Euler(0, cameraRotation.y, 0);
			this.inputDirection = inputRotation * Vector3.ClampMagnitude(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")), 1);
			this.jumpHeld = Input.GetButton("Jump");
		}

		private void FixedUpdate() {
			if (!this.isGrounded && this.isGrounded != this.characterController.isGrounded) {
				AudioHelper.PlayNullableClip(this.soundData.landing(), this.transform.position);
			}
			
			this.isGrounded = this.characterController.isGrounded;
			
			this.velocity.x *= this.isGrounded ? this.playerData.velocityPreservationGround : this.playerData.velocityPreservationAir;
			if (!this.isGrounded) {
				this.velocity.y += this.playerData.weight * Physics.gravity.y;
			}
			this.velocity.z *= this.isGrounded ? this.playerData.velocityPreservationGround : this.playerData.velocityPreservationAir;

			var desiredVelocity = this.inputDirection * (this.isGrounded ? this.playerData.maxSpeedGround : this.playerData.maxSpeedAir);

			this.velocity.x = Mathf.MoveTowards(this.velocity.x, this.isGrounded ? desiredVelocity.x : this.velocity.x + desiredVelocity.x, (this.isGrounded ? this.playerData.accelerationGround : this.playerData.accelerationAir) * Time.deltaTime);
			if (this.jumpHeld && !this.wasJumpHeld && this.isGrounded) {
				this.velocity.y = this.playerData.jumpPower;
				AudioHelper.PlayNullableClip(this.soundData.jump(), this.transform.position);
			}
			this.velocity.z = Mathf.MoveTowards(this.velocity.z, this.isGrounded ? desiredVelocity.z : this.velocity.z + desiredVelocity.z, (this.isGrounded ? this.playerData.accelerationGround : this.playerData.accelerationAir) * Time.deltaTime);

			this.characterController.Move(this.velocity * Time.deltaTime);
			if (Physics.Raycast(new Ray(this.transform.position, Vector3.down), out var groundPoint, 1.2f) && this.isGrounded) {
				this.characterController.Move(groundPoint.point - this.transform.position + Vector3.up * 1.0f);
				this.velocity.y = 0;
				if (this.velocity.magnitude > 0.1 && this.stepSoundCooldown-- <= 0) {
					AudioHelper.PlayNullableClip(this.soundData.walk(), this.transform.position);
					this.stepSoundCooldown = 6;
				}
			}
			
			var flatVelocity = this.velocity;
			flatVelocity.y = 0;
			if (flatVelocity.magnitude > 0.1f) {
				var toRotation = Quaternion.LookRotation(flatVelocity, Vector3.up);
				this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, toRotation, 360);
			}

			this.wasJumpHeld = this.jumpHeld;
			
			if (this.AnimationController == null) return;
			this.AnimationController.SetVelocity(this.velocity);
			this.AnimationController.SetGrounded(this.isGrounded);
			this.AnimationController.SetJumpHeld(this.jumpHeld);
			this.AnimationController.SetWasJumpHeld(this.wasJumpHeld);
		}
	}
}