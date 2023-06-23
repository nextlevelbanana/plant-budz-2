using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibleOnHit : MonoBehaviour
{
    public float invincibilityLength = 3f;

    public Color colorOnHit;

    //the collider on the same object as this script
    private Collider2D myCollider;

    //the collider on the same object as this script
    private SpriteRenderer spriteRenderer;

    private Color startColor;

    //the time at which this obect got hit, stored so we can time the flash speed
    private float hitStartTime;

    // Start is called before the first frame update
    void Start()
    {
        //find the damagable attached to this same gameObject
        myCollider = GetComponent<Collider2D>();

        if(myCollider == null)
        {
            //A baked in warning message if you forget a damagable
            Debug.LogWarning("InvincibleOnHit component missing a Collider on:" + gameObject.name);
        }

        //find the SpriteRenderer attached to this same gameObject
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer == null)
        {
            //A baked in warning message if you forget a damagable
            Debug.LogWarning("InvincibleOnHit component missing a SpriteRenderer on:" + gameObject.name);
        }
        else
        {
            startColor = spriteRenderer.color;
        }
    }

    public void Update()
    {
        if (Time.time - hitStartTime > invincibilityLength && myCollider.enabled == false)
        {
            //End the invincible
            InvincibleEnd();
        }
    }

    // Update is called once per frame
    public void InvincibleStart()
    {
        hitStartTime = Time.time;

        if(myCollider != null)
        {
            myCollider.enabled = false;
        }

        if(spriteRenderer != null)
        {
            Color newColor = colorOnHit;
            spriteRenderer.color = newColor;
        }
    }

    public void InvincibleEnd()
    {
        if (myCollider != null)
        {
            myCollider.enabled = true;
            spriteRenderer.color = startColor;
        }
    }
}
