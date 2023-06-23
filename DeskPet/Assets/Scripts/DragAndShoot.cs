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
    private PetInteractionReaction petReaction;
    private bool dragSfxPlayed = false;
    private void Start()
    {
        petReaction = GetComponent<PetInteractionReaction>();
        rb = GetComponent<Rigidbody2D>();
        ld = GetComponent<LineDrag>();
        cam = Camera.main;
    }

    public void StartDrag()
    {
        Vector3 curPoint = cam.ScreenToWorldPoint(Desktopia.Cursor.Position);
        curPoint = new Vector2(curPoint.x, -curPoint.y);
        startDrag = transform.position;
        startDrag.z = 0f;
        ld.RenderLine(startDrag, curPoint);

        if (dragSfxPlayed) { return; }
        AudioManager.instance.PlaySFX(6);
        dragSfxPlayed = true;
    }
    public void FalseEnd()
    {
        //for scenarios where you wouldn't expect the fling action...
        ld.EndLine();
    }

    public void EndDrag()
    {
        endDrag = cam.ScreenToWorldPoint(Desktopia.Cursor.Position);
        endDrag = new Vector2(endDrag.x, -endDrag.y);

        petReaction.PetStartFling();
        force = new Vector2(Mathf.Clamp(startDrag.x - endDrag.x, minPower.x, maxPower.x), Mathf.Clamp(startDrag.y - endDrag.y, minPower.y, maxPower.y));
        rb.AddForce(force * shotPower, ForceMode2D.Impulse);
        AudioManager.instance.PlaySFX(5);
        dragSfxPlayed = false;
        ld.EndLine();
    }
}
