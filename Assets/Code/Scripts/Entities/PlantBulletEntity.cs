using Code.Scripts.Managers;
using UnityEngine;

namespace Code.Scripts.Entities {
	[RequireComponent(typeof(Collider))]
	public class PlantBulletEntity : MonoBehaviour {
		private GameController gameController;
		[SerializeField] private string pickupType = "PlantBullet";
		[SerializeField] public AudioClip pickupSound;
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
			if (this.age > 6000) {
				this.discard();
			}
		}

		private void OnTriggerEnter(Collider other) {
			if (other.gameObject == this.gameController.player.gameObject) {
				this.gameController.pickupGainedEvent.Raise(this.gameController.player, this.pickupType);
				AudioSource.PlayClipAtPoint(this.pickupSound, other.gameObject.transform.position);
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