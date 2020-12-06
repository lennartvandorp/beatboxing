using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DramaticText : MonoBehaviour
{
    Text text;
    Color color;
    float amountOfRed;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        color = new Color(amountOfRed, 0, 0);
        amountOfRed += Time.deltaTime * .2f;
        if (amountOfRed > 1)
        {
            amountOfRed = 1;
            SceneManager.LoadScene("SampleScene");
        }
        text.color = color;
    }
}
