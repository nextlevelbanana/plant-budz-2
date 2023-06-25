using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorIcons : MonoBehaviour
{
    private SpriteRenderer sr;
    private Sprite idleSprite;
    public Sprite actionSprite;
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        idleSprite = sr.sprite;
    }

    public void UseTool()
    {
        sr.sprite = actionSprite;
    }

    public void IdleTool()
    {
        sr.sprite = idleSprite;
    }
}
