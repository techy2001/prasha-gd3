using Code.Scripts.Util;
using UnityEngine;

namespace Code.Scripts.Entities {
	[RequireComponent(typeof(Collider))]
	public class PickupEntity : MonoBehaviour {
		[SerializeField] private PickupType pickupType = PickupType.Health;

		private void OnTriggerEnter(Collider other) {
			if (other.gameObject == PlayerController.Instance.gameObject) {
				
				this.gameObject.SetActive(false);
			}
		}
	}
}