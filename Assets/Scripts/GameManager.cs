using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player player;
    public ParticleSystem explosion;
    public float respawnTime = 3.0f;
    public int lives = 3;
    public int score = 0;
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
    public void AsteroidDestroyed(Asteroid asteroid) {
        this.explosion.transform.position = asteroid.transform.position; // has same position as player
        this.explosion.Play();

        // TODO: Increase Score
    }
    private void Respawn() {
        this.player.transform.position = Vector3.zero; // reset player position to 0
        // this.player.gameObject.layer = LayerMask.NameToLayer("Ignore Collisions"); // when we respawn the layer will have been changed (we do this in the onEnable)
        this.player.gameObject.SetActive(true); // start/enable player game object again 
    }
    private void GameOver() {
        // TODO:
    }
}
