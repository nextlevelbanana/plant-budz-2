using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class UIButtons : MonoBehaviour
{
    public Light2D highlight;
    public bool isSelected = false;
    public int buttonNum;
    private SpriteRenderer sr;
    public Sprite pressedSprite;
    private Sprite unpressedSprite;
    private bool isSling = false;
    public ScoreHole target;

    private void Start()
    {
        highlight.gameObject.SetActive(false);
        sr = GetComponent<SpriteRenderer>();
        unpressedSprite = sr.sprite;
        if(buttonNum == 4) { isSling = true; target.gameObject.SetActive(false); }
    }
    public int GetButtonNum()
    {
        return buttonNum;
    }

    public void SetSelected()
    {
        sr.sprite = pressedSprite;
        highlight.gameObject.SetActive(true);
        isSelected = true;
        if(isSling && !target.gameObject.activeInHierarchy) { target.gameObject.SetActive(true); }
    }


    public void SetUnselected()
    {
        sr.sprite = unpressedSprite;
        highlight.gameObject.SetActive(false);
        isSelected = false;
        if (isSling) { target.gameObject.SetActive(false); }
    }
}
