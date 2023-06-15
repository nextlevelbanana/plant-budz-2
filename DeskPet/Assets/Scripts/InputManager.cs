using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class InputManager : MonoBehaviour
{
    public InputAction actionKey;

    public DragAndShoot shootScript;

    private int curButton = 0;

    [Header("Cursor Settings")]
    public Texture2D petCursor;
    private Vector2 cursorHotSpot = Vector2.zero;

    private void OnEnable()
    {
        actionKey.Enable();
    }

    private void OnDisable()
    {
        actionKey.Disable();
    }

    private void Update()
    {
        NumericInput();

        if (actionKey.IsPressed()) { ActionKeyPress(); }

        if (actionKey.WasReleasedThisFrame()) { ActionKeyRelease(); }
    }

    private void NumericInput()
    {
        if (Input.inputString != "")
        {
            int number;
            bool is_a_number = Int32.TryParse(Input.inputString, out number);
            if (is_a_number && number >= 0 && number < 10)
            {
                curButton = number;
            }
        }
    }

    private void ActionKeyPress()
    {
        switch (curButton)
        {
            case 1:
                Press1();
                break;

            case 2:
                Press2();
                break;

            default:
                print("default key");
                break;
        }
    }

    private void ActionKeyRelease()
    {
        switch (curButton)
        {
            case 1:
                Release1();
                break;
        }

        DefaultCursor();
    }

    private void DefaultCursor()
    {
        Cursor.SetCursor(null, cursorHotSpot, CursorMode.Auto);
    }

    public void Press1()
    {
        shootScript.StartDrag();
    }

    public void Release1()
    {
        shootScript.EndDrag();
    }

    public void Press2()
    {
        Cursor.SetCursor(petCursor, cursorHotSpot, CursorMode.Auto);
        print("swap cursor");
    }


}
