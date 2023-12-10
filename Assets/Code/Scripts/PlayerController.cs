using Cinemachine;
using UnityEngine;

namespace Code.Scripts {
	[RequireComponent(typeof(CharacterController))]
	public class PlayerController : MonoBehaviour {
		public static PlayerController Instance { get; private set; }
		[SerializeField] [Range(0f, 100f)] private float maxSpeedGround = 16f;
		[SerializeField] [Range(0f, 100f)] private float maxSpeedAir = 4f;
		[SerializeField] [Range(0f, 1f)] private float velocityPreservationGround = 0.1f;
		[SerializeField] [Range(0f, 1f)] private float velocityPreservationAir = 0.98f;
		[SerializeField] [Range(0f, 1280f)] private float accelerationGround = 480f;
		[SerializeField] [Range(0f, 1280f)] private float accelerationAir = 20f;
		[SerializeField] [Range(0f, 10f)] private float weight = 0.12f;
		[SerializeField] [Range(0f, 64f)] private float jumpPower = 20f;
		[SerializeField] private CinemachineVirtualCamera virtualCamera;
		private CharacterController characterController;
		private Vector3 inputDirection;
		private Vector3 velocity;
		private bool jumpHeld;
		private bool wasJumpHeld;

		private void Awake() {
			this.characterController = this.GetComponent<CharacterController>();
			Instance = this;
		}

		private void Update() {
			var cameraRotation = this.virtualCamera.transform.rotation.eulerAngles;
			var inputRotation = Quaternion.Euler(0, cameraRotation.y, 0);
			this.inputDirection = inputRotation * Vector3.ClampMagnitude(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")), 1);
			this.jumpHeld = Input.GetButton("Jump");
		}

		private void FixedUpdate() {
			var grounded = this.characterController.isGrounded;
			
			this.velocity.x *= grounded ? this.velocityPreservationGround : this.velocityPreservationAir;
			if (!grounded) {
				this.velocity.y += this.weight * Physics.gravity.y;
			}
			this.velocity.z *= grounded ? this.velocityPreservationGround : this.velocityPreservationAir;

			var desiredVelocity = this.inputDirection * (grounded ? this.maxSpeedGround : this.maxSpeedAir);

			this.velocity.x = Mathf.MoveTowards(this.velocity.x, grounded ? desiredVelocity.x : this.velocity.x + desiredVelocity.x, (grounded ? this.accelerationGround : this.accelerationAir) * Time.deltaTime);
			if (this.jumpHeld && !this.wasJumpHeld && grounded) {
				this.velocity.y = this.jumpPower;
			}
			this.velocity.z = Mathf.MoveTowards(this.velocity.z, grounded ? desiredVelocity.z : this.velocity.z + desiredVelocity.z, (grounded ? this.accelerationGround : this.accelerationAir) * Time.deltaTime);

			this.characterController.Move(this.velocity * Time.deltaTime);
			if (Physics.Raycast(new Ray(this.transform.position, Vector3.down), out var groundPoint, 1.2f) && this.velocity.y < 0) {
				this.characterController.Move(groundPoint.point - this.transform.position + Vector3.up * 1.0f);
			}
			
			var flatVelocity = this.velocity;
			flatVelocity.y = 0;
			if (flatVelocity.magnitude > 0.1f) {
				var toRotation = Quaternion.LookRotation(flatVelocity, Vector3.up);
				this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, toRotation, 360);
			}

			this.wasJumpHeld = this.jumpHeld;
		}
	}
}