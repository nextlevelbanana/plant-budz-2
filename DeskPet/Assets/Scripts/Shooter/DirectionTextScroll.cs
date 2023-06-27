using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DirectionTextScroll : MonoBehaviour
{
    private TMP_Text directionsText;
    public float fadeTime = 10f;

    private void Start()
    {
        directionsText = GetComponent<TMP_Text>();
        directionsText.CrossFadeAlpha(0, fadeTime, false);
        StartCoroutine(MoveText());
    }
    private IEnumerator MoveText()
    {
        float moveTime = fadeTime;
        while(moveTime > 0)
        {
            Vector2 curT = transform.position;
            curT.y += 0.005f;
            transform.position = curT;
            moveTime -= Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
        yield break;
    }
}
