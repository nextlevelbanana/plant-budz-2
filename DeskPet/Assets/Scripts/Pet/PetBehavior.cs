using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetBehavior : MonoBehaviour
{
    private Rigidbody2D rb;
    public enum currentBehavior { Patrol, FindFood, ExternalControl, WallClimb };
    public currentBehavior behavior;


    [Header("Idle Patrol")]
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] float timeToMove = 2f;
    [SerializeField] float timeToPause = 2f;
    private float pauseTimeHold;
    private float moveTimeHold;
    private bool moving = false;
    [SerializeField] Vector2 moveDir = Vector2.zero;
    private Vector2 lastDir = Vector2.zero;

    [Header("Wall Climb")]
    [SerializeField] float climbSpeed = 4f;
    private bool climbing = false;
    private Collider2D wallCol;
    private Vector2 wallDir;
    private float wallHeight = 0f;
    [SerializeField] float cornerOffset = 0.2f;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        moveTimeHold = timeToMove;
        pauseTimeHold = timeToPause;
        behavior = currentBehavior.Patrol;
    }

    private void Update()
    {
        switch (behavior)
        {
            case currentBehavior.ExternalControl:
                break;

            case currentBehavior.Patrol:
                IdlePatrol();
                break;

            case currentBehavior.WallClimb:
                ScaleWall();
                break;
        }
    }

    private void FixedUpdate()
    {
        if (moving)
        {
            rb.AddForce(moveSpeed * moveDir, ForceMode2D.Force);
        }

        if (climbing)
        {
            rb.AddForce(climbSpeed * moveDir, ForceMode2D.Force);
        }
    }

    private void IdlePatrol()
    {
        if (moving)
        {
            timeToMove -= Time.deltaTime;
            if (timeToMove <= 0f)
            {
                timeToMove = moveTimeHold;
                moveDir = RandomDirection();
                moving = false;
            }
        }
        else
        {
            timeToPause -= Time.deltaTime;
            if (timeToPause <= 0f)
            {
                timeToPause = pauseTimeHold;
                moving = true;
            }
        }
    }

    private Vector2 RandomDirection()
    {
        return new Vector2(1, 0);
        /*Vector2 newDir = new Vector2(Random.Range(-1, 1), 0);
        if(newDir == lastDir) { return lastDir *= -1; }
        lastDir = newDir;
        return newDir;*/
    }

    public void InitWallClimb(BoxCollider2D col)
    {
        behavior = currentBehavior.WallClimb;
        moving = false;
        rb.gravityScale = 0;
        climbing = true;
        wallCol = col;
        wallHeight = col.size.y;
        float direcitonCheck = AngleDir(col.transform.position, transform.position);
        if(direcitonCheck < 0f)
        {
            //left
            moveDir = new Vector2(-1, 1);
        }
        else
        {
            //right
            moveDir = new Vector2(1, 1);
        }
    }

    public static float AngleDir(Vector2 A, Vector2 B)
    {
        //B is left of A = negative
        // right = positive
        // 0 = aligned
        return -A.x * B.y + A.y * B.x;
    }

    private void ScaleWall()
    {
        if(transform.position.y + cornerOffset > wallHeight)
        {
            Vector2 edgeAssist = new Vector2(wallDir.x, 0);
            rb.AddForce(edgeAssist * climbSpeed, ForceMode2D.Impulse);
            EndClimb();
        }
    }

    public void EndClimb()
    {
        moving = true;
        rb.gravityScale = 1;
        climbing = false;
        //food check?
        behavior = currentBehavior.Patrol;
    }

}
