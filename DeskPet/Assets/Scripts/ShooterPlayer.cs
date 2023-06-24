using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShooterPlayer : MonoBehaviour
{
    public TextMeshProUGUI textField;
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.instance?.pos != null)
            transform.position = GameManager.instance.pos;
           // textField.text = "(debug text)";
    }

    // Update is called once per frame
    void Update()
    {
        //verifying that bullets despawn off screen
        //textField.text = Object.FindObjectsByType<GameObject>(FindObjectsSortMode.None).Length.ToString();
    }
}
