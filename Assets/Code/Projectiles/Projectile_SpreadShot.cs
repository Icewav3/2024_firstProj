using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Recursion Crash
/// <summary>
/// Why did this happen and how to fix it? 
/// </summary>
public class Projectile_SpreadShot : Projectile
{
    /* Projectile
    public Sprite defaultSprites;

    // 4 - play explosion
    public SpriteRenderer renderer;
    public Sprite[] explosionSprites;
    
    // 5
    // AudioSource

  
*/

    // Projectile didn't have a self ref for rigidbody.
    // we may want to refactor and move it there though. 

    public Rigidbody2D rigid;

    public float spawnDistance = 1f;
    public float spawnAngleInterval = 30;
    public float spawnedForce = 1f;

    public float explosionDelay = 1f;
    public float currentDelayTimer =1f;

    // can't use this Projectile or it will recursively fire forever, so use basic projectile for base. 
    public Projectile subSpawnProjectile;

   // Start is called before the first frame update
    void Start() {
        rigid = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update() {
        base.Update();
        currentDelayTimer += Time.deltaTime;
        if (currentDelayTimer > explosionDelay) {
            Explode();
        }
    }
    /*
    // 1
    private void OnCollisionEnter2D(Collision2D collision) {

    }
      */
    // 2
    void Explode() {
        base.Explode();
        
        rigid.simulated = false ; // disable the old rigidbody so it doesn't collide with the new ones. 

        // explode in a uniform burst to learn rotational trig. 

        Vector2 blueAxis = transform.forward;

        // straight up
        // Duplicate for testing
        // [ ] XXX
        // This will crash up and lock your system. why?
        //        Instantiate<Projectile>(subSpawnProjectile, transform.position + Vector3.up * spawnDistance * 2 , Quaternion.identity);

        Instantiate<Projectile>(subSpawnProjectile, transform.position + Vector3.up * spawnDistance * 2, Quaternion.identity);
        //     Vector2.up

        // find a point r distance from here (longer then the radius of the projectile)

        // break into x and y components
        // Y comes from Sin
        // Sin = O/H
        // the distance is teh hyp. solve for O (Y)

        int numOfProjectilesThatFit = (int)(360f / spawnAngleInterval);

        for (int i = 0; i < numOfProjectilesThatFit; i++) {
            float y = spawnDistance * Mathf.Sin(spawnAngleInterval * i);

            // Solve for A (X)
            float x = spawnDistance * Mathf.Cos(spawnAngleInterval * i);

            Vector2 spawnPosition = new Vector2(x, y);
            Debug.DrawRay(transform.position, spawnPosition, Color.yellow, 1f);
            // bonus - can i face the right direction? 
            Projectile p = Instantiate<Projectile>(subSpawnProjectile, spawnPosition * spawnDistance * 2, Quaternion.identity);

            // 
            p.transform.rotation = Quaternion.FromToRotation(p.transform.up, spawnPosition);
            Debug.DrawRay(p.transform.position, p.transform.up * 10f, Color.red, 1f);


            p.gameObject.name = p.gameObject.name + "(" + i + ")";
            // This is the vector frmo origin to the point on the unit circle.

            p.GetComponent<Rigidbody2D>().AddRelativeForce(spawnPosition * spawnedForce);


        }


        Destroy(gameObject);

        //   Mathf.Sin(30)
        // find the angle. 

        //Vector2.Ro

        //        Vector2.Angle




    }
}
