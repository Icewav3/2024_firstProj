using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;



public class Basic2DArrays : MonoBehaviour
{
    // Array of ints
    public int[] basicArray = new int[5]; // C# style since we are creating an object but not instantiating it. 

    // Array of objects of type Array of Ints
    public int[][] basicArrayOfArrays = new int[5][]; // sub arrays are not created yet. 

    // "True" 2D array
    public int[,] basic2DArray = new int[5, 4];
    

    // Start is called before the first frame update
    void Start()
    {

        // Iterate the array creating the inner array, which was not initialized 
        for(int i = 0; i < 5; i++) {
            basicArrayOfArrays[i] = new int[4];
        }

        // Many techniques for this but I like to use the i value inthe inner loop,
        // as it handles jagged arrays as well without changes. 
        for(int i = 0; i < basicArrayOfArrays.Length; i++) {
            for(int j = 0; j < basicArrayOfArrays[i].Length; j++) {
                basicArrayOfArrays[i][j] = i * basicArrayOfArrays.Length + j;
            }
        }

        // This is 2D arrays now
        for (int i = 0; i < basic2DArray.GetLength(0); i++) {
            for (int j = 0; j < basic2DArray.GetLength(1); j++) {
                basic2DArray[i,j] = i * basic2DArray.GetLength(0) + j;
            }
        }

        PrintArrayOfArray();
        Print2DArray();

    }

    public void PrintArrayOfArray() {
        
        Debug.Log("Array of Arrays");

        for(int i =0 ; i < basicArrayOfArrays.Length;i++) {
             for(int j = 0; j < basicArrayOfArrays[i].Length; j++) {
                Debug.Log("[" + i + "," + j + "] = " + basicArrayOfArrays[i][j]);
            }
        }
    }

    public void Print2DArray() {

        Debug.Log("2D Arrays");

        for (int i = 0; i < basic2DArray.GetLength(0); i++) {
            for (int j = 0; j < basic2DArray.GetLength(1); j++) {
                Debug.Log("[" + i + "," + j + "] = " + basic2DArray[i,j]);
            }
        }
    }
}
