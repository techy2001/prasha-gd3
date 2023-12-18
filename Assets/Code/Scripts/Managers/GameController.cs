using System.Collections;
using Cinemachine;
using Code.Scripts.Data;
using Code.Scripts.Events;
using Code.Scripts.Player;
using UnityEngine;

namespace Code.Scripts.Managers {
	public class GameController : MonoBehaviour {
		public PlayerController player;
		public MusicData music;
		
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
			this.StartCoroutine(this.MusicLoop());
		}

		private IEnumerator MusicLoop() {
			var camera = FindObjectOfType<CinemachineBrain>();
			var audioSource = camera.gameObject.AddComponent<AudioSource>();
			audioSource.clip = this.music.introduction;
			audioSource.volume = 1f;
			audioSource.bypassEffects = true;
			audioSource.bypassListenerEffects = true;
			audioSource.bypassReverbZones = true;
			while (true) {
				audioSource.Play();
				yield return new WaitForSeconds(audioSource.clip.length * Mathf.Max(0.01f, Time.timeScale));
				audioSource.clip = this.music.getSegment();
			}
		}
	}
}