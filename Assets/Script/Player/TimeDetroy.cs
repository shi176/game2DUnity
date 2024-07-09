using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeDetroy : MonoBehaviour
{
    public float timeDetroy;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,timeDetroy);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
