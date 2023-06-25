using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetTrigger : MonoBehaviour
{
    public PetBehavior pb;
    public bool ignoreCollisions;
    private CircleCollider2D col;
    public PetInteractionReaction petReaction;

    private void Awake()
    {
        col = GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (ignoreCollisions || pb.behavior == PetBehavior.currentBehavior.ExternalControl) { return; }

        if(other.tag == "Wall")
        {
           //pb.StartBehavior(PetBehavior.currentBehavior.WallClimb, other);
        }

        if(other.tag == "Food")
        {
            pb.StartBehavior(PetBehavior.currentBehavior.EatFood, other);
            StartCoroutine(IgnoreCollision(1.2f));
        }

        if(other.tag == "Water")
        {
            petReaction.PetWatered();
        }
    }

    public IEnumerator IgnoreCollision(float ignoreTime)
    {
        //if (!col.enabled) { yield break; }
        col.enabled = false;
        ignoreCollisions = true;

        yield return new WaitForSeconds(ignoreTime);

        ignoreCollisions = false;
        col.enabled = true;
        yield break;
    }
}
