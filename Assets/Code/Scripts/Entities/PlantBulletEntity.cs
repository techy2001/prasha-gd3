using Code.Scripts.Data;
using Code.Scripts.Managers;
using Code.Scripts.Util;
using UnityEngine;

namespace Code.Scripts.Entities {
	[RequireComponent(typeof(Collider))]
	public class PlantBulletEntity : MonoBehaviour {
		private GameController gameController;
		[SerializeField] private PickupData pickupData;
		[SerializeField] private float xVel;
		[SerializeField] private float yVel;
		[SerializeField] private float zVel;
		[SerializeField] private int age;

		private void Awake() {
			this.gameController = FindObjectOfType<GameController>();
		}

		private void Update() {
			this.gameObject.transform.position += new Vector3(this.xVel, this.yVel, this.zVel);
		}

		private void FixedUpdate() {
			this.age++;
			if (this.age > 1600) {
				this.discard();
			}
		}

		private void OnTriggerEnter(Collider other) {
			if (other.gameObject == this.gameController.player.gameObject) {
				this.gameController.pickupGainedEvent.Raise(this.gameController.player, this.pickupData.pickupType);
				AudioHelper.PlayNullableClip(this.pickupData.pickupSound, other.gameObject.transform.position);
				this.discard();
			} else {
				this.discard();
			}
		}

		private void OnCollisionEnter(Collision other) {
			if (other.gameObject != this.gameController.player.gameObject) {
				this.discard();
			}
		}

		public void setVelocity(Vector3 vector3) {
			this.setVelocity(vector3.x, vector3.y, vector3.z);
		}

		public void setVelocity(float x, float y, float z) {
			this.xVel = x;
			this.yVel = y;
			this.zVel = z;
		}

		private void discard() {
			Destroy(this.gameObject);
		}
	}
}