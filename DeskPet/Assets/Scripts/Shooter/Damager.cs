using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
 [Tooltip("How much damage we will do every time we touch a Damagable")]
    public float damageToDo = 1;

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
    public void OnCollisionEnter2D(Collision2D other)
    {
        //Check if the thing that touched us has a damagable
        other.gameObject.TryGetComponent<Damagable>(out Damagable dam);

        //If it does, and it's not friendly
        if(dam != null && dam.faction != faction)
        {
            //Deal damage to the damagable we touched!
            dam.TakeDamage(damageToDo);
            //After that, check if the thing that touched us has an InvincibleOnHit
            InvincibleOnHit invincibleOnHit = other.gameObject.GetComponent<InvincibleOnHit>();

            if(invincibleOnHit != null)
            {
                invincibleOnHit.InvincibleStart();
            }

            if (deleteAfterCollision) { Destroy(gameObject); }
            
        }
    }
}
