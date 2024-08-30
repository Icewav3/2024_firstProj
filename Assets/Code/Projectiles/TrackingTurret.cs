using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingTurret : MonoBehaviour
{
    //TODO currently functions but need to think of a way to allow the aiming to lead targets
    private Transform target;

    public Rigidbody2D projectilePrefab;

    public float fireRate = .3f;
    private float currentFireTimer = 0;

    public float shootForce = 1f;
    public float bulletDeathTimer = 1f;

    void Start()
    {
        if (!projectilePrefab)
        {
            Debug.LogWarning("Please give the " + gameObject.name + " PlayerShooter script a projectile reference");
        }

        currentFireTimer = fireRate;
    }

    void Update()
    {
        if (target == null)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("EnemyUnit");

            if (enemies.Length == 0)
                return;

            float minDistance = float.MaxValue;
            GameObject nearestEnemy = null;

            foreach (GameObject enemy in enemies)
            {
                float distance = Vector2.Distance(transform.position, enemy.transform.position);

                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestEnemy = enemy;
                }
            }

            if (nearestEnemy != null)
            {
                target = nearestEnemy.transform;
            }
        }

        if (target)
        {
            // Vector to target;
            Vector2 targetVector = target.position - transform.position;
            Debug.DrawRay(transform.position, targetVector, Color.red, .5f);

            currentFireTimer += Time.deltaTime;
            if (currentFireTimer >= fireRate)
            {
                FireOneBullet(targetVector);
                currentFireTimer = 0;
            }
        }
    }

    private void FireOneBullet(Vector2 direction)
    {
        Rigidbody2D rg = Instantiate<Rigidbody2D>(projectilePrefab, transform.position, Quaternion.identity);

        rg.AddForce(direction.normalized * shootForce, ForceMode2D.Impulse);
        Destroy(rg.gameObject, bulletDeathTimer);
    }
}