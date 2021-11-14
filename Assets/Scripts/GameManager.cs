using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerMovement playerPrefab;
    public int playerLives = 3;
    public int score = 0;
    // Start is called before the first frame update
    void Start()
    {
        int numGameManagers = FindObjectsOfType<GameManager>().Length;
        Debug.Log(numGameManagers);
        if (numGameManagers > 1) {
            Destroy(this.gameObject);
        }
        else {
            DontDestroyOnLoad(this.gameObject);
        }
    }
    public void cambridgeDestroyed(Cambridge cambridge) {
        // this.explosion.transform.position = cambridge.transform.position; // has same position as cambridge
        // this.explosion.Play();
        if ((cambridge.size * 0.5f) >= cambridge.minSize) {
            score += 100;
        }
        else {
            score += 50;
        }
        Debug.Log(score);
    }
    public void processPlayerDeath() {
        if (playerLives > 1) {
                playerLives--;
                Debug.Log(playerLives);
                PlayerMovement player = Instantiate(this.playerPrefab);
        }
        else {
            playerLives--;
            endGame();
        }
    }
    public void endGame() {
        Debug.Log("Game Over!");
    }
}
