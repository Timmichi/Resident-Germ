using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 500.0f;
    public float maxLifetime = 10.0f;
    Rigidbody2D myRigidBody;
    void Awake() {
        myRigidBody = GetComponent<Rigidbody2D>();
    }
    public void Project(Vector2 direction) {
        myRigidBody.AddForce(direction * this.speed);
        Destroy(this.gameObject, this.maxLifetime);
    }
    private void OnTriggerEnter2D(Collider2D other) { // will get called whenever objects collide with something else
        Destroy(this.gameObject);
    }
}
