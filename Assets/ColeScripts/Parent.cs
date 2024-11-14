using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parent : MonoBehaviour
{
    [SerializeField] private int age;

    public virtual void Method()
    {
        print("this is from the parent");
    }
    
}
