using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float  maxHealth;
    private float currentHealth;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        //if()
        //{
        //    TakeDamage(10f);
        //}
    }

    void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            //they die
            //play death animation
            anim.SetBool("isDead", true);
            //game over screen

        }
    }
}
