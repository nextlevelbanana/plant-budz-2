using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShooterInputManager : MonoBehaviour
{
    [Tooltip("How fast the player moves back and forth")]
    public float moveSpeed = 5f;

    private GameObject curShooter = null;
    private PlayerManager pm = null;
    private bool ready = false;
    private Vector2 vRef = Vector2.zero;
    public LayerMask uiLayer;
    public EventSystem es;
    //A timestamp of the moment we last fired
    //private float lastTimeFired;

    //private ProjectileSettings projectileSettings;

    public void Awake()
    {
        //lastTimeFired = Time.time;
    }
    // Start is called before the first frame update
    void Start()
    {
        //Desktopia.Inputs.AddOnMouseDown(0, MouseDown);
    }

    public void SetCurPlayer(GameObject curPlayer)
    {
        curShooter = curPlayer;
        pm = curShooter.GetComponent<PlayerManager>();
        ready = true;
    }

 void Update()
    {
        /*if( Desktopia.Inputs.GetKey(KeyCode.LeftArrow) || Desktopia.Inputs.GetKey(KeyCode.A) )
        {
            Vector2 newPosition = curShooter.transform.position;
            newPosition.x -= moveSpeed * Time.deltaTime;
            pm.PlayerMove(newPosition);
        }

        if (Desktopia.Inputs.GetKey(KeyCode.RightArrow) || Desktopia.Inputs.GetKey(KeyCode.D) )
        {
            Vector2 newPosition = curShooter.transform.position;
            newPosition.x += moveSpeed * Time.deltaTime;
            pm.PlayerMove(newPosition);
        }

        if( Desktopia.Inputs.GetKey(KeyCode.UpArrow) || Desktopia.Inputs.GetKey(KeyCode.W) )
        {
            Vector2 newPosition = curShooter.transform.position;
            newPosition.y += moveSpeed * Time.deltaTime;
            pm.PlayerMove(newPosition);
        }

        if( Desktopia.Inputs.GetKey(KeyCode.DownArrow) || Desktopia.Inputs.GetKey(KeyCode.S) )
        {
            Vector2 newPosition = curShooter.transform.position;
            newPosition.y -= moveSpeed * Time.deltaTime;
            pm.PlayerMove(newPosition);
        }*/
        if (!ready) { return; }
        PlayerFollowCursor();


        if (Desktopia.Inputs.GetMouseButton(0))
        {
            pm.ShootBullet();
        }
    }

    public void MouseDown()
    {
        /*Vector3 clickLoc = Camera.main.ScreenToWorldPoint(Desktopia.Cursor.Position);
        clickLoc = new Vector2(clickLoc.x, -clickLoc.y);
        Collider2D col = Physics2D.OverlapCircle(clickLoc, 0.1f, uiLayer);
        //if (col == null) { return; }

        if (col.TryGetComponent<Button>(out Button button))
        {
            //button.onClick.Invoke();
            //ExecuteEvents.Execute(button.gameObject, new BaseEventData(es), ExecuteEvents.submitHandler);
        }*/
    }

    private void PlayerFollowCursor()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Desktopia.Cursor.Position);
        mousePos = new Vector2(mousePos.x, -mousePos.y);
        curShooter.transform.position = Vector2.SmoothDamp(curShooter.transform.position, mousePos, ref vRef, moveSpeed);
        //curShooter.transform.position = Vector2.Lerp(curShooter.transform.position, mousePos, moveSpeed);
        //curShooter.transform.position = mousePos;
    }
}
