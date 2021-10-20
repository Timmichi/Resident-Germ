using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float speed = 500.0f;
    public float maxLifetime = 10.0f;
    private Rigidbody2D _rigidBody;
    private void Awake() {
        _rigidBody = GetComponent<Rigidbody2D>();
    }
    public void Project(Vector2 direction) {
        _rigidBody.AddForce(direction * this.speed);
        Destroy(this.gameObject, this.maxLifetime);
    }
    private void OnCollisionEnter2D(Collision2D collision) { // will get called whenever objects collide with something else
        Destroy(this.gameObject);
    }
}
