using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using TMPro;

public class NPCPathfinding : MonoBehaviour
{
    private AIPath aiPath;
    public GameObject target;
    public AstarPath path;

    public TextMeshProUGUI debugText;
    private void Awake()
    {
        aiPath = GetComponent<AIPath>();
        aiPath.canMove = true;
        SetDestination(target.transform);
    }

    private void Start()
    {
        path.Scan();
    }

    private void Update()
    {
        if(aiPath.destination != target.transform.position)
        {
            SetDestination(target.transform);
        }
    }

    private void OnApplicationFocus(bool focus)
    {
        path.Scan();
        debugText.text = "Focus + rescan" + System.DateTime.Now;
    }
    public void SetDestination(Transform location)
    {
        aiPath.canMove = true;
        aiPath.destination = location.position;
    }
}
