using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {
    public Projectile prefabReference;

    public List<Projectile> currentActiveProjectiles = new List<Projectile>();

    public Vector2 fireForce = new Vector2(0, 1f);


    public bool DEBUG_MODE = false;

    public void Fire()
    {
        Projectile p = Instantiate<Projectile>(prefabReference, transform);
        Rigidbody2D rigid = p.GetComponent<Rigidbody2D>();

        if (rigid != null) {
            // ok but this mode of force is for applying a force over time, continiously
            //rigid.AddRelativeForce(fireForce);
            // This is the same as the above. The above line has a default parameter for ForceMode. 
            // rigid.AddRelativeForce(fireForce, ForceMode2D.Force;

            // Use this to apply the entire force this frame. 
            rigid.AddRelativeForce(fireForce, ForceMode2D.Impulse);


        } else {
            Debug.LogWarning("No rigidbody found on projectile");
        }

        currentActiveProjectiles.Add(p);

        if (DEBUG_MODE){
            Debug.Log("Spawned a Projectile: " + prefabReference.gameObject.name);
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
