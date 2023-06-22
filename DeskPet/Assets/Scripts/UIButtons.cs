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

    private void Start()
    {
        highlight.gameObject.SetActive(false);
        sr = GetComponent<SpriteRenderer>();
        unpressedSprite = sr.sprite;
    }
    public int SetSelected()
    {
        sr.sprite = pressedSprite;
        highlight.gameObject.SetActive(true);
        isSelected = true;
        return buttonNum;
    }

    public void SetUnselected()
    {
        sr.sprite = unpressedSprite;
        highlight.gameObject.SetActive(false);
        isSelected = false;
    }
}
