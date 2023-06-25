using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    public static UIController Instance { get; private set; }

    public GameObject playerObject;

    public TMP_Text scoreText;

    public string defaultScoreText = "SCORE: ";

    public TMP_Text healthText;

    public string defaultHealthText = "HP: ";

    public GameObject winScreen;

    public GameObject loseScreen;

    private int currentScore = 0;

    private float elapsedTime = 0f;

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

    public void Start()
    {
        winScreen.SetActive(false);
        loseScreen.SetActive(false);
        scoreText.text = defaultScoreText + currentScore;
    }

    public void Update()
    {
        if(playerObject == null && loseScreen != null)
        {
            BulletAudioManager.Instance.Muffle();
            loseScreen.SetActive(true);
            healthText.text = defaultHealthText + "0";
        }

        if (playerObject != null && playerObject.GetComponent<Damagable>())
        {
            healthText.text = defaultHealthText + playerObject.GetComponent<Damagable>().GetCurrentHitPoints();
        }
    }

    public void ChangeScore(int scoreChange)
    {
        currentScore += scoreChange;
        scoreText.text = defaultScoreText + currentScore;
    }

    public void ShowWinScreen()
    {
        winScreen.SetActive(true);
    }
}
