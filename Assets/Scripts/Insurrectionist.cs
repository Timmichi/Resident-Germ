using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Order of execution: https://gamedev.stackexchange.com/questions/135895/what-is-the-order-of-execution-when-calling-an-instantiation-method-in-unity/135905
public class Insurrectionist : MonoBehaviour
{
    public Sprite[] sprites;
    public float size = 1.0f; // this will get randomized when we spawn them later
    public float minSize = 0.5f;
    public float maxSize = 1.5f;
    public float speed = 25.0f;
    public float maxLifetime = 50.0f;

    // private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody;
    
    private void Awake() { // constructor
        // _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    private void Start() { // gets called before the first Update()/FixedUpdate()
        // _spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];// randomize which Insurrectionist sprite we choose
        this.transform.eulerAngles = new Vector3(0.0f, 0.0f, Random.value * 360.0f); // randomize rotation (orientation) of the Insurrectionist sprite to make it look more unique (Random.value gives us a random value between 0 and 1) 
        this.transform.localScale = Vector3.one * this.size; // another way of writing new Vector3(this.size, this.size, this.size)
        _rigidbody.mass = this.size; // set mass (larger size == larger mass)
    }
    public void SetTrajectory(Vector2 direction){
        _rigidbody.AddForce(direction * this.speed);
        Destroy(this.gameObject, this.maxLifetime);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Bullet") { 
            FindObjectOfType<GameManager>().InsurrectionistAttacked(this);
        }
    }
}
