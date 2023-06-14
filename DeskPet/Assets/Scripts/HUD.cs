using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class HUD : MonoBehaviour
{
    public Button button;
    public TextMeshProUGUI buttonTxt;

    public InputAction button1, button2;

    private void Update()
    {
        if (button1.IsPressed())
        {
            print("butt1");
        }

        if (button2.IsPressed())
        {
            print("butt2");
        }
    }
}
