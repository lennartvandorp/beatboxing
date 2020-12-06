using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class HitPauze : MonoBehaviour
{
    bool waiting;

    WaitForSecondsRealtime wait;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Freeze(float seconds)
    {
        if (waiting)
        { return; }
        Time.timeScale = 0f;
        StartCoroutine(Wait(seconds));
    }

    IEnumerator Wait(float duration)
    {
        waiting = true;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1f;
        waiting = false;
    }


}
