using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DatingInputManager : MonoBehaviour
{
    public TMP_Text debugTxt;
    public LayerMask buttonsLayer;
    public QuickUIButton settingsButtons;

    private void Start()
    {
        Desktopia.Inputs.AddOnMouseDown(0, MouseDown);
    }

    public void MouseDown()
    {

        Vector3 clickLoc = Camera.main.ScreenToWorldPoint(Desktopia.Cursor.Position);
        clickLoc = new Vector2(clickLoc.x, -clickLoc.y);
        Collider2D col = Physics2D.OverlapCircle(clickLoc, 0.15f, buttonsLayer);

        if (col == null) { return; }

        if (col.TryGetComponent<ButtonTag>(out ButtonTag button))
        {
            if(button.buttonNum == 0)
            {
                debugTxt.text = "Pushed prefab button";
                button.GetComponent<ButtonTag>().HandleChoice();
            }

            if (button.buttonNum == 1) { settingsButtons.AudioButtonPress(); }

            if (button.buttonNum == 2) { settingsButtons.ResetButton(); }

            if (button.buttonNum == 3) { settingsButtons.QuitButton(); }
        }
    }
}
