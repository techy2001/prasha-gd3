using UnityEngine;

namespace Code.Scripts.Util {
	public interface AudioHelper {
		public static bool PlayNullableClip(AudioClip clip, Vector3 position) {
			if (clip == null) return false;
			AudioSource.PlayClipAtPoint(clip, position);
			return true;
		}
	}
}