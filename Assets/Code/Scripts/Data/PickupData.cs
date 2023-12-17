using UnityEngine;

namespace Code.Scripts.Data {
	[CreateAssetMenu(fileName = "PickupData", menuName = "ScriptableObjects/PickupData")]
	public class PickupData : ScriptableObject {
		public string pickupType;
		public AudioClip pickupSound;
	}
}