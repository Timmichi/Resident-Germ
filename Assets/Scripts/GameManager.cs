using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player player;
    public ParticleSystem explosion;
    public float respawnTime = 3.0f;
    public int lives = 10;
    public int score = 0;

    private void Awake() {
        int numGameManagers = FindObjectsOfType<GameManager>().Length;
        Debug.Log(numGameManagers);
        if (numGameManagers > 1) {
            Destroy(this.gameObject);
        }
        else {
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void PlayerDied() {
        this.explosion.transform.position = this.player.transform.position; // has same position as player
        this.explosion.Play();
        this.lives--;
        if (this.lives <= 0) {
            GameOver();
        } else {
             Invoke(nameof(Respawn), this.respawnTime);
        }
    }
    public void CambridgeDestroyed(Cambridge cambridge) {
        this.explosion.transform.position = cambridge.transform.position; // has same position as cambridge
        this.explosion.Play();

        if ((cambridge.size * 0.5f) >= cambridge.minSize) {
            score += 100;
        }
        else {
            score += 50;
        }
        Debug.Log(score);
    }
    public void InsurrectionistAttacked(Insurrectionist insurrectionist) {
        // TO DO: Trigger insurrectionist sound effect first
        this.player.hitInsurrectionist();
    }
    private void Respawn() {
        this.player.transform.position = Vector3.zero; // reset player position to 0
        // this.player.gameObject.layer = LayerMask.NameToLayer("Ignore Collisions"); // when we respawn the layer will have been changed (we do this in the onEnable)
        this.player.gameObject.SetActive(true); // start/enable player game object again 
    }
    private void GameOver() {
        Debug.Log("Game Over");
    }
}
