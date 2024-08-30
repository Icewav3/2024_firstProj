using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    
    public Sprite defaultSprites;

    // 4 - play explosion
    public SpriteRenderer renderer;
    public Sprite[] explosionSprites;

    // 5
    // AudioSource

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Update()
    {
        
    }

    // 1
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    // 2
    protected void Explode()
    {

    }
}
