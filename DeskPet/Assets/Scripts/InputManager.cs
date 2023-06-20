using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class InputManager : MonoBehaviour
{
    //assign button behavior + icon based on current phase
    //'If time allows' - add swatter and cleanup tools
    //fade function could be nice for the icons
    //endallfunctions needs tweaking

    //Current logic: 1 = water, 2 = petting, 3 = food, 4 = fling
    public InputAction actionKey;
    private int curButton = 0;
    public PetInteractionReaction petReaction;
    public int highestToolAllowed = 1;
    private GameObject currentTool = null;
    [SerializeField] LayerMask playerLayer;
    public bool ignoreInput = false;

    [Header("Water")]
    public GameObject waterIcon;
    public ParticleSystem waterParticles;
    private BoxCollider2D waterTrig;

    [Header("Petting")]
    public GameObject pettingIcon;

    [Header("Food")]
    public GameObject foodIcon;
    public GameObject[] possibleFood;
    private int curFood = 0, maxFood;
    private bool stopFood = false;

    [Header("Fling")]
    public GameObject flingIcon;
    public DragAndShoot shootScript;

    private void Start()
    {
        maxFood = possibleFood.Length;
        waterParticles.Pause();
        waterTrig = waterParticles.GetComponent<BoxCollider2D>();
        waterTrig.enabled = false;
        EndAllFunctions();
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
        if (!Application.isFocused) { EndAllFunctions(); }

        if (ignoreInput) { return; }

        NumericInput();

        if (actionKey.IsPressed()) { ActionKeyPress(); }

        if (actionKey.WasReleasedThisFrame()) { ActionKeyRelease(); }

        if(currentTool != null)
        {
            ToolFollowCursor();
        }
    }

    private bool isMouseOverPet()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var hits = Physics2D.GetRayIntersectionAll(ray, 100f, playerLayer);
        foreach(var hit in hits)
        {
            if(hit.collider.tag == "Player")
            {
                return true;
            }
        }

        return false;
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
                if(number > highestToolAllowed) { number = 0; }
                curButton = number;
            }
        }
    }

    private void ActionKeyPress()
    {
        switch (curButton)
        {
            case 0:
                GameManager.instance.SetDebugMessage("No tool selected");
                break;

            case 1:
                InitWater();
                break;

            case 2:
                InitPetting();
                break;

            case 3:
                InitFood();
                break;

            case 4:
                InitFling();
                break;
        }
    }

    private void ActionKeyRelease()
    {
        switch (curButton)
        {
            case 1:
                ReleaseWater();
                break;

            case 2:
                ReleasePetting();
                break;

            case 3:
                ReleaseFood();
                break;

            case 4:
                ReleaseFling();
                break;
        }

    }

    public void EndAllFunctions()
    {
        //release fling triggers dazed anim - find workaround
        ReleaseWater();
        ReleasePetting();
        ReleaseFood();
        ReleaseFling();
        //currentTool = null;
        //curButton = 0;
    }

    private void InitWater()
    {
        //seems unnecessary but it's a quirk of the particle system...
        waterIcon.SetActive(true);
        if (!waterParticles.isPlaying) { waterParticles.Play(); }
        waterTrig.enabled = true;
        currentTool = waterIcon;
    }

    private void ReleaseWater()
    {
        if (!waterParticles.isStopped) { waterParticles.Stop(true, ParticleSystemStopBehavior.StopEmitting); }
        waterTrig.enabled = false;
        waterIcon.SetActive(false);
    }

    public void InitPetting()
    {
        pettingIcon.SetActive(true);
        currentTool = pettingIcon;
        if (isMouseOverPet())
        {
            petReaction.PetPetted();
        }
    }

    public void ReleasePetting()
    {
        pettingIcon.SetActive(false);
    }

    private void InitFood()
    {
        foodIcon.SetActive(true);
        currentTool = foodIcon;
        if (stopFood) { return; }
        Vector3 foodPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        foodPos.z = 0;
        Instantiate(possibleFood[curFood], foodPos, Quaternion.identity, null);
        FoodCheck();
        stopFood = true;
    }

    private void FoodCheck()
    {
        curFood++;
        if(curFood >= maxFood)
        {
            curFood = 0;
        }
    }

    public void ReleaseFood()
    {
        stopFood = false;
        foodIcon.SetActive(false);
    }

    public void InitFling()
    {
        flingIcon.SetActive(true);
        shootScript.StartDrag();
        currentTool = flingIcon;
    }

    public void ReleaseFling()
    {
        flingIcon.SetActive(false);
        shootScript.EndDrag();
    }

}
