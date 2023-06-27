using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.instance?.pos != null)
            transform.position = GameManager.instance.pos;
    }

}
