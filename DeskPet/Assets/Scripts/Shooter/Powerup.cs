using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            SetPowerup(other.gameObject);
        }
    }

    private void SetPowerup(GameObject player)
    {
        player.GetComponent<PlayerManager>().IncreasePowerup();
        Destroy(gameObject);
    }
}
