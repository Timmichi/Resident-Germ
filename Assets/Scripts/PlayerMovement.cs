using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float turnSpeed = 1.0f;
    [SerializeField] float thrustSpeed = 1.0f;
    [SerializeField] float respawnInvulnerabilityTime = 3.0f;

    [SerializeField] float bulletSpawnDistance = 1.0f;
    GameManager gameManager;
    Rigidbody2D myRigidBody;
    public Bullet bulletPrefab;
    CapsuleCollider2D myCollider;
    Vector2 moveInput;
    void Awake()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<CapsuleCollider2D>();
        this.gameObject.layer = LayerMask.NameToLayer("Ignore Collisions");
    }

    void Start() {
        Invoke(nameof(turnOnCollisions), respawnInvulnerabilityTime);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        rotatePlayer();
        thrustPlayer();
        die();
    }

    private void OnFire() {
        Vector3 bulletPosition = this.transform.position + this.transform.up *bulletSpawnDistance; 
        Bullet bullet = Instantiate(this.bulletPrefab, bulletPosition, this.transform.rotation); //bullet will have same position/rotation as player
        bullet.Project(this.transform.up);
    }

    void turnOnCollisions() {
        this.gameObject.layer = LayerMask.NameToLayer("Player");
    }

    void OnMove(InputValue value) {
        moveInput = value.Get<Vector2>();
    }

    void thrustPlayer() {
        if (moveInput.y == 1) {
            myRigidBody.AddForce(this.transform.up * thrustSpeed);
        }
    }
    void rotatePlayer() {
        if (moveInput.x != 0) {
            myRigidBody.AddTorque(-1*moveInput.x * turnSpeed);
        }
    }

    void die() {
        if (myCollider.IsTouchingLayers(LayerMask.GetMask("Enemy"))) {
            myRigidBody.velocity = Vector3.zero; // stops movement
            myRigidBody.angularVelocity = 0.0f; // stops rotation
            Destroy(this.gameObject);
            FindObjectOfType<GameManager>().processPlayerDeath();    
            // Set particle effect and respawn
        }
    }
}
