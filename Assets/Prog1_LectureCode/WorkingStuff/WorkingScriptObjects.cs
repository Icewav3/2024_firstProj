using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkingScriptObjects : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision) {
        Spaceship s = collision.gameObject.GetComponent<Spaceship>();

        if(s != null) {
            s.Damage(10);
        }


    }
}
