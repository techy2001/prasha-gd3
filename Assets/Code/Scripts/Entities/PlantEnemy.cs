using Code.Scripts.Managers;
using Code.Scripts.Util;
using UnityEngine;

namespace Code.Scripts.Entities {
	[RequireComponent(typeof(Collider))]
	public class PlantEnemy : MonoBehaviour {
		private GameController gameController;
		[SerializeField] private const int FIRE_COOLDOWN = 80;
		[SerializeField] private int fireCooldown;
		[SerializeField] private GameObject BulletPrefab;

		private void Awake() {
			this.gameController = FindObjectOfType<GameController>();
		}
		
		private void FixedUpdate() {
			var distance = MathHelper.distanceTo(this.gameObject.transform.position,
				this.gameController.player.gameObject.transform.position);
			if (distance is < 20 and > 3) {
				if (this.fireCooldown > 0) {
					this.fireCooldown--;
				} else {
					var gameObject = Instantiate(this.BulletPrefab);
					var shootPos = this.gameObject.transform.position + Vector3.up;
					gameObject.transform.position = shootPos;
					var bullet = gameObject.GetComponent<PlantBulletEntity>();
					var difference = this.gameController.player.gameObject.transform.position - shootPos;
					bullet.setVelocity(difference.normalized * 0.1f);
					this.fireCooldown = FIRE_COOLDOWN;
				}
			}
		}

		private void discard() {
			Destroy(this.gameObject);
		}
	}
}