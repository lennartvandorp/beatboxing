using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public AudioObject[] objects;
    public GameObject boss;

    float waitUntilPlay = 4;
    bool hasStarted;

    // Start is called before the first frame update
    void Awake()
    {
        foreach (AudioObject a in objects)
        {
            a.setSource(gameObject.AddComponent<AudioSource>());
            a.getSource().pitch = a.pitch;
            a.getSource().volume = a.volume;
            a.getSource().clip = a.audioClip;
        }
    }

    void Start()
    {
    }

    void Update()
    {
        foreach (AudioObject a in objects)
        {
            if (a.name == "music")
            {
                AudioSource source = a.getSource();
                if (!source.isPlaying && hasStarted)
                {
                    ResetSong();
                    Play("music");
                }
            }
        }
        if (!hasStarted)
        {
            waitUntilPlay -= Time.deltaTime;
        }
        if (waitUntilPlay < 0 && !hasStarted)
        {
            ResetSong();
        }

    }

    public void Play(string name)
    {
        AudioObject a = Array.Find(objects, AudioObject => AudioObject.name == name);
        a.getSource().Play();
    }

    void ResetSong()
    {
        Play("music");
        hasStarted = true;
        boss.GetComponent<BossScript>().ResetMusic();
    }

}
