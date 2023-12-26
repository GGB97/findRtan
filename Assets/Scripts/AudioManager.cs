using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip bgm;

    AudioSource audioSource;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        audioSource.clip = bgm;
    }

    void Start()
    {
        audioSource.Play();
    }
}
