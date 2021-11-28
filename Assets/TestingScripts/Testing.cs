using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Testing : MonoBehaviour
{

    DialogueSystem dialogue;

    // Start is called before the first frame update
    void Start()
    {
        dialogue = DialogueSystem.instance;
    }

    public string[] s;

    int index = 0;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) {
            if(!dialogue.isSpeaking || dialogue.isWaitingForUserInput) {
                if(index >= s.Length) {
                    index = 0;
                    int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
                    print("Testing - Going to scene " + (currentSceneIndex+1));
                    SceneManager.LoadScene(currentSceneIndex+1);
                    return;
                }
                Say(s[index++]);
            }
        }
    }

    void Say(string s) {
        string[] parts = s.Split(':');
        string speech = parts[0];
        string speaker = (parts.Length >= 2) ? parts[1] : "";

        dialogue.Say(speech, speaker);
    }
}
