using System;
using UnityEngine;

namespace Code.Scripts.Managers {
	public class GameController : MonoBehaviour {
		public static GameController Instance;
		// Prefabs
		[SerializeField] private GameObject playerPrefab;
		[SerializeField] public PlayerController instance { get; private set; }
		[SerializeField] public Transform playerSpawnPosition { get; private set; }
		
		private void Awake() {
			Instance = this;
			this.instance.transform.position = this.playerSpawnPosition.position;
			this.instance = Instantiate(this.playerPrefab).GetComponent<PlayerController>();
		}
	}
}