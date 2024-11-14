using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PhysicsPlayers : MonoBehaviour
{
    // Cached input
    public Vector2 input;

    public Rigidbody2D body;

    public float moveForce = 1f;
    public float rotationForce = 1f;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        // body.AddForce(input);
        body.AddRelativeForce(new Vector2(0, input.y) * moveForce);
        body.AddTorque(input.x * -1f * rotationForce);
    }
}
