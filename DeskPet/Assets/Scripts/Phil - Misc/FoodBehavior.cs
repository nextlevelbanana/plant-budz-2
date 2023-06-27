using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodBehavior : MonoBehaviour
{
    //private Rigidbody2D rb;
    public CircleCollider2D trigger;
    public CircleCollider2D physCol;
    private Animator anim;
    private bool sfxPlayed = false;
    private Rigidbody2D rb;
    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (sfxPlayed) { return; }

        if(other.tag == "Ground")
        {
            AudioManager.instance.PlaySFX(9);
            sfxPlayed = true;
        }
    }

    private void OnDestroy()
    {
        if(GameManager.instance.foodEaten > 10)
        {
            AudioManager.instance.UpsetTum();
        }
    }

    public void PlayEat(Vector2 eatPos)
    {
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
        physCol.enabled = false;
        trigger.enabled = false;
        transform.position = eatPos;
        anim.SetTrigger("Eat");
        Destroy(this.gameObject, 2.1f);
    }
}
