using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BulletAudioManager : MonoBehaviour
{
    public static BulletAudioManager Instance { get; private set; }

    private AudioSource audioSource;
    private bool soundOff = false;
    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip sound)
    {
        if (soundOff) { return; }
        audioSource.PlayOneShot(sound);
    }

    public void Muffle()
    {
        audioSource.volume = 0.05f;
    }

}