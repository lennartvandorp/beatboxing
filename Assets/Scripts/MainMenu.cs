using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour
{
    public Button startButton;

    // Start is called before the first frame update
    void Update(){
        if(Input.GetKeyDown(KeyCode.Mouse0)){
            SwitchToPlayingState();
        }

    }

    public void SwitchToPlayingState(){
        SceneManager.LoadScene("samplescene");
    }
}
