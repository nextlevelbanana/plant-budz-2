using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDown: MonoBehaviour
{
    public Vector2 direction = new Vector2(0f, -1f);
    
    void Update()
    {
        Vector2 newPosition = transform.position;

        Vector2 distanceToMove = direction * Time.deltaTime;

        newPosition += distanceToMove;

        transform.position = newPosition;
    }
}