using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButtonTest : MonoBehaviour
{
    public Button testButton;
    public Color newColor;


    private void Start()
    {
        testButton = GetComponent<Button>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<BoxCollider2D>())
        {
            testButton.image.color = newColor;
        }
    }
}
