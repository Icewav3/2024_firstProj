using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject explosionGo;

    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision) {
        // hit an enemy
        // might not need to if enemy destroys self
        
        if (explosionGo != null) {
            Instantiate(explosionGo);
        }
        Destroy(gameObject);

    }

    private void ExplodeMe() {

    }
}
