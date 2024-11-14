using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

// A very simple 1D Automata

public class Automata_1D : MonoBehaviour
{


    [Header("Basic Settings")]
    
    public int arrayWidth = 10;
    [Header("Time Between Step Updates")]
    // Timer for slowing down the simulation
    public float timeBetweenSteps = .01f;
    private float currentTimer = 0;

    [Header("Starting Settings - Note they will overwrite each other")]
    public bool simpleSeed = false;
    public bool randomSeed = false;
    public bool randomStart = false;

    // To DO : Convert ruleset to use something more like this. 
    // See Wolfram Rules
    // public bool[] ruleSet = new bool[8] {0,1,1,0,1,1,1,0};


    [Header("Current State")]
    // Base Array is the current "state"  
    public int[] baseArray;
    // Next Gen is a temporary swap space to store the next calculated step
    public int[] nextGeneration;

    // Q: Why are we not calculating it directly in the baseArray? 

    // tracks how many generations have occured
    private int currentStep = 0;
    private List<GameObject> _jim_list = new List<GameObject>();

    [SerializeField] private GameObject _prefabbbb;

    void Start()
    {
        // Create the arrays
        baseArray = new int[arrayWidth];
        nextGeneration = new int[arrayWidth];

        // --- Startup Settings ---
        // Set the midpoint cell to be true
        if(simpleSeed) {
            baseArray[arrayWidth/2] = 1;
        }

        // Set a random cell value to be true
        if(randomSeed) {
            int randomIndex = UnityEngine.Random.Range(0, arrayWidth);
            baseArray[randomIndex] = 1;
        }

        // Randomize all cells
        if(randomStart) {
            for(int i = 0; i < arrayWidth; i++) {
                if (UnityEngine.Random.Range(0, 2) == 1) {
                    baseArray[i] = 1;
                } else{
                    baseArray[i] = 0;
                }
            }
        }


        // First Line output 
        PrintCurrentGeneration();

    }

    // Update is called once per frame
    void Update() {
        // Delay n seconds then call StepUpdate. 
        // This allows us to control the simulation speed and order. 
        currentTimer += Time.deltaTime;
        if(currentTimer > timeBetweenSteps) {
            currentTimer = currentTimer - timeBetweenSteps;
            StepUpdate();
        }
    }

    // This is the simulation update loop
    void StepUpdate() {

        // Copy edges for now, add wrapping later
        nextGeneration[0] = baseArray[0]; // not wrapping yet
        nextGeneration[baseArray.Length-1] = baseArray[baseArray.Length-1];

        // Evaluate middle
        for (int i = 1; i < baseArray.Length-1; i++) {
            nextGeneration[i] = EvaluateNextGeneration(i - 1, i, i + 1);
        }


        // Dont' forget to update self!
        // Note we are continually losing the last array and creating a new one, so not the most efficient solution. 
        baseArray = nextGeneration;
        nextGeneration = new int[arrayWidth];

        currentStep++;

        // Output - This could be improved. 
        PrintCurrentGeneration();
    }

    // Right now prints to Debug. Could visualize this. 
    public void PrintCurrentGeneration() {
        //ideal naming convention
        foreach (var jim in _jim_list)
        {
            GameObject.Destroy(jim);
        }
        _jim_list.Clear();
        
        for (int i = 0; i < baseArray.Length; i++) {
            var jim = GameObject.Instantiate(_prefabbbb, new Vector3(i, 0f, 0f), quaternion.identity);
            _jim_list.Add(jim);
            print(baseArray[i]);
            jim.GetComponent<SpriteRenderer>().enabled = (baseArray[i] == 1);
        }
    }

    // The left , current, and right indexes of the base generation (for spot i in next)
    int EvaluateNextGeneration(int left, int mid, int right) {
        int returnValue = 0;

        // Code as an 8 bit binary number
        // eg 01011011
        // This is why there is 256 Wolfram Rules

        // Q Which rule is this? (Serpinski Triangle)

        // Rule 110
        // Rule set (Brute Force Edition)
        if(baseArray[left] == 0 && baseArray[mid] == 0 && baseArray[right] == 0) { returnValue = 0; } //
        if (baseArray[left] == 0 && baseArray[mid] == 0 && baseArray[right] == 1) { returnValue = 1; }
        if (baseArray[left] == 0 && baseArray[mid] == 1 && baseArray[right] == 0) { returnValue = 1; }
        if (baseArray[left] == 0 && baseArray[mid] == 1 && baseArray[right] == 1) { returnValue = 1; }
        if (baseArray[left] == 1 && baseArray[mid] == 0 && baseArray[right] == 0) { returnValue = 0; } //
        if (baseArray[left] == 1 && baseArray[mid] == 0 && baseArray[right] == 1) { returnValue = 1; }
        if (baseArray[left] == 1 && baseArray[mid] == 1 && baseArray[right] == 0) { returnValue = 1; }
        if (baseArray[left] == 1 && baseArray[mid] == 1 && baseArray[right] == 1) { returnValue = 0; } //
        return returnValue;
    }
}
