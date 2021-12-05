using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public static AudioPlayer singleton { get; private set; }

    private AudioSource audio_bird = new AudioSource();


    private void Awake()
    {
        if (!singleton)
        {
            singleton = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        audio_bird.playOnAwake = false;
    }

    public void Play_Bird_death(int i)
    {
        audio_bird.Play();
    }
}
