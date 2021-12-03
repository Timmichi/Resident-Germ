using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI highScoreText;
    public void Start() {
        GameManager gm = FindObjectOfType<GameManager>();
        if (gm) {
            highScoreText.text = "High Score: $" + gm.highScore.ToString();
        }
        else {
            highScoreText.text = "High Score: $0";
        }

    }
    public void ExitButton() { 
        Application.Quit();
        Debug.Log("Game closed");
    }

    public void StartGame() { 
        // Destroy(GameObject.FindWithTag("Game Manager"));
        SceneManager.LoadScene(1);
        
    }

}