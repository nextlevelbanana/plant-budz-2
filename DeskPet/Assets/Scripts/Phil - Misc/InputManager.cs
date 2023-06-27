using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour
{
    private int curButton = 0;
    public PetInteractionReaction petReaction;
    private GameObject currentTool = null;
    [SerializeField] LayerMask playerLayer;
    //public GameObject[] cursors;
    public CursorIcons[] cursors;

    [Header("Button Tab")]
    public Animator buttonLayoutAnim;
    private string openTrig = "Open", closeTrig = "Close";
    private bool buttonsAreOpen = false;

    [Header("Buttons")]
    public UIButtons[] uiButtons;
    private bool newButtonSelectionIgnore = false;

    [Header("Water")]
    public ParticleSystem waterParticles;
    private BoxCollider2D waterTrig;

    [Header("Food")]
    public GameObject[] possibleFood;
    private int curFood = 0, foodCount;
    private bool stopFood = false;

    [Header("Fling")]
    public DragAndShoot shootScript;
    public ScoreHole target;
    private void Start()
    {
        Desktopia.Inputs.AddOnMouseDown(0, MouseDown);
        Desktopia.Inputs.AddOnMouseUp(0, MouseRelease);
        foodCount = possibleFood.Length;
        waterParticles.Pause();
        waterTrig = waterParticles.GetComponent<BoxCollider2D>();
        waterTrig.enabled = false;
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
            if (button.isSelected) 
            {
                NoTool();
                button.SetUnselected();
                return;
            }

            //clicked button
            newButtonSelectionIgnore = true;
            curButton = button.GetButtonNum();
            SwapIcon();
            cursors[curButton].IdleTool();
            button.SetSelected();
            DeselectUnusedButtons();
        }
    }

    public void MouseRelease()
    {
        if (newButtonSelectionIgnore) 
        {
            newButtonSelectionIgnore = false;
            //specific behavior with slingshot
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

    private void DeselectUnusedButtons()
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

    public void UnlockAllButtons()
    {
        buttonLayoutAnim.SetTrigger(openTrig);
        buttonsAreOpen = true;
    }

    private void ActionKeyPress()
    {
        cursors[curButton].UseTool();

        switch (curButton)
        {
            case 0:
                //GameManager.instance.SetDebugMessage("No tool selected");
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

            case 7:
                break;

            case 8:
                
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
                //just need to know if mouse button down + over player.
                //ReleasePetting();
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

            case 7:
                ResetGame();
                break;

            case 8:
                ToggleButtonsTab();
                break;
        }

    }

    private void NoTool()
    {
        curButton = 0;
        EndAllFunctions();
    }

    public void AudioButtonUp()
    {
        AudioManager.instance.AudioButtonPushed();
        curButton = 0;
        DeselectUnusedButtons();
    }

    public void EndAllFunctions()
    {
        AllIconsOff();
        ReleaseWater();
        //ReleasePetting();
        ReleaseFood();
        shootScript.FalseEnd();
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

    #region Water

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
        AudioManager.instance.StopWater();

    }


    #endregion

    #region Petting

    public void InitPetting()
    {
        if (isMouseOverPet())
        {
            petReaction.PetPetted();
        }
    }

    #endregion

    #region Food
    private void InitFood()
    {
        if (stopFood) { return; }
        Vector3 foodPos = Camera.main.ScreenToWorldPoint(Desktopia.Cursor.Position);
        foodPos.y = -foodPos.y;
        foodPos.z = 0;
        GameObject clone = Instantiate(possibleFood[curFood], foodPos, Quaternion.identity, null);
        GameManager.instance.AddFood(clone);
        CycleFood();
        stopFood = true;
    }

    private void CycleFood()
    {
        curFood++;
        if (curFood >= foodCount)
        {
            curFood = 0;
        }
    }

    public void ReleaseFood()
    {
        stopFood = false;
    }

    #endregion

    #region Fling
    public void InitFling()
    {
        shootScript.StartDrag();
    }

    public void ReleaseFling()
    {
        if (!cursors[4].gameObject.activeInHierarchy) { shootScript.FalseEnd(); return; }
        shootScript.EndDrag();
    }

    #endregion



    private void QuitApp()
    {
        Application.Quit();
    }

    private void ResetGame()
    {
        SceneManager.LoadScene(1);
    }

    private void ToggleButtonsTab()
    {
        curButton = 0;
        //DeselectUnusedButtons();
        uiButtons[7].SetUnselected();

        if (buttonsAreOpen)
        {
            buttonLayoutAnim.SetTrigger(closeTrig);
            buttonsAreOpen = false;
            return;
        }

        buttonLayoutAnim.SetTrigger(openTrig);
        buttonsAreOpen = true;
    }

}
