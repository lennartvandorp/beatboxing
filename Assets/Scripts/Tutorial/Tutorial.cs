using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    public GameObject[] tutorials;
    int currentTutorial;

    bool wait = false;
    float waitTime;
    // Start is called before the first frame update
    void Start()
    {
        currentTutorial = 0;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentTutorial)
        {
            case 0:
                MovetoNextOnKey(KeyCode.D);
                break;
            case 1:
                waitUntilNext(3);
                break;
            case 2:
                MovetoNextOnKey(KeyCode.Mouse1);
                break;
            case 3:
                waitUntilNext(3);
                break;
            case 4:
                MovetoNextOnKey(KeyCode.Space);
                break;
            case 5:
                waitUntilNext(3);
                break;
            case 6:
                waitUntilNext(5);
                break;
        }

        if (wait)
        {
            waitTime -= Time.deltaTime;
            Debug.Log(waitTime);
            if (waitTime <= 0)
            {
                MoveToNext();
            }
        }
    }

    void MovetoNextOnKey(KeyCode key)
    {
        if (Input.GetKeyDown(key))
        {
            MoveToNext();
        }
    }

    void waitUntilNext(float duration)
    {
        if (!wait)
        {
            waitTime = duration;
        }
        wait = true;

    }
    void MoveToNext()
    {
        if (currentTutorial < tutorials.Length)
        {
            tutorials[currentTutorial].GetComponent<Text>().color = Color.green;
            currentTutorial++;
            if (currentTutorial < tutorials.Length)
            {
                tutorials[currentTutorial].GetComponent<Text>().enabled = true;
            }
            else { SceneManager.LoadScene("Music"); }
        }
        wait = false;
    }
}
