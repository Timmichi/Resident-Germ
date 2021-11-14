using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Insurrectionist : MonoBehaviour
{
    public float size = 1.0f; // this will get randomized when we spawn them later
    [SerializeField] float speed = 100.0f;
    [SerializeField] float maxLifetime = 20.0f;
    public Rigidbody2D myRigidBody;
    int bullet;

    void Awake() {
        myRigidBody = GetComponent<Rigidbody2D>();
        bullet = LayerMask.NameToLayer("Bullet");
    }
    // Start is called before the first frame update
    void Start()
    {
        this.transform.eulerAngles = new Vector3(0.0f, 0.0f, Random.value * 360.0f);
        this.transform.localScale = Vector3.one * size;
        myRigidBody.mass = size;
    }
    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == bullet) {
            FindObjectOfType<GameManager>().insurrectionistAttacked();
        }
    }
    public void setTrajectory(Vector2 direction){
        myRigidBody.AddForce(direction * speed);
        Destroy(this.gameObject, maxLifetime);
    }
}
