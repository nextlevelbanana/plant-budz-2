using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShooterPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.instance?.pos != null)
            transform.position = GameManager.instance.pos;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
