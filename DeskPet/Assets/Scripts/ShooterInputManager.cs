using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterInputManager : MonoBehaviour
{
    [Tooltip("Which prefab to spawn on fire")]
    public GameObject projectilePrefab;

    [Tooltip("Which position to spawn the projectiles from")]
    public Transform spawnPoint;

    [Tooltip("How fast the player moves back and forth")]
    public float moveSpeed = 5f;

    //A timestamp of the moment we last fired
    private float lastTimeFired;

    public void Awake()
    {
        lastTimeFired = Time.time;
    }
    // Start is called before the first frame update
    void Start()
    {
        Desktopia.Inputs.AddOnKeyDown(KeyCode.Escape, Quit);
    }

 void Update()
    {
        if( Desktopia.Inputs.GetKey(KeyCode.LeftArrow) || Desktopia.Inputs.GetKey(KeyCode.A) )
        {
            Vector2 newPosition = transform.position;
            newPosition.x -= moveSpeed * Time.deltaTime;
            transform.position = newPosition;
        }

        if (Desktopia.Inputs.GetKey(KeyCode.RightArrow) || Desktopia.Inputs.GetKey(KeyCode.D) )
        {
            Vector2 newPosition = transform.position;
            newPosition.x += moveSpeed * Time.deltaTime;
            transform.position = newPosition;
        }

        if( Desktopia.Inputs.GetKey(KeyCode.UpArrow) || Desktopia.Inputs.GetKey(KeyCode.W) )
        {
            Vector2 newPosition = transform.position;
            newPosition.y += moveSpeed * Time.deltaTime;
            transform.position = newPosition;
        }

        if( Desktopia.Inputs.GetKey(KeyCode.DownArrow) || Desktopia.Inputs.GetKey(KeyCode.S) )
        {
            Vector2 newPosition = transform.position;
            newPosition.y -= moveSpeed * Time.deltaTime;
            transform.position = newPosition;
        }

        if( Desktopia.Inputs.GetKey(KeyCode.Space) )
        {
            Fire();
        }
    }

    public void Fire()
    {
        ProjectileSettings projectileSettings = projectilePrefab.GetComponent<ProjectileSettings>();

        if (projectileSettings == null)
        {
            Debug.LogWarning("The equipped projectile is missing settings.");
        }
        else if (Time.time - lastTimeFired > projectileSettings.firingSpeed)
        {
            lastTimeFired = Time.time;
            GameObject spawnedPrefab = Instantiate(projectilePrefab, spawnPoint.transform.position, Quaternion.identity) as GameObject;
        }
    }

    void Quit() {
        Application.Quit();
    }
}
