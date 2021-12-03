using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YesButtonHandler : MonoBehaviour
{
    public void yesHandler() {
        GameManager gm = FindObjectOfType<GameManager>();
        gm.addToLife(1);
        gm.addToScore(-1000);
        gm.lifeCanvasDisplaying = false;
        gm.unpauseGame();
        Destroy(GameObject.FindWithTag("Life Canvas"));
    }
}
