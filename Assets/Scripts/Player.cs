using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Bullet bulletPrefab;
    public float turnSpeed = 1.0f;
    public float thrustSpeed = 1.0f; // make this public to make it show up in our editor
    public float respawnInvulnerabilityTime = 3.0f;
    private Rigidbody2D _rigidBody;
    private bool _thrusting;
    private float _turnDirection;
    private void Update() { // will update every frame 
        _thrusting = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
            _turnDirection = 1.0f;
        } else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
            _turnDirection = -1.0f;
        } else {
            _turnDirection = 0.0f;
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) {
            Shoot();
        }
    }
    private void Awake() { // an initialization function or establishing references
        _rigidBody = GetComponent<Rigidbody2D>(); // getting a component of type rigidbody2d. GetComponent will find the component we specified within the game object we are in (Player)
    }
    private void OnEnable() { // when enabled again, run these things
        this.gameObject.layer = LayerMask.NameToLayer("Ignore Collisions");
        Invoke(nameof(TurnOnCollisions), this.respawnInvulnerabilityTime);
    }
    private void TurnOnCollisions() {
        this.gameObject.layer = LayerMask.NameToLayer("Player");
    }

    private void FixedUpdate() { // used for physics/movement so that it isn't relient on frames (update())
        if (_thrusting) {
            _rigidBody.AddForce(this.transform.up * this.thrustSpeed); //addforce allows us to move the object (this.transform.up means to thrust in the forward direction of our player)
        }
        if (_turnDirection != 0) {
            _rigidBody.AddTorque(_turnDirection * this.turnSpeed);
        }
    }
    private void Shoot() {
        Bullet bullet = Instantiate(this.bulletPrefab, this.transform.position, this.transform.rotation); //bullet will have same position/rotation as player
        bullet.Project(this.transform.up);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Asteroid") {
            _rigidBody.velocity = Vector3.zero; // stops movement
            _rigidBody.angularVelocity = 0.0f; // stops rotation
            this.gameObject.SetActive(false); // turns off the entire game object until player is turned back on
            FindObjectOfType<GameManager>().PlayerDied(); //gets a global reference and calls it. Costly because it has to look through every game object in your scene, and every component
        }
    }
}
