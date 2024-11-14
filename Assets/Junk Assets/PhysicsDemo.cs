using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsDemo : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if( spriteRenderer != null) {
            spriteRenderer.color = Color.red;
        } else {
            Debug.Log("No spriterenderer found");
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision) {

        spriteRenderer.color = Color.blue;

        //collision.gameObject.SetActive(false);
        Debug.Log("We hit a object: " + collision.gameObject.name);

        SpriteRenderer otherSprite = collision.gameObject.GetComponent<SpriteRenderer>();
        if(otherSprite != null) {
            otherSprite.color = Color.yellow;
        }

    }

    private void OnCollisionExit2D(Collision2D collision) {
        spriteRenderer.color = Color.green;
    }

    private void OnCollisionStay2D(Collision2D collision) {
        
    }
}
