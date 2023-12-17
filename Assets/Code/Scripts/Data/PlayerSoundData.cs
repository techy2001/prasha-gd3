using System.Collections.Generic;
using UnityEngine;

namespace Code.Scripts.Data {
	[CreateAssetMenu(fileName = "PlayerSoundData", menuName = "ScriptableObjects/PlayerSoundData")]
	public class PlayerSoundData : ScriptableObject {
		public AudioClip[] jumpSounds;
		public AudioClip[] walkSounds;
		public AudioClip[] stompSounds;
		public AudioClip[] landingSounds;
		public AudioClip[] damagedSounds;
		
		public AudioClip jump() {
			return randomSound(this.jumpSounds);
		}
		
		public AudioClip walk() {
			return randomSound(this.walkSounds);
		}
		
		public AudioClip stomp() {
			return randomSound(this.stompSounds);
		}
		
		public AudioClip landing() {
			return randomSound(this.landingSounds);
		}
		
		public AudioClip damaged() {
			return randomSound(this.damagedSounds);
		}
		
		private static AudioClip randomSound(IReadOnlyList<AudioClip> sounds) {
			return sounds.Count == 0 ? null : sounds[Random.Range(0, sounds.Count)];
		}
	}
}