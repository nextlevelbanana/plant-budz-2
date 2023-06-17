using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetTrigger : MonoBehaviour
{
    public PetBehavior pb;
    public bool ignoreCollisions;
    private CircleCollider2D col;

    private void Awake()
    {
        col = GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (ignoreCollisions) { return; }

        if(other.tag == "Wall")
        {
           //pb.StartBehavior(PetBehavior.currentBehavior.WallClimb, other);
        }

        if(other.tag == "Food")
        {
            pb.StartBehavior(PetBehavior.currentBehavior.EatFood, other);
            print("YUMMY YUMMY");
        }
    }
    public IEnumerator IgnoreCollision(float ignoreTime)
    {
        col.enabled = false;
        ignoreCollisions = true;

        while(ignoreTime > 0f)
        {
            ignoreTime -= Time.deltaTime;
            yield return null;
        }

        ignoreCollisions = false;
        col.enabled = true;
        yield break;
    }
}
