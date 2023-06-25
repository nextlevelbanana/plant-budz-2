using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate3D : MonoBehaviour
{
   public Vector3 rotAmt;

    private void Update()
    {
        transform.Rotate(rotAmt * Time.deltaTime);
    }
}
