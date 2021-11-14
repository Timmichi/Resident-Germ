using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cambridge : MonoBehaviour
{
    public float size = 1.0f; // this will get randomized when we spawn them later
    public float minSize = 0.5f;
    public float maxSize = 1.5f;
    [SerializeField] float speed = 50.0f;
    [SerializeField] float maxLifetime = 30.0f;
    [SerializeField] int lives = 3;
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
    public void setTrajectory(Vector2 direction){
        myRigidBody.AddForce(direction * speed);
        Destroy(this.gameObject, maxLifetime);
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == bullet) {
            if ((size * 0.5f) >= minSize) {
                if (lives > 1) {
                    lives -= 1;
                    Debug.Log(lives);
                }
                else {
                    lives -= 1;
                    createSplit();
                    createSplit();
                    FindObjectOfType<GameManager>().cambridgeDestroyed(this);
                    Destroy(this.gameObject);
                }
            }
            else {
                FindObjectOfType<GameManager>().cambridgeDestroyed(this);
                Destroy(this.gameObject); 
            }
        }
    }
    void createSplit() {
        Vector2 position = this.transform.position;
        position += Random.insideUnitCircle * 0.5f;
        Cambridge half = Instantiate(this, position, this.transform.rotation);
        half.size = size * 0.5f;
        half.setTrajectory(Random.insideUnitCircle.normalized); // picks a random direction with a normalized magnitude
    }
}
