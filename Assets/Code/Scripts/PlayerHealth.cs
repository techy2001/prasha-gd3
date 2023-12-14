using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    private float currentHealth;
    public HealthBar healthBar;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetSliderMax(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            TakeDamage(10f);
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.SetSlider(currentHealth);

        if (currentHealth <= 0)
        {
            //they die

            //play death animation
            //anim.SetBool("isDead", true);

            //game over screen
        }


    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        healthBar.SetSlider(currentHealth);
    }

   
}

   

   

