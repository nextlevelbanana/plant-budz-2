using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Damagable : MonoBehaviour
{
    [Tooltip("How much damage this Damagable takes before it dies")]
    public float maximumHitPoints = 3;

    [Tooltip("The number of points that will be awarded upon death")]
    public int pointValue;

    [Tooltip("Damagers don't hurt Damagables of the same faction")]
    public int faction = 1;

    // [Tooltip("The sound to play on a hit that doesn't result in a death. Defaults to nothing.")]
    // public AudioClip hitSound;

    // [Tooltip("The sound to play on death. Defaults to nothing.")]
    // public AudioClip deathSound;

    private float currentHitPoints;

    public float GetCurrentHitPoints()
    {
        return currentHitPoints;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHitPoints = maximumHitPoints;
    }

    //This function gets called by other scripts when its time to take damage
    public void TakeDamage(float damageAmount)
    {
        ModifyHitPoints(-damageAmount);
    }

    //We can do the same thing, but positive, to heal us
    public void HealDamage(float healAmount)
    {
        ModifyHitPoints(healAmount);
    }

    //This function adds or subtracts health
    private void ModifyHitPoints(float modAmount )
    {
        currentHitPoints += modAmount;

        if( currentHitPoints > maximumHitPoints )
        {
            currentHitPoints = maximumHitPoints;
        }

        if( currentHitPoints <= 0 )
        {
            Die();
        }
        else if( modAmount < 0 )
        {
            //Could add animation here for when hit!
            // if (hitSound != null && AudioManager.Instance != null)
            // {
            //     AudioManager.Instance.PlaySound(hitSound);
            // }
        }
    }

    //This function is called when our health is 0
    private void Die()
    {
        //Could add animation here!
        // if (deathSound != null && AudioManager.Instance != null)
        // {
        //     AudioManager.Instance.PlaySound(deathSound);
        // }

        // if( UIController.Instance != null )
        // {
        //     UIController.Instance.ChangeScore(pointValue);
        // }

        Destroy(gameObject);
    }
}
