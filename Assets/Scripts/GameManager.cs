using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public PlayerMovement playerPrefab;
    public int playerLives = 3;
    public int score = 0;
    int oldSceneIndex;
    int currentSceneIndex;
    Coroutine levelCoroutine;
    bool restartCoroutine = false;
    // Start is called before the first frame update
    void Start()
    {
        int numGameManagers = FindObjectsOfType<GameManager>().Length;
        if (numGameManagers > 1) {
            Destroy(this.gameObject);
        }
        else {
            DontDestroyOnLoad(this.gameObject);
        }
        currentSceneIndex = oldSceneIndex = SceneManager.GetActiveScene().buildIndex;
        levelCoroutine = StartCoroutine(goToNextLevel());
    }
    void Update() {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (oldSceneIndex != currentSceneIndex) {
            oldSceneIndex = currentSceneIndex;
            Debug.Log(currentSceneIndex);
            //levelCoroutine = StartCoroutine(goToNextLevel()); doubling up on next level jumps
        }
    }

    IEnumerator goToNextLevel() {
        int duration = 0;
        if (currentSceneIndex == 2) {
            while (score < 500 && duration < 60) {
                duration++;
                Debug.Log(duration);
                yield return new WaitForSeconds(1);
            }
        }
        else if (currentSceneIndex == 4) {
            while (score < 1000 && duration < 60) {
                duration++;
                Debug.Log(duration);
                yield return new WaitForSeconds(1);
            }
        }
        else if (currentSceneIndex == 6) {
            while (score < 1500 && duration < 60) {
                duration++;
                Debug.Log(duration);
                yield return new WaitForSeconds(1);
            }
        }
        print("Game Manager - Going to scene " + (currentSceneIndex+1));
        SceneManager.LoadScene(currentSceneIndex+1);
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
    public void articleDestroyed() {
        score += 300;
        Debug.Log(score);
    }
    public void insurrectionistAttacked() {
        playerLives--;
        score -= 200;
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
