using System.Collections.Generic;
using UnityEngine;

namespace Code.Scripts.Data {
	[CreateAssetMenu(fileName = "MusicData", menuName = "ScriptableObjects/MusicData")]
	public class MusicData : ScriptableObject {
		public AudioClip introduction;
		public AudioClip[] musicSegments;
		
		public AudioClip getSegment() {
			return randomSound(this.musicSegments);
		}
		
		private static AudioClip randomSound(IReadOnlyList<AudioClip> sounds) {
			return sounds.Count == 0 ? null : sounds[Random.Range(0, sounds.Count)];
		}
	}
}