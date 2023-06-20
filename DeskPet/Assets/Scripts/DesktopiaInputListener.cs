using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesktopiaInputListener : MonoBehaviour
{
    [SerializeField] UnityEngine.UI.Text display;
    int count;
    public SpriteRenderer sr;
    [SerializeField] Sprite square;
    [SerializeField] Sprite tab;

    void Start()
    {
        // Register all the listeners
        Desktopia.Inputs.AddOnKeyDown(KeyCode.Escape, Quit);
        Desktopia.Inputs.AddOnMouseDown(0, MouseTest);
    }

    void Update()
    {
        // Increment a value when Space is pressed
        // if(Desktopia.Inputs.GetKey(KeyCode.Space)) ++count;
        // display.text = count.ToString();
    }

    //an attempt at a general utility function - 
    //unclear whether it should be the renderer or the sprite itself? 
    //but this works for my one test case at least
    public bool isClickedOn(SpriteRenderer spriteRenderer) {
        var rawCL = Camera.main.ScreenToWorldPoint(Desktopia.Cursor.Position);
        var clickLocation = new Vector3(rawCL.x, -rawCL.y, 0); //idk if the z index hacking is necessary, nor why I needed to invert the Y value
        return (sr.bound.Contains(clickLocation));
    }

    void MouseTest( ) {
        if (isClickedOn(sr)) {
            if (sr.sprite != square) {
                sr.sprite = square;
            } else {
                sr.sprite = tab;
            }
        }
    }

    void Quit()
    {
        Application.Quit();
    }
}
