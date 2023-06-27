using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuickUIButton : MonoBehaviour
{
    public SpriteRenderer audioButton;
    public Sprite audioPress;
    private Sprite audioStart;
    public static QuickUIButton instance;


    private void Start()
    {
        instance = this;
        audioStart = audioButton.sprite;
    }

    public void AudioButtonPress()
    {
        audioButton.sprite = audioPress;
        AudioManager.instance.AudioButtonPushed();
        Invoke("AudioButtonUp", .2f);
    }

    private void AudioButtonUp()
    {
        audioButton.sprite = audioStart;
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void ResetButton()
    {
        Destroy(GameManager.instance.gameObject);
        SceneManager.LoadScene(1);
    }
}
