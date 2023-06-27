using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSettings : MonoBehaviour
{
    [Tooltip("How many seconds between shots")]
    public float firingSpeed = 1f;
    public float autoFireRate = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        //no shoot sfx anymore?
        Destroy(gameObject, 6f);
    }
}