using System.Collections;
using System.Collections.Generic;
using Code.Scripts.Managers;
using Code.Scripts.Player;
using Code.Scripts.Util;
using UnityEngine;

public class BugEnemy : MonoBehaviour {
    public float damage;
    public float speed;
    private Code.Scripts.Managers.GameController gameController;
    [SerializeField] public AudioClip deathSound;

    private void Awake()
    {
        this.gameController = FindObjectOfType<GameController>();
    }

    void Start() {
        
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(this.transform.position, GameObject.FindWithTag("Player").transform.position, speed);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject == this.gameController.player.gameObject)
        {
            var player = this.gameController.player;
            if (player.velocity.y < 0)
            {
                player.velocity.y = -player.velocity.y;
                AudioHelper.PlayNullableClip(this.deathSound, player.transform.position);
                this.discard();
            }
            else
            {
                player.playerHealth.TakeDamage(damage);
                this.gameController.player.velocity *= -1;
                this.gameController.player.velocity.y = 10;
            }
        }
    }

    private void discard()
    {
        
        Destroy(this.gameObject);
    }
}

