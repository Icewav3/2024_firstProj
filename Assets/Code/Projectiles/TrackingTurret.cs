using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingTurret : MonoBehaviour
{
    [SerializeField] private int cooldownTime = 10;
    [SerializeField] private int activeTime = 5;
    private bool active;
    private bool abilityUseButton;
    private Transform target;
    private float cooldownTimer = 0f;
    private float ActiveTimer = 0f;

    public Rigidbody2D projectilePrefab;

    public float fireRate = .1f;
    private float currentFireTimer = 0;

    public float shootForce = 1f;
    public float bulletDeathTimer = 1f;

    void Start()
    {
        if (!projectilePrefab)
        {
            Debug.LogWarning("Please give the " + gameObject.name + " PlayerShooter script a projectile reference");
        }

        active = false;
        currentFireTimer = fireRate;
        cooldownTimer = cooldownTime; //set ability active when game starts
    }

    void Update()
    {
        if (!active) //if inactive do checks
        {
            cooldownTimer += Time.deltaTime; 
            if (cooldownTimer < cooldownTime) //if ability isnt cooldown yet return
            {
                print("remaining cooldown: "+(cooldownTime-cooldownTimer));
                return;
            }
            abilityUseButton = Input.GetKeyDown("1");
            if (!abilityUseButton)
            {
                return;
            }

            cooldownTimer = 0f;
            active = true;
        }
        ActiveTimer += Time.deltaTime;
        if (ActiveTimer > activeTime) //if time up, reset ability
        {
            active = false;
            ActiveTimer = 0f;
            return;
        }
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
        Debug.DrawRay(this.transform.position, direction.normalized*100, Color.green, 1);

        rg.AddForce(direction.normalized * shootForce, ForceMode2D.Impulse);
        Destroy(rg.gameObject, bulletDeathTimer);
    }
}