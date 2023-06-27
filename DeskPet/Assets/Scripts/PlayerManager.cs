using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public Transform shotPoint;
    public Transform[] dualShotPoints;

    public GameObject projectile;
    //public float shootSpeed = 5;
    public float coolDown = 0.8f;
    private bool stopShoot = false;
    public void ShootBullet()
    {
        if (stopShoot) { return; }
        stopShoot = true;
        Instantiate(projectile, shotPoint.transform.position, Quaternion.identity);
        Invoke("ResetShoot", coolDown);
    }

    private void ResetShoot()
    {
        stopShoot = false;
    }

    public void PlayerMove(Vector2 move)
    {
        transform.position = move;
    }
}
