using Code.Scripts.Events;
using Code.Scripts.Player;
using UnityEngine;

namespace Code.Scripts.Managers {
	public class GameController : MonoBehaviour {
		public PlayerController player;
		public OnPickupGained pickupGainedEvent { get; private set; }
		
		private void Awake() {
			this.pickupGainedEvent = ScriptableObject.CreateInstance<OnPickupGained>();
			this.pickupGainedEvent.RegisterListener(static (controller, type) => {
				switch (type) {
					case "Heal":
						controller.playerHealth.Heal(1);
						return;
					case "PlantBullet":
						controller.playerHealth.TakeDamage(1);
						return;
				}
			});
		}
	}
}