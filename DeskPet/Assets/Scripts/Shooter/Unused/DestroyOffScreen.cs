using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DestroyOffScreen : MonoBehaviour
{
    public float spawnTime;

    void Start() {
        spawnTime = Time.time;
    }

    void Update()
    {
        //not working :/ Just set a hard destroy upon spawn
        /*Vector2 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        if (!JustSpawned() && (screenPosition.y > Screen.height || screenPosition.y < 0
                || screenPosition.x > Screen.width || screenPosition.x < 0)) {
            Destroy(this.gameObject);   
        }*/
    }

    private bool JustSpawned() 
    {
        //allows objects to spawn offscreen
        return Time.time - spawnTime < 2f;
    }
}
