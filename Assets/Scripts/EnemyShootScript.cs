using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class EnemyShootScript : MonoBehaviour
{
    public GameObject projectile;

    public float shootInterval = 1.0f;

    public float shootTimer = 1.0f;

    public GameObject target;
    public Transform projectileSpawnLocation;

    private void Start()
    {
        shootTimer = shootInterval;
    }

    // Update is called once per frame
    void Update()
    {

        shootTimer -= Time.deltaTime;

        if(shootTimer < 0.0f)
        {
            shootTimer = shootInterval;

            FireBulletAtPlayer();
        }

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            FireBulletAtPlayer();
        }

    }

    public float projectileSpeed = 2.0f;
    public float yOffsetTarget = 1.0f;


    private void FireBulletAtPlayer()
    {

        GameObject bullet = Instantiate(projectile, projectileSpawnLocation.position, Quaternion.identity);

        bullet.GetComponent<Rigidbody>().velocity = ((target.transform.position + Vector3.up * yOffsetTarget) - projectileSpawnLocation.position).normalized * projectileSpeed;

    }
}
