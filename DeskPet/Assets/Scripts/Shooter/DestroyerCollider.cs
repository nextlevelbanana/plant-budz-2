using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyerCollider : MonoBehaviour
{
    public bool isBackCol;
    public float moveAmt = 3;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            if (isBackCol)
            {
                ShootForward(other.gameObject);
            }
            else
            {
                ShootBack(other.gameObject);
            }
        }
        else
        {
            Destroy(other.gameObject);
        }
    }

    private void ShootForward(GameObject player)
    {
        Vector2 t = player.transform.position;
        t.y += moveAmt;
        player.transform.position = t;
    }

    private void ShootBack(GameObject player)
    {
        Vector2 t = player.transform.position;
        t.y -= moveAmt;
        player.transform.position = t;
    }
}
