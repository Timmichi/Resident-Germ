using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Article : MonoBehaviour
{
    public float size = 1.0f; // this will get randomized when we spawn them later
    [SerializeField] float speed = 75.0f;
    [SerializeField] float maxLifetime = 20.0f;
    [SerializeField] int lives = 5;
    [SerializeField] float bulletSpawnDistance = 1.0f;
    [SerializeField] float firingRate = 0.2f;
    public Rigidbody2D myRigidBody;
    public EnemyBullet enemyBulletPrefab;
    public Bullet bulletPrefab;
    Coroutine firingCoroutine;
    int bullet;
    Vector2 direction;

    void Awake() {
        myRigidBody = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        this.transform.localScale = Vector3.one * size;
        myRigidBody.mass = size;
        firingCoroutine = StartCoroutine(fireContinuously());
    }
    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Bullet") {
            if (lives > 1) {
                lives -=1;
            }
            else {
                lives -= 1;
                FindObjectOfType<GameManager>().articleDestroyed();
                Destroy(this.gameObject);
            }
        }
    }
    IEnumerator fireContinuously() {
        while (true) {
            Vector3 bulletPosition = this.transform.position + this.transform.up*bulletSpawnDistance; 
            EnemyBullet enemyBullet = Instantiate(this.enemyBulletPrefab, bulletPosition, this.transform.rotation); //bullet will have same position/rotation as player
            enemyBullet.Project(this.transform.up);
            yield return new WaitForSeconds(firingRate);
        }
    }


    public void setTrajectory(Vector2 direction){

        myRigidBody.AddForce(direction * speed);
        Destroy(this.gameObject, maxLifetime);
    }
}
