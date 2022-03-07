using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCard : MonoBehaviour
{
    [HideInInspector] public Vector3 StartPos;
    
    // Start is called before the first frame update
    void Start()
    {
        StartPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
