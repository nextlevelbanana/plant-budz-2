using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoFire : MonoBehaviour
{
    [Tooltip("Which prefab to spawn on fire")]
    public GameObject projectilePrefab;

    [Tooltip("Which position to spawn the projectiles from")]
    public Transform spawnPoint;

    //A timestamp of the moment we last fired
    private float lastTimeFired;

    private ProjectileSettings projectileSettings;

    public void Awake()
    {
        lastTimeFired = Time.time;
    }
    // Start is called before the first frame update
    void Start()
    {
        projectileSettings = projectilePrefab.GetComponent<ProjectileSettings>();

        if (projectileSettings == null)
        {
            Debug.LogWarning("The equipped projectile is missing settings.");
        }
    }

    void Update()
    {
       
        if (Time.time - lastTimeFired > projectileSettings.autoFireRate)
        {
            lastTimeFired = Time.time;
            GameObject spawnedPrefab = Instantiate(projectilePrefab, spawnPoint.transform.position, Quaternion.identity) as GameObject;
        }
    }
}
