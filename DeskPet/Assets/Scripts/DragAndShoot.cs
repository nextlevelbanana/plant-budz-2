using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DragAndShoot : MonoBehaviour
{
    [SerializeField] float shotPower = 10f;
    private Rigidbody2D rb;
    private LineDrag ld;
    [SerializeField] Vector2 minPower, maxPower;
    [SerializeField] Vector2 force;
    private Vector3 startDrag, endDrag;
    //public KeyCode dragKey = KeyCode.Space;
    public InputAction dragKey;
    private Camera cam;

    private void OnEnable()
    {
        dragKey.Enable();
    }

    private void OnDisable()
    {
        dragKey.Disable();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ld = GetComponent<LineDrag>();
        cam = Camera.main;
    }

    private void Update()
    {
        if (dragKey.IsPressed())
        {
            Vector3 curPoint = cam.ScreenToWorldPoint(Input.mousePosition);
            startDrag = transform.position;
            startDrag.z = 0f;
            curPoint.z = 0f;
            ld.RenderLine(startDrag, curPoint);
        }
        


        if(dragKey.WasReleasedThisFrame())
        {
            endDrag = cam.ScreenToWorldPoint(Input.mousePosition);
            endDrag.z = 0f;

            force = new Vector2(Mathf.Clamp(startDrag.x - endDrag.x, minPower.x, maxPower.x), Mathf.Clamp(startDrag.y - endDrag.y, minPower.y, maxPower.y));
            rb.AddForce(force * shotPower, ForceMode2D.Impulse);

            ld.EndLine();
        }
    }
}
