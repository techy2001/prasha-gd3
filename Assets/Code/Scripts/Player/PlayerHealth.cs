using System.Collections;
using System.Collections.Generic;
using Code.Scripts.Player;
using Code.Scripts.Util;
using Code.Scripts.Managers;
using UnityEngine;

namespace Code.Scripts.Player {
	[RequireComponent(typeof(PlayerController))]
	public class PlayerHealth : MonoBehaviour {
		private GameController gameController;
		private PlayerController playerController;
		[SerializeField] private float maxHealth;
		[SerializeField] private float currentHealth;
		public HealthBar healthBar;
		public Animator anim;

		private void Awake() {
			this.playerController = this.GetComponent<PlayerController>();
			this.gameController = FindObjectOfType<GameController>();
			this.currentHealth = this.maxHealth;
			if (this.healthBar != null) this.healthBar.SetSliderMax(this.maxHealth);
		}

		public void TakeDamage(float damage) {
			this.currentHealth = MathHelper.clamp(this.currentHealth - damage, 0, this.maxHealth);
			if (this.healthBar != null) this.healthBar.SetSlider(this.currentHealth);
			if (this.currentHealth == 0) {
				//they die
				//play death animation
				//anim.SetBool("isDead", true);
				//game over screen
			}
			AudioHelper.PlayNullableClip(playerController.soundData.damaged(), this.transform.position);
		}

		public void Heal(float amount) {
			this.currentHealth = MathHelper.clamp(this.currentHealth + amount, 0, this.maxHealth);
			if (this.healthBar != null) this.healthBar.SetSlider(this.currentHealth);
		}
	}
}