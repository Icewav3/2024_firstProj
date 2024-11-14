using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

    public bool isMoving = true;

    public Rigidbody2D body;
    public float xForce;

    public float moveRange = 5f;
    private float offsetFromStart = 0;
    private bool movingRight = true;
    private Vector3 startingLocation;

    // Start is called before the first frame update
    void Start() {
        body = GetComponent<Rigidbody2D>();
        startingLocation = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate() {
        // skip this method if we are not moving. 
        if (!isMoving)
            return;


        offsetFromStart = startingLocation.x - transform.position.x;
        if (Mathf.Abs(offsetFromStart) > moveRange) {
            if (offsetFromStart < 0) {
                // Hit the right boundary
                movingRight = false;
            } else {
                movingRight = true;
            }
        }

        if (movingRight) {
            body.AddForce(new Vector2(xForce, 0));
        } else {
            body.AddForce(new Vector2(-xForce, 0));
        }



    }
}
