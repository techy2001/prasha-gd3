using Code.Scripts.Managers;
using Code.Scripts.Util;
using UnityEngine;

namespace Code.Scripts.Entities {
	[RequireComponent(typeof(Collider))]
	public class PlantEnemy : MonoBehaviour {
		public static int FIRE_COOLDOWN = 80;
		private GameController gameController;
		[SerializeField] public AudioClip deathSound;
		[SerializeField] private int fireCooldown;
		[SerializeField] private GameObject BulletPrefab;
		[SerializeField] private VineEntity connectedVine;

		private void Awake() {
			this.gameController = FindObjectOfType<GameController>();
		}
		
		private void FixedUpdate() {
			var distance = MathHelper.distanceTo(this.transform.position, this.gameController.player.gameObject.transform.position);
			if (distance is < 20 and > 3) {
				if (this.fireCooldown > 0) {
					this.fireCooldown--;
				} else {
					var gameObject = Instantiate(this.BulletPrefab);
					var shootPos = this.transform.position + Vector3.up;
					gameObject.transform.position = shootPos;
					var bullet = gameObject.GetComponent<PlantBulletEntity>();
					var difference = this.gameController.player.transform.position - shootPos;
					bullet.setVelocity(difference.normalized * 0.1f);
					this.fireCooldown = FIRE_COOLDOWN;
				}
			}
		}

		private void OnTriggerEnter(Collider other) {
			if (other.gameObject == this.gameController.player.gameObject) {
				var player = this.gameController.player;
				if (player.velocity.y < 0) {
					player.velocity.y = -player.velocity.y;
					AudioSource.PlayClipAtPoint(this.deathSound, player.transform.position);
					this.discard();
				} else {
					player.playerHealth.TakeDamage(1);
					this.gameController.player.velocity *= -24;
					this.gameController.player.velocity.y = 10;
				}
			}
		}

		private void discard() {
			if (this.connectedVine != null) this.connectedVine.destroyVine();
			Destroy(this.gameObject);
		}
	}
}