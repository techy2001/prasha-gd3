using Code.Scripts.Events;
using Code.Scripts.Player;
using UnityEngine;

namespace Code.Scripts.Managers {
	public class GameController : MonoBehaviour {
		public PlayerController player;
		public readonly OnPickupGained pickupGainedEvent = new OnPickupGained();
		
		private void Awake() {
			this.pickupGainedEvent.RegisterListener(static (controller, type) => {
				switch (type) {
					case "Heal":
						controller.playerHealth.Heal(1);
						return;
					case "Orb":
						// ORB CODE
						return;
					case "PlantBullet":
						controller.playerHealth.TakeDamage(1);
						return;
				}
			});
		}
	}
}