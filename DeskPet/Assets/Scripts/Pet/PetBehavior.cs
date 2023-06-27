using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PetBehavior : MonoBehaviour
{
    public enum currentBehavior { Patrol, FindFood, ExternalControl, WallClimb, EatFood, Sleep };
    public currentBehavior behavior;

    [SerializeField] Transform curTarget = null;
    [SerializeField] LayerMask groundLayer;
    private Rigidbody2D rb;
    private PetInteractionReaction petReaction;

    [Header("Animation")]
    private Animator anim;
    private SpriteRenderer sr;
    private bool flippedLeft = false;

    [Header("FishMod")]
    public bool isFish;
    [SerializeField] float fishGravMod = 0.01f;
    [SerializeField] float fishMoveSpeed;

    [Header("Asleep")]
    [SerializeField] float timeUntilSleep = 25f;
    private float sleepTimeHold = 0f;
    public bool isAsleep = false;

    [Header("Trigger")]
    [SerializeField] PetTrigger trigger;
    [SerializeField] float ignoreCollisionTime = 3f;

    [Header("Idle Patrol")]
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float timeToMove = 2f;
    [SerializeField] float timeToPause = 2f;
    [SerializeField] private float minMoveTime = 2f;
    [SerializeField] private float maxMoveTime = 10f;
    [SerializeField] private bool isMoving;
    [SerializeField] private bool stopMoving;
    [SerializeField] Vector2 moveDir = Vector2.zero;
    [SerializeField] private float[] possibleMoveDir;
    private int moveDirCounter = 0;

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
    [SerializeField] Transform eatPosition;
    [SerializeField] float eatPauseTime = 2f;

    [Header("External Control")]
    private currentBehavior lastBehavior = currentBehavior.ExternalControl;
    private bool initialGroundCheck = true;

    //IF we're doing climbing, it should only climb windows now

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        StartBehavior(currentBehavior.Patrol);
        anim = GetComponent<Animator>();
        petReaction = GetComponent<PetInteractionReaction>();
        sleepTimeHold = timeUntilSleep;
        moveDirCounter = Random.Range(0, possibleMoveDir.Length);
    }

    private void Update()
    {
        switch (behavior)
        {
            case currentBehavior.ExternalControl:
                break;

            case currentBehavior.Sleep:
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

        AnimUpdate();

        if (!isAsleep)
        {
            timeUntilSleep -= Time.deltaTime;
            if(timeUntilSleep < 0)
            {
                anim.SetBool("Asleep", true);
                isAsleep = true;
                StartBehavior(currentBehavior.Sleep);
            }
        }
    }

    private void FixedUpdate()
    {
        if (isAsleep) { return; }

        if (isMoving)
        {
            if (!isFish) { moveDir.y = rb.velocity.y; }

            rb.AddForce(moveDir, ForceMode2D.Force);

            FlipChar();
            //sr.flipX = moveDir.x < 0;
        }

        if (stopMoving) 
        {
            rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0, .05f * Time.fixedDeltaTime), rb.velocity.y);
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
        PetReset();
    }

    private void FlipChar()
    {
        if(moveDir.x < 0 && !flippedLeft)
        {
            //flip left
            Vector2 curScale = transform.localScale;
            curScale.x *= -1;
            transform.localScale = curScale;
            flippedLeft = true;
            return;
        }


        if(moveDir.x > 0 && flippedLeft)
        {
            Vector2 curScale = transform.localScale;
            curScale.x *= -1;
            transform.localScale = curScale;
            flippedLeft = false;
        }
    }

    public void StartBehavior(currentBehavior newBehavior, Collider2D obj = null)
    {
        behavior = newBehavior;

        switch (newBehavior)
        {
            case currentBehavior.ExternalControl:
                break;

            case currentBehavior.Sleep:
                break;

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

    public void SleepCheck()
    {
        //Actions trigger this
        if (isAsleep)
        {
            //wake up
            StartBehavior(currentBehavior.Patrol);
            anim.SetBool("Asleep", false);
            isAsleep = false;
            GameManager.instance.ResetFoodScore();
        }

        timeUntilSleep = sleepTimeHold;
    }
    public void EnableFishMode()
    {
        rb.gravityScale = fishGravMod;
        moveSpeed = fishMoveSpeed;
        isFish = true;
    }

    public void EnableCatMode()
    {
        moveSpeed += 0.7f;
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
        for (int i = 0; i < 10; i++)
        {
            float spawnY = Random.Range
                (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y, Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y);
            float spawnX = Random.Range
                (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x);

            transform.position = new Vector2(spawnX, spawnY);
        }

        if (!isFish) { rb.gravityScale = 1; }
        
    }

    public void AnimUpdate()
    {
        anim.SetFloat("VelocityX", rb.velocity.x);
    }

    public void SetExternalControl(float triggerOffTime)
    {
        if (behavior == currentBehavior.ExternalControl) { return; }
        //lastBehavior = behavior;
        StartBehavior(currentBehavior.ExternalControl);
        isMoving = false;
        isAsleep = false;
        climbing = false;
        findingFood = false;
        StartCoroutine(trigger.IgnoreCollision(triggerOffTime));
    }

    public void EndExternalControl()
    {
        StartBehavior(currentBehavior.Patrol);
        //StartBehavior(lastBehavior);
    }

    public void RBZero()
    {
        rb.velocity = Vector2.zero;
    }

    #region Fling

    public void StartFling()
    {
        if (behavior == currentBehavior.ExternalControl) { return; }
        lastBehavior = behavior;
        StartBehavior(currentBehavior.ExternalControl);
        StartCoroutine(CheckEndFling());
        isMoving = false;
        climbing = false;
        findingFood = false;
    }

    private IEnumerator CheckEndFling()
    {
        //small pause needed to get pet into the air, then see when it lands
        if (initialGroundCheck) { yield return new WaitForSeconds(.5f); }
        initialGroundCheck = false;

        while (!isGrounded())
        {
            yield return null;
        }

        EndFling();
    }

    public void EndFling()
    {
        StartBehavior(lastBehavior);
        initialGroundCheck = true;
    }

    #endregion

    #region Patrol

    private void StartMoving()
    {
        timeToPause = Random.Range(minMoveTime, maxMoveTime);
        moveDir.x = possibleMoveDir[moveDirCounter];
        if (isFish) { moveDir.y = Random.Range(0.05f , 1f); }
        moveDir *= moveSpeed;
        isMoving = true;
        moveDirCounter++;
        if(moveDirCounter > possibleMoveDir.Length - 1)
        {
            moveDirCounter = 0;
        }
    
    }

    private void StopMoving() {
        isMoving = false;
        timeToMove = Random.Range(minMoveTime, maxMoveTime);
        stopMoving = true;
    }

    private void IdlePatrol()
    {
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
        if (!isFish) { rb.gravityScale = 1; }
        
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
            StartBehavior(currentBehavior.Patrol);
        }
    }

    private void GetTargetPos()
    {
        moveDir = new Vector2(Mathf.Clamp(curTarget.transform.position.x - transform.position.x, -chaseSpeedMod, chaseSpeedMod), 0);
    }

    public void FoodFound()
    {
        //slow down
        if (!isFish) { rb.gravityScale = 1; }

        petReaction.PetEatingAnim();
        StartCoroutine(ReturnToPatrol(eatPauseTime));
        curTarget = null;
        findingFood = false;
    }
    private void DestroyFood(GameObject food)
    {
        if(food == null || behavior == currentBehavior.ExternalControl) { return; }
        isMoving = false;
        stopMoving = true;
        AudioManager.instance.PlaySFX(3);
        GameManager.instance.RemoveFood(food);
        food.GetComponent<FoodBehavior>().PlayEat(eatPosition.position);
        FoodFound();
    }

    #endregion

}
