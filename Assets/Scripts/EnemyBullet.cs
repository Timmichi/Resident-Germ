using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 500.0f;
    public float maxLifetime = 10.0f;
    Rigidbody2D myRigidBody;
    PlayerMovement playerMovement;
    void Awake() {
        myRigidBody = GetComponent<Rigidbody2D>();
    }
    public void Project(Vector2 direction) {
        myRigidBody.AddForce(direction * this.speed);
        Destroy(this.gameObject, this.maxLifetime);
    }
    private void OnTriggerEnter2D(Collider2D other) { // will get called whenever objects collide with something else
        Destroy(this.gameObject);
        if (other.tag == "Player") {
           playerMovement = FindObjectOfType<PlayerMovement>();
           playerMovement.changeHitByEnemyBullet();
           playerMovement.die();
        }
    }
}
