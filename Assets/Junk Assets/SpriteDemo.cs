using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteDemo : MonoBehaviour
{
    public bool isButtonDown;

    public SpriteRenderer spriteRenderer;
    public Sprite sprite_Idle;
    public Sprite sprite_Kick;

    public Color idle_Color;
    // setting sprite renderer enabled
    public SpriteRenderer triangle;

    // setting gameobject enabled
    public GameObject triangle_LG;

    // Update is called once per frame
    void Update()
    {
        isButtonDown = Input.GetButton("Jump");

        // Button down = Kick
        if (isButtonDown) {
            spriteRenderer.sprite = sprite_Kick;
            spriteRenderer.color = Color.blue;
            triangle.enabled = true;
            triangle_LG.SetActive(true);
        } else {
            // Idle 
            spriteRenderer.sprite = sprite_Idle;
            spriteRenderer.color = idle_Color;
            triangle.enabled = false;
            triangle_LG.SetActive(false);
        }
    }
}
