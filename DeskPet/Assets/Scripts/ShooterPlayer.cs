using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.position = GameManager.instance.pos;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
