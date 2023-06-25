using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSettings : MonoBehaviour
{
    [Tooltip("How many seconds between shots")]
    public float firingSpeed = 1f;
    public float autoFireRate = 0.5f;

    public AudioClip fireSound;

    // Start is called before the first frame update
    void Start()
    {
        if (fireSound != null && BulletAudioManager.Instance != null)
        {
            BulletAudioManager.Instance.PlaySound(fireSound);
        }
    }
}