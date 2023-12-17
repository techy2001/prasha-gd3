using Code.Scripts.Data;
using Code.Scripts.Managers;
using Code.Scripts.Util;
using UnityEngine;

namespace Code.Scripts.Entities {
	[RequireComponent(typeof(Collider))]
	public class PickupEntity : MonoBehaviour {
		private GameController gameController;
		public PickupData pickupData;

		private void Awake() {
			this.gameController = FindObjectOfType<GameController>();
		}

		private void Update() {
			this.gameObject.transform.Rotate(Vector3.up, 1);
		}

		private void OnTriggerEnter(Collider other) {
			if (other.gameObject == this.gameController.player.gameObject) {
				this.gameController.pickupGainedEvent.Raise(this.gameController.player, this.pickupData.pickupType);
				AudioHelper.PlayNullableClip(this.pickupData.pickupSound, other.gameObject.transform.position);
				this.gameObject.SetActive(false);
			}
		}
	}
}