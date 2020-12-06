using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MomentMaker : MonoBehaviour
{
    List<float> moments = new List<float>();

    string export;
    bool hasStarted;

    float currentTime;
    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0;

    }

    //Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime * 100;
        if(!hasStarted && currentTime > 400){
            hasStarted = true;
            currentTime = 0;
        }

        if(Input.GetMouseButtonDown(0) && hasStarted){
            moments.Add(currentTime);
            export = $"{export}, {currentTime.ToString()}f";
            Debug.Log(export);
        }
        if(Input.GetMouseButtonDown(1)){
            }

        }
        public void SetCurrentTime(){}
    }

