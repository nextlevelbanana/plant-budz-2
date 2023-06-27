using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreHole : MonoBehaviour
{
    public List<BoxCollider2D> collidersInScene = new List<BoxCollider2D>();
    private float timer = 3f;
    private SpriteRenderer sr;
    //public KeyCode resetKey = KeyCode.R;

    private void Start()
    {
        //GatherColliders();
        sr = GetComponent<SpriteRenderer>();
        //NewHolePlacement();
    }

    private void OnEnable()
    {
        StartCoroutine(PlacementCo());
    }
    public void NewHolePlacement()
    {
        timer = 3f;
        for (int i = 0; i < 10; i++)
        {
            float spawnY = Random.Range
                (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y, Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height -15)).y);
            float spawnX = Random.Range
                (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x);

            transform.position = new Vector2(spawnX, spawnY);
        }
    }

    private IEnumerator PlacementCo()
    {
        sr.enabled = false;
        NewHolePlacement();

        while(timer > 0)
        {
            timer -= Time.deltaTime;
            yield return null;
        }

        sr.enabled = true;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.TryGetComponent<BoxCollider2D>(out BoxCollider2D col))
        {
            NewHolePlacement();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            //score++?
            AudioManager.instance.PlaySFX(17);
            StartCoroutine(PlacementCo());
            return;
        }
    }

    public void GatherColliders()
    {
        var cols = FindObjectsOfType<BoxCollider2D>();
        foreach(BoxCollider2D b in cols)
        {
            if (b.isTrigger) { return; }

            collidersInScene.Add(b);
        }

    }
}
