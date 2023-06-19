using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PetBehavior : MonoBehaviour
{
    public enum currentBehavior { Patrol, FindFood, ExternalControl, WallClimb, EatFood };
    public currentBehavior behavior;

    [SerializeField] Transform curTarget = null;
    [SerializeField] LayerMask groundLayer;
    private Rigidbody2D rb;

    [Header("Animation")]
    private Animator anim;
    private SpriteRenderer sr;

    [Header("Trigger")]
    [SerializeField] PetTrigger trigger;
    [SerializeField] float ignoreCollisionTime = 3f;

    [Header("Idle Patrol")]
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float timeToMove = 2f;
    [SerializeField] float timeToPause = 2f;
    private float pauseTimeHold = 2f;
    private float moveTimeHold = 2f;
    private float minMoveTime = 2f;
    private float maxMoveTime = 10f;
    private float minPauseTime = 2f;
    private float maxPauseTime = 10f;
    private bool isMoving;
    private bool stopMoving;
    [SerializeField] Vector2 moveDir = Vector2.zero;
    private Vector2 lastDir = Vector2.zero;

    [Header("Wall Climb")]
    [SerializeField] float climbSpeed = 4f;
    private bool climbing = false;
    private Collider2D wallCol;
    private Vector2 wallDir;
    private float wallHeight = 0f;
    [SerializeField] float cornerOffset = -0.2f;
    [SerializeField] float rotationAmount = 360f;
    [SerializeField] float rotationSpeed;

    [Header("Finding Food")]
    private bool findingFood = false;
    [SerializeField] float chaseSpeedMod = 0.75f;
    [SerializeField] float eatPauseTime = 2f;

    private TextMeshProUGUI hudButton;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        StartBehavior(currentBehavior.Patrol);
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        //GameManager.instance.SetDebugMessage(behavior.ToString());
        //new method in GameMan (if ya want!) Should make it easier for both of us to have our own debugs
        //had to comment out to test different logs

        switch (behavior)
        {
            case currentBehavior.ExternalControl:
                break;

            case currentBehavior.Patrol:
                IdlePatrol();
                break;

            // case currentBehavior.WallClimb:
            //     //ScaleWall();
            //     break;

            case currentBehavior.FindFood:
                //MoveToTarget();
                break;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            //SetFoodTarget();
        }

        AnimUpdate();
    }

    private void FixedUpdate()
    {

        if (isMoving)
        {
            rb.velocity = moveDir;
            //idle sprite was always facing right - this flips it based on current dir
            sr.flipX = moveDir.x < 0;
            //startMoving = false;
        }
        if (stopMoving) {
            rb.velocity = Vector2.zero;
            stopMoving = false;
        }

        if (climbing)
        {
            rb.AddForce(climbSpeed * moveDir, ForceMode2D.Force);
            //figure out smooth rotation...
            return;
        }

        if (findingFood)
        {
            GetTargetPos();
        }
    }

    private void OnBecameInvisible()
    {
        GameManager.instance.SetDebugMessage("Pet off screen. Resetting.");
        PetReset();
    }

    private bool isGrounded()
    {
        if (Physics2D.Raycast(transform.position, Vector2.down, 1f, groundLayer))
        {
            return true;
        }

        return false;
    }

    public void PetReset()
    {
        //Not sure if we want to check to see if pet overlaps collider? If it does, it will just shoot out and be fine
        for (int i = 0; i < 10; i++)
        {
            float spawnY = Random.Range
                (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y, Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y);
            float spawnX = Random.Range
                (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x);

            transform.position = new Vector2(spawnX, spawnY);
        }

        rb.gravityScale = 1;
    }

    public void AnimUpdate()
    {
        anim.SetFloat("VelocityX", rb.velocity.x);
    }



    #region Patrol

    private void StartMoving() {
        timeToPause = Random.Range(minMoveTime, maxMoveTime);
        moveDir = Random.Range(0f,1f) > 0.5f ? Vector2.right : Vector2.left;
        isMoving = true;
        //startMoving = true;
    }

    private void StopMoving() {
        isMoving = false;
        timeToMove = Random.Range(minMoveTime, maxMoveTime);
        stopMoving = true;
    }

    private void IdlePatrol()
    {
        //Temp comment out to test my own logs
        //GameManager.instance.SetDebugMessage(timeToMove.ToString() + rb.velocity.ToString());

        if (isMoving)
        {
            timeToMove -= Time.deltaTime;
            if (timeToMove <= 0f)
            {
                StopMoving();
            }
        }
        else
        {
            timeToPause -= Time.deltaTime;
            if (timeToPause <= 0f)
            {
                StartMoving();
            }
        }
    }

    private IEnumerator ReturnToPatrol(float pauseBeforeReturn)
    {
        yield return new WaitForSeconds(pauseBeforeReturn);
        StartBehavior(currentBehavior.Patrol);
    }

    #endregion

    #region Climbing

    private void InitWallClimb(BoxCollider2D col)
    {
        behavior = currentBehavior.WallClimb;
        isMoving = false;
        rb.gravityScale = 0;
        climbing = true;
        wallCol = col;
        wallHeight = col.size.y;
        float direcitonCheck = AngleDir(col.transform.position, transform.position);
        if (direcitonCheck < 0f)
        {
            //wall on the left
            moveDir = new Vector2(-1, 1);
            rotationAmount = Mathf.Abs(rotationAmount);
        }
        else
        {
            //right
            moveDir = new Vector2(1, 1);
            rotationAmount = -Mathf.Abs(rotationAmount);
        }
    }

    public static float AngleDir(Vector2 A, Vector2 B)
    {
        //B is left of A = negative
        //B is right of A = positive
        // 0 = aligned
        return -A.x * B.y + A.y * B.x;
    }

    private void ScaleWall()
    {
        if (transform.position.y + cornerOffset > wallHeight)
        {
            Vector2 edgeAssist = new Vector2(wallDir.x, 0);
            rb.AddForce(edgeAssist * climbSpeed, ForceMode2D.Impulse);
            EndClimb();
        }
    }

    public void EndClimb()
    {
        //otherwise climb will be initiated again
        rb.rotation = 0;
        StartCoroutine(trigger.IgnoreCollision(ignoreCollisionTime));

        rb.gravityScale = 1;
        climbing = false;

        if (findingFood)
        {
            print("Finding food target");
            SetFoodTarget();
            return;
        }

        StartBehavior(currentBehavior.Patrol);
    }

    #endregion

    public void StartBehavior(currentBehavior newBehavior, Collider2D obj = null) {
        behavior = newBehavior;
        switch (newBehavior) {
            case currentBehavior.Patrol:
                StartMoving();
                break;
            case currentBehavior.WallClimb: 
                //InitWallClimb((BoxCollider2D)obj);
                StartBehavior(currentBehavior.Patrol);
                break;
            case currentBehavior.EatFood: 
                DestroyFood(obj.gameObject);
                break;
        }
    }

        private void DestroyFood(GameObject food)
    {
        GameManager.instance.RemoveFood(food);
        FoodFound();
        Destroy(food);
    }

    #region Find Food
    private void SetFoodTarget()
    {
        Transform potentialTarget = GameManager.instance.GetClosestFood(transform).transform;
        
        if(potentialTarget != null)
        {
            behavior = currentBehavior.FindFood;
            curTarget = potentialTarget;
            findingFood = true;
            GetTargetPos();
        }
        else
        {
            print("Food not found");
            StartBehavior(currentBehavior.Patrol);
        }
    }

    private void GetTargetPos()
    {
        moveDir = new Vector2(Mathf.Clamp(curTarget.transform.position.x - transform.position.x, -chaseSpeedMod, chaseSpeedMod), 0);
    }

    public void FoodFound()
    {
        rb.velocity = new Vector2(rb.velocity.x * 0.15f, rb.velocity.y);
        rb.gravityScale = 1;
        anim.SetTrigger("Eating");
        StartCoroutine(ReturnToPatrol(eatPauseTime));
        curTarget = null;
        findingFood = false;
    }

    #endregion

}
