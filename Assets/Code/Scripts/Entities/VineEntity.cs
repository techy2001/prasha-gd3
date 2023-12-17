using UnityEngine;

namespace Code.Scripts.Entities {
	public class VineEntity : MonoBehaviour {
		public void destroyVine() {
			Destroy(this.gameObject);
		}
	}
}