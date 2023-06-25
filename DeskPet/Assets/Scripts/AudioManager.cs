using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource[] bkg;
    public AudioSource[] sfx;
    private bool fadingOut = false;
    private bool fadingIn = false;
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
        if (fadingOut || fadingIn) { return; }

        int num = 0;

        for (int i = 0; i < bkg.Length; i++)
        {
            if (bkg[i].isPlaying)
            {
                StartCoroutine(FadeAudioOut(i, 3.5f));
                //bkg[i].Stop();
                num = i + 1;
            }
        }

        //num += 1;

        if (num > bkg.Length -1)
        {
            StopAllAudio();
            return;
        }

        //bkg[num].Play();
        StartCoroutine(FadeAudioIn(num, 3.5f));

    }
    public void PlayBKG(int num)
    {
        if (fadingOut || fadingIn) { return; }

        for (int i = 0; i <bkg.Length; i++)
        {
            if (bkg[i].isPlaying)
            {
                StartCoroutine(FadeAudioOut(i, 3.5f));
                //bkg[i].Stop();
            }
        }

        //bkg[num].Play();
        StartCoroutine(FadeAudioIn(num, 3.5f));
    }

    public void PlaySFX(int num)
    {
        sfx[num].Play();
    }

    public void StopWater()
    {
        sfx[7].Stop();
    }

    public void UpsetTum()
    {
        if(GameManager.instance.foodObjectsOnScreen.Count > 7)
        {
            PlaySFX(16);
            return;
        }

        PlaySFX(15);
    }

    public void StopAllAudio()
    {
        for(int i = 0; i < bkg.Length; i++)
        {
            bkg[i].Stop();
        }
    }

    public IEnumerator FadeAudioIn(int track, float duration)
    {
        if (fadingIn) { yield break; }
        fadingIn = true;
        float currentTime = 0;
        bkg[track].volume = 0;
        bkg[track].Play();
        float start = bkg[track].volume;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            bkg[track].volume = Mathf.Lerp(start, 1, currentTime / duration);
            yield return null;
        }

        bkg[track].volume = 1;
        fadingIn = false;
        yield break;
    }

    public IEnumerator FadeAudioOut(int track, float duration)
    {
        if (fadingOut) { yield break; }
        fadingOut = true;
        float currentTime = 0;
        float start = bkg[track].volume;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            bkg[track].volume = Mathf.Lerp(start, 0, currentTime / duration);
            yield return null;
        }

        bkg[track].volume = 0;
        bkg[track].Stop();
        fadingOut = false;
        yield break;
    }

}
