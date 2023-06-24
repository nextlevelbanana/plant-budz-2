using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource[] bkg;
    public AudioSource[] sfx;
    private float fadeTime = 3.5f;
    public bool transitioningSong = false;
    private AudioSource aud;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        bkg[0].Play();
    }

    public void AudioButtonPushed()
    {
        if (transitioningSong) { return; }

        transitioningSong = true;

        int num = 0;

        for (int i = 0; i < bkg.Length; i++)
        {
            if (bkg[i].isPlaying)
            {
                StartCoroutine(FadeOut(bkg[i], fadeTime));
                num = i;
            }
        }

        num += 1;

        if (num > bkg.Length -1)
        {
            num = 0;
        }

        StartCoroutine(FadeIn(bkg[num], fadeTime));
        Invoke("FadeOff", fadeTime);
    }

    private void FadeOff()
    {
        transitioningSong = false;
    }

    public void PlayBKG(int num)
    {
        if (transitioningSong) { return; }

        transitioningSong = true;

        for(int i = 0; i <bkg.Length; i++)
        {
            if (bkg[i].isPlaying)
            {
                StartCoroutine(FadeOut(bkg[i], fadeTime));
            }
        }

        StartCoroutine(FadeIn(bkg[num], fadeTime));
        Invoke("FadeOff", fadeTime);
    }

    public void PlaySFX(int num)
    {
        sfx[num].Play();
    }

    public IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }

    public IEnumerator FadeIn(AudioSource audioSource, float FadeTime)
    {
        float startVolume = 0.2f;

        audioSource.volume = 0;
        audioSource.Play();

        while (audioSource.volume < 1.0f)
        {
            audioSource.volume += startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        audioSource.volume = 1f;
    }

    public void PlaySound(AudioClip sound)
    {
        aud.PlayOneShot(sound);
    }
}
