using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
 [Tooltip("How much damage we will do every time we touch a Damagable")]
    public float damage = 1;

    [Tooltip("Damagers don't hurt Damagables of the same faction")]
    public int faction = 1;

    [Tooltip("Whether or not this object should delete itself after it deals damage")]
    public bool deleteAfterCollision = false;

    public void Start()
    {
        if( GetComponent<Collider2D>() == null )
        {
            Debug.LogWarning("Something you attached a Damager to is missing a collider!");
        }
    }

    //If we touch something...
    public void OnCollisionEnter2D(Collision2D collision)
    {
        //Check if the thing that touched us has a damagable
        Damagable damagable = collision.gameObject.GetComponent<Damagable>();

        //If it does, and it's not friendly
        if(damagable != null && damagable.faction != faction)
        {
            //Deal damage to the damagable we touched!
            damagable.TakeDamage(damage);

            if(deleteAfterCollision)
            {
                Destroy(this.gameObject);
            }
        }

        //After that, check if the thing that touched us has an InvincibleOnHit
        InvincibleOnHit invincibleOnHit = collision.gameObject.GetComponent<InvincibleOnHit>();

        if(invincibleOnHit != null)
        {
            invincibleOnHit.InvincibleStart();
        }
    }
}
