using System.Collections;
using System.Collections.Generic;
using Code.Scripts.Player;
using UnityEngine;

public class BugEnemy : MonoBehaviour {
    public float damage;
    
    void Start() {
        
    }
    
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            other.GetComponent<PlayerHealth>().TakeDamage(this.damage);
        }
    }
}