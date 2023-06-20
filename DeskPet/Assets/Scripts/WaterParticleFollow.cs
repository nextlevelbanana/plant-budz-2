using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterParticleFollow : MonoBehaviour
{
     [SerializeField] Transform followPos;

    private void Update()
    {
        transform.position = followPos.transform.position;
    }


}
