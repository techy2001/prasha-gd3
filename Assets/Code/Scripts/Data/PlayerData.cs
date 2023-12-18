using UnityEngine;

namespace Code.Scripts.Data {
	[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData")]
	public class PlayerData : ScriptableObject {
		[SerializeField] [Range(0f, 100f)] public float maxSpeedGround = 16f;
		[SerializeField] [Range(0f, 100f)] public float maxSpeedAir = 4f;
		[SerializeField] [Range(0f, 1f)] public float velocityPreservationGround = 0.1f;
		[SerializeField] [Range(0f, 1f)] public float velocityPreservationAir = 0.98f;
		[SerializeField] [Range(0f, 1280f)] public float accelerationGround = 480f;
		[SerializeField] [Range(0f, 1280f)] public float accelerationAir = 20f;
		[SerializeField] [Range(0f, 10f)] public float weight = 0.12f;
		[SerializeField] [Range(0f, 64f)] public float jumpPower = 20f;
	}
}