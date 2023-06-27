using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Tooltip("The direction, x and y, that the object moves")]
    public Vector2 direction = new Vector2(0f, -1f);
    public Vector2 patrolOffset = new Vector2(-1f, 0f);
    public float timePerPatrol = 3f;
    private float timePatrolling = 0f;

    public void Start()
    {
        timePatrolling = timePerPatrol / 2f;
    }


    // Update is called once per frame
    void Update()
    {
        MoveSideToSide();
    }



    void MoveSideToSide()
    {
 
        timePatrolling += Time.deltaTime;

        float patrolProgress = timePatrolling / timePerPatrol;

        int directionMultiplier = (int)patrolProgress % 2 == 0 ? 1 : -1;

        transform.position = Vector3.Lerp(transform.position, transform.position + (Vector3)patrolOffset * directionMultiplier, Time.deltaTime);

    }
}


