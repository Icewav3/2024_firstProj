using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SimplestCollider2D : MonoBehaviour
{

    public UnityEvent OnCollisionEvent = new UnityEvent();

    private void OnCollisionEnter2D(Collision2D collision) {
        OnCollisionEvent.Invoke();
    }
}
