using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public PlayerMovement playerPrefab;
    public int playerLives = 3;
    public int score = 0;
    int oldSceneIndex;
    int currentSceneIndex;
    Coroutine levelCoroutine;
    // public ParticleSystem explosion;
    // Start is called before the first frame update
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoreText;
    public int highScore = 0;
    public LifeCanvas lifeCanvasPrefab;
    int[] buyLifeArr = new int[] {1500, 3000, 9000, 36000, 72000, 144000}; // player can choose to buy a life if passing any of these scores
    int buyLifeIndex = 0;
    public bool lifeCanvasDisplaying = false;
    void Start()
    {
        livesText.text = playerLives.ToString();
        scoreText.text = "$" + score.ToString();
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
        if (buyLifeIndex != buyLifeArr.Length && score > buyLifeArr[buyLifeIndex] && !lifeCanvasDisplaying) {
            LifeCanvas lifeCanvas = Instantiate(lifeCanvasPrefab);
            pauseGame();
            lifeCanvasDisplaying = true;
            buyLifeIndex++;
        }
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (oldSceneIndex != currentSceneIndex) {
            oldSceneIndex = currentSceneIndex;
            Debug.Log(currentSceneIndex);
            levelCoroutine = StartCoroutine(goToNextLevel());
        }
    }

    public void pauseGame() {
        Time.timeScale = 0f;
    }
    public void unpauseGame() {
        Time.timeScale = 1f;
    }
    IEnumerator goToNextLevel() {
        int duration = 0;
        if (currentSceneIndex == 2) {
            while (score < 2000 && duration < 60) {
                duration++;
                Debug.Log(duration);
                yield return new WaitForSeconds(1);
            }
        }
        else if (currentSceneIndex == 4) {
            while (score < 4000 && duration < 60) {
                duration++;
                Debug.Log(duration);
                yield return new WaitForSeconds(1);
            }
        }
        else if (currentSceneIndex == 6) {
            while (true) {
                duration++;
                Debug.Log(duration);
                yield return new WaitForSeconds(1);
            }
        }
        // Only load the next scene if we are currently on a level scene (not including main menu).
        if (currentSceneIndex % 2 == 0 && currentSceneIndex > 0) {
            print("Game Manager - Going to scene " + (currentSceneIndex+1));
            SceneManager.LoadScene(currentSceneIndex+1);
        }
       
    }
    public void cambridgeDestroyed(Cambridge cambridge) {
        // this.explosion.transform.position = cambridge.transform.position; // has same position as cambridge
        // this.explosion.Play();
        if ((cambridge.size * 0.5f) >= cambridge.minSize) {
            addToScore(100);
        }
        else {
            addToScore(50);
        }
        Debug.Log(score);
    }
    public void articleDestroyed() {
        addToScore(1000);
        Debug.Log(score);
    }
    public void insurrectionistAttacked() {
        addToScore(-200);
        Debug.Log(score);
        FindObjectOfType<PlayerMovement>().changeShotWrongEnemy();
    }

    public void addToScore(int pointsToAdd) {
        score += pointsToAdd;
        scoreText.text = "$" + score.ToString();
    }
    public void addToLife(int livesToAdd) {
        playerLives+=livesToAdd;
        livesText.text = playerLives.ToString();
    }
    public void processPlayerDeath() {
        // if lives drops to 0 or score is less than 0 when player loses a life, player loses.
        if (playerLives > 1 && score >= 0) {
                addToLife(-1);
                addToScore(-1000);
                Debug.Log(playerLives);
                PlayerMovement player = Instantiate(this.playerPrefab);
        }
        else {
            playerLives = 0;
            livesText.text = playerLives.ToString();
            endGame();
        }
    }
    public void resetGame() {
        playerLives = 3;
        livesText.text = playerLives.ToString();
        score = 0;
        scoreText.text = "$" + score.ToString();
        buyLifeIndex = 0;
        lifeCanvasDisplaying = false;
    }
    public void endGame() {
        if (score > highScore) {
            highScore = score;
        }
        resetGame();
        SceneManager.LoadScene(0);
    }
}
