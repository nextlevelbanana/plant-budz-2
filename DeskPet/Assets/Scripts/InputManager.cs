using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class InputManager : MonoBehaviour
{

    //separate function for just activating cursor & setting current tool. second sprite for using it...
   
    private int curButton = 0;
    public PetInteractionReaction petReaction;
    private GameObject currentTool = null;
    [SerializeField] LayerMask playerLayer;
    //public GameObject[] cursors;
    public CursorIcons[] cursors;

    [Header("Buttons")]
    public UIButtons[] uiButtons;
    private bool newButtonSelectionIgnore = false;

    [Header("Water")]
    public ParticleSystem waterParticles;
    private BoxCollider2D waterTrig;

    [Header("Food")]
    public GameObject[] possibleFood;
    private int curFood = 0, maxFood;
    private bool stopFood = false;

    [Header("Fling")]
    public DragAndShoot shootScript;

    

    private void Start()
    {
        Desktopia.Inputs.AddOnMouseDown(0, MouseDown);
        Desktopia.Inputs.AddOnMouseUp(0, MouseRelease);
        maxFood = possibleFood.Length;
        waterParticles.Pause();
        waterTrig = waterParticles.GetComponent<BoxCollider2D>();
        waterTrig.enabled = false;
        StarterButtons();
        EndAllFunctions();
    }



    private void Update()
    {
        if (Desktopia.Inputs.GetMouseButton(0) && !newButtonSelectionIgnore) { ActionKeyPress(); }

        if(currentTool != null || curButton != 0)
        {
            ToolFollowCursor();
        }
    }

    private bool isMouseOverPet()
    {
        Vector3 clickLoc = Camera.main.ScreenToWorldPoint(Desktopia.Cursor.Position);
        clickLoc = new Vector2(clickLoc.x, -clickLoc.y);
        Collider2D col = Physics2D.OverlapCircle(clickLoc, 0.1f, playerLayer);
        if (col == null) { return false; }

        if(col.tag == "Player") { return true; }

        return false;
    }

    public void MouseDown()
    {
        Vector3 clickLoc = Camera.main.ScreenToWorldPoint(Desktopia.Cursor.Position);
        clickLoc = new Vector2(clickLoc.x, -clickLoc.y);
        Collider2D col = Physics2D.OverlapCircle(clickLoc, 0.1f, playerLayer);
        if (col == null) { return; }

        if (col.TryGetComponent<UIButtons>(out UIButtons button))
        {
            //clicked button
            newButtonSelectionIgnore = true;
            curButton = button.SetSelected();
            SwapIcon();
            cursors[curButton].IdleTool();
            button.SetSelected();
            DeselectButtons();
        }
    }

    public void MouseRelease()
    {
        if (newButtonSelectionIgnore) 
        {
            newButtonSelectionIgnore = false;
            if(curButton == 4) { return; }
        }
        ActionKeyRelease();
    }
    private void ToolFollowCursor()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Desktopia.Cursor.Position);
        mousePos = new Vector2(mousePos.x, -mousePos.y);
        currentTool.transform.position = mousePos;
    }

    private void DeselectButtons()
    {
        for (int i = 0; i < uiButtons.Length; i++)
        {
            if (!uiButtons[i].gameObject.activeInHierarchy) { return; }

            if (uiButtons[i].buttonNum != curButton)
            {
                uiButtons[i].SetUnselected();
            }
        }
    }

    private void StarterButtons()
    {
        uiButtons[1].gameObject.SetActive(false);
        uiButtons[2].gameObject.SetActive(false);
        uiButtons[3].gameObject.SetActive(false);
        /*for(int i = 0; i < uiButtons.Length; i++)
        {
            if (uiButtons[i].buttonNum != 1)
            {
                uiButtons[i].gameObject.SetActive(false);
            }
        }*/
    }

    public void UnlockAllButtons()
    {
        for (int i = 0; i < uiButtons.Length; i++)
        {
            uiButtons[i].gameObject.SetActive(true);
        }
    }

    private void ActionKeyPress()
    {
        cursors[curButton].UseTool();

        switch (curButton)
        {
            case 0:
                GameManager.instance.SetDebugMessage("No tool selected");
                break;

            case 1:
                InitWater();
                break;

            case 2:
                InitFood();
                break;

            case 3:
                InitPetting();
                break;

            case 4:
                InitFling();
                break;

            case 5:
                //AudioButtonUp();
                break;

            case 6:
                //QuitApp();
                break;
        }
    }

    private void ActionKeyRelease()
    {
        cursors[curButton].IdleTool();

        switch (curButton)
        {
            case 0:
                break;

            case 1:
                ReleaseWater();
                break;

            case 2:
                ReleaseFood();
                break;

            case 3:
                ReleasePetting();
                break;

            case 4:
                ReleaseFling();
                break;

            case 5:
                AudioButtonUp();
                break;

            case 6:
                QuitApp();
                break;
        }

    }

    public void AudioButtonUp()
    {
        AudioManager.instance.AudioButtonPushed();
    }

    public void EndAllFunctions()
    {
        //release fling triggers dazed anim - find workaround
        AllIconsOff();
        ReleaseWater();
        ReleasePetting();
        ReleaseFood();
        shootScript.FalseEnd();
        //currentTool = null;
        //curButton = 0;
    }

    public void AllIconsOff()
    {
        for(int i = 0; i < cursors.Length; i++)
        {
            cursors[i].gameObject.SetActive(false);
        }
    }

    public void SwapIcon()
    {
        for(int i = 0; i < cursors.Length; i++)
        {

            if(i == curButton)
            {
                cursors[i].gameObject.SetActive(true);
            }
            else
            {
                cursors[i].gameObject.SetActive(false);
            }
        }

        currentTool = cursors[curButton].gameObject;
    }

    private void InitWater()
    {
        if (!waterParticles.isEmitting) { waterParticles.Play(); }
        waterTrig.enabled = true;
        WaterSound();
    }

    private void WaterSound()
    {
        if (AudioManager.instance.sfx[7].isPlaying) { return; }
        AudioManager.instance.PlaySFX(7);
    }

    private void ReleaseWater()
    {
        if (!waterParticles.isStopped) { waterParticles.Stop(true, ParticleSystemStopBehavior.StopEmitting); }
        waterTrig.enabled = false;
        StartCoroutine(AudioManager.instance.FadeOut(AudioManager.instance.sfx[7], 0.5f));
    }

    public void InitPetting()
    {
        if (isMouseOverPet())
        {
            petReaction.PetPetted();
        }
    }

    public void ReleasePetting()
    {

    }

    private void InitFood()
    {
        if (stopFood) { return; }
        Vector3 foodPos = Camera.main.ScreenToWorldPoint(Desktopia.Cursor.Position);
        foodPos.y = -foodPos.y;
        foodPos.z = 0;
        GameObject clone = Instantiate(possibleFood[curFood], foodPos, Quaternion.identity, null);
        GameManager.instance.foodObjectsOnScreen.Add(clone);
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
    }

    public void InitFling()
    {
        shootScript.StartDrag();
    }

    public void ReleaseFling()
    {
        if (!cursors[4].gameObject.activeInHierarchy) { shootScript.FalseEnd(); return; }
        shootScript.EndDrag();
    }

    private void QuitApp()
    {
        Application.Quit();
    }

}
