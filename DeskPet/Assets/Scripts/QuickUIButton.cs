using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuickUIButton : MonoBehaviour
{
    public Button audioButton;
    public Sprite audioPress;
    private Sprite audioStart;



    private void Start()
    {
        audioStart = audioButton.image.sprite;
    }

    public void AudioButtonPress()
    {
        audioButton.image.sprite = audioPress;
        AudioManager.instance.AudioButtonPushed();
        Invoke("AudioButtonUp", .2f);
    }

    private void AudioButtonUp()
    {
        audioButton.image.sprite = audioStart;
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void ResetButton()
    {
        SceneManager.LoadScene(1);
    }
}
