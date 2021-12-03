using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoButtonHandler : MonoBehaviour
{
    public void noHandler() {
        GameManager gm = FindObjectOfType<GameManager>();
        gm.lifeCanvasDisplaying = false;
        gm.unpauseGame();
        Destroy(GameObject.FindWithTag("Life Canvas"));
    }
}
