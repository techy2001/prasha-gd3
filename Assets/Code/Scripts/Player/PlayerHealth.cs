using System.Collections;
using System.Collections.Generic;
using Code.Scripts.Player;
using Code.Scripts.Util;
using UnityEngine;

namespace Code.Scripts.Player {
	public class PlayerHealth : MonoBehaviour {
		[SerializeField] private float maxHealth;
		[SerializeField] private float currentHealth;
		public HealthBar healthBar;
		public Animator anim;

		private void Start() {
			currentHealth = maxHealth;
			if (this.healthBar != null) healthBar.SetSliderMax(maxHealth);
		}

		public void TakeDamage(float damage) {
			this.currentHealth = MathHelper.clamp(this.currentHealth - damage, 0, this.maxHealth);
			if (this.healthBar != null) healthBar.SetSlider(currentHealth);
			if (currentHealth == 0) {
				//they die
				//play death animation
				//anim.SetBool("isDead", true);
				//game over screen
			}
		}

		public void Heal(float amount) {
			this.currentHealth = MathHelper.clamp(this.currentHealth + amount, 0, this.maxHealth);
			if (this.healthBar != null) healthBar.SetSlider(currentHealth);
		}
	}
}