using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    public static UIController Instance { get; private set; }

    public ShooterInputManager shootManager;

    public GameObject curPlayer = null, playerFish, playerCat;

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
        //winScreen.SetActive(false);
        loseScreen.SetActive(false);
        scoreText.text = defaultScoreText + currentScore;

        curPlayer = Instantiate(playerCat, Vector2.zero, Quaternion.identity);

        /*if (GameManager.instance.finalForm == 3)
        {
            curPlayer = Instantiate(playerFish, GameManager.instance.pos, Quaternion.identity);
        }
        else
        {
            curPlayer = Instantiate(playerCat, GameManager.instance.pos, Quaternion.identity);
        }*/

        shootManager.SetCurPlayer(curPlayer);
    }

    public void Update()
    {
        if(curPlayer == null && loseScreen != null)
        {
            loseScreen.SetActive(true);
            healthText.text = defaultHealthText + "0";
        }

        if (curPlayer != null && curPlayer.GetComponent<Damagable>())
        {
            healthText.text = defaultHealthText + curPlayer.GetComponent<Damagable>().GetCurrentHitPoints();
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
