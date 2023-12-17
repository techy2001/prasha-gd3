using Code.Scripts.Managers;
using UnityEngine;

namespace Code.Scripts.Entities {
	[RequireComponent(typeof(Collider))]
	public class PickupEntity : MonoBehaviour {
		private GameController gameController;
		[SerializeField] private string pickupType = "Heal";
		[SerializeField] public AudioClip pickupSound;

		private void Awake() {
			this.gameController = FindObjectOfType<GameController>();
		}

		private void Update() {
			this.gameObject.transform.Rotate(Vector3.up, 1);
		}

		private void OnTriggerEnter(Collider other) {
			if (other.gameObject == this.gameController.player.gameObject) {
				this.gameController.pickupGainedEvent.Raise(this.gameController.player, this.pickupType);
				AudioSource.PlayClipAtPoint(this.pickupSound, other.gameObject.transform.position);
				this.gameObject.SetActive(false);
			}
		}
	}
}