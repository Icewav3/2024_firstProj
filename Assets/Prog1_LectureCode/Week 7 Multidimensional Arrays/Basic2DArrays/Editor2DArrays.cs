using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Editor2DArrays : MonoBehaviour
{
    public ArrayClass[] arrayOfArrays = new ArrayClass[5];

    
    public void Start() {
        // This is 2D arrays now
        for (int i = 0; i < arrayOfArrays.Length; i++) {
            // need to create these
            arrayOfArrays[i] = new ArrayClass();
            arrayOfArrays[i].intArray = new int[4];

            for (int j = 0; j < arrayOfArrays[i].intArray.Length; j++) {
                arrayOfArrays[i].intArray[j] = i * arrayOfArrays.Length + j;
            }
        }
    }
}
