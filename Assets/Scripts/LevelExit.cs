using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelExit : MonoBehaviour
{
    int player;
    void Awake()
    {
        player = LayerMask.NameToLayer("Player");
    }
    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == player) {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex+1);
        }     
    }
}
