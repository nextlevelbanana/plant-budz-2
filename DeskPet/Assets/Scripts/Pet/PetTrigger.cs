using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetTrigger : MonoBehaviour
{
    public PetBehavior pb;

    private CircleCollider2D col;

    private void Awake()
    {
        col = GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Wall")
        {
            print("Trig hit");
            pb.InitWallClimb(other.GetComponent<BoxCollider2D>());
        }
    }
}
