using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource[] bkg;
    public AudioSource[] sfx;

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
        int num = 0;
        for (int i = 0; i < bkg.Length; i++)
        {
            if (bkg[i].isPlaying)
            {
                StartCoroutine(FadeOut(bkg[i], 3.5f));
                num = i + 1;
                if(num > bkg.Length)
                {
                    num = 0;
                }
            }
        }

        StartCoroutine(FadeIn(bkg[num], 3.5f));
    }

    public void PlayBKG(int num)
    {
        for(int i = 0; i <bkg.Length; i++)
        {
            if (bkg[i].isPlaying)
            {
                StartCoroutine(FadeOut(bkg[i], 3.5f));
            }
        }

        StartCoroutine(FadeIn(bkg[num], 3.5f));
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
}
