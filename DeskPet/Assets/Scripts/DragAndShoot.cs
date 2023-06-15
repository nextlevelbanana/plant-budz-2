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
    private Camera cam;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ld = GetComponent<LineDrag>();
        cam = Camera.main;
    }

    public void StartDrag()
    {
        Vector3 curPoint = cam.ScreenToWorldPoint(Input.mousePosition);
        startDrag = transform.position;
        startDrag.z = 0f;
        curPoint.z = 0f;
        ld.RenderLine(startDrag, curPoint);
    }

    public void EndDrag()
    {
        endDrag = cam.ScreenToWorldPoint(Input.mousePosition);
        endDrag.z = 0f;

        force = new Vector2(Mathf.Clamp(startDrag.x - endDrag.x, minPower.x, maxPower.x), Mathf.Clamp(startDrag.y - endDrag.y, minPower.y, maxPower.y));
        rb.AddForce(force * shotPower, ForceMode2D.Impulse);

        ld.EndLine();
    }
}
