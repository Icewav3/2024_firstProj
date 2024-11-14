using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Child : Parent
{
    [SerializeField] private string stuff;

    public override void Method()
    {
        print("this is from the child class");
    }
}
