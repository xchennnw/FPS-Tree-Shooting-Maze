using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class greenArea : MonoBehaviour
{
    // This is the code for the floating green panel in the game,
    // That panel brings player up, from the end of maze to the winning side of canyon
    
    public float amplitude = 0.5f;
    public float frequency = 1f;

    
    Vector3 posOffset = new Vector3();
    Vector3 tempPos = new Vector3();

    void Start()
    {       
        posOffset = transform.position;
    }

    void Update()
    {
       
        // Float up/down with a Sin()
        tempPos = posOffset;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;
        transform.position = tempPos;
    }
}
