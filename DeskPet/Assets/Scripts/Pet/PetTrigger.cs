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
            pb.InitWallClimb(other.GetComponent<BoxCollider2D>());
        }

        if(other.tag == "Food")
        {
            print("YUMMY YUMMY");
            DestroyFood(other.gameObject);
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

    private void DestroyFood(GameObject food)
    {
        GameManager.instance.RemoveFood(food);
        pb.FoodFound();
        Destroy(food);
    }
}
