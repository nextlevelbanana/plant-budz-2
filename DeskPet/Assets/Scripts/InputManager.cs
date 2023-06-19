using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class InputManager : MonoBehaviour
{
    //assign button behavior + icon based on current phase
    //'If time allows' - add swatter and cleanup tools


    //Current logic: 1 (key) = water, 2 = petting, 3 = food, 4 = fling
    public InputAction actionKey;

    public DragAndShoot shootScript;

    private int curButton = 0;
    public PetInteractionReaction petReaction;
    private GameObject currentTool = null;

    [Header("Water")]
    public ParticleSystem waterParticles;
    private BoxCollider2D waterTrig;

    private void Start()
    {
        waterParticles.Pause();
        waterTrig = waterParticles.GetComponent<BoxCollider2D>();
        waterTrig.enabled = false;
    }

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

        if(currentTool != null)
        {
            ToolFollowCursor();
        }
    }

    private void ToolFollowCursor()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currentTool.transform.position = mousePos;
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
                InitWater();
                break;

            case 2:
                Press2();
                break;

            case 3:
                //press 3
                break;

            case 4:
                Press4();
                break;

            default:
                GameManager.instance.SetDebugMessage("No tool selected");
                break;
        }
    }

    private void ActionKeyRelease()
    {
        switch (curButton)
        {
            case 1:
                WaterOff();
                break;

            case 4:
                Release4();
                break;
        }

    }

    private void InitWater()
    {
        //seems unnecessary but it's a quirk of the particle system...
        if (!waterParticles.isPlaying) { waterParticles.Play(); }
        waterTrig.enabled = true;
        currentTool = waterParticles.gameObject;
    }

    private void WaterOff()
    {
        if (!waterParticles.isStopped) { waterParticles.Stop(); }
        waterTrig.enabled = false;
    }

    public void Press2()
    {

    }

    public void Press4()
    {
        shootScript.StartDrag();
    }

    public void Release4()
    {
        shootScript.EndDrag();
    }




}
