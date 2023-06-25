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

    // Update is called once per frame
    void Update()
    {
        //verifying that bullets despawn off screen
        //textField.text = Object.FindObjectsByType<GameObject>(FindObjectsSortMode.None).Length.ToString();
    }
}
