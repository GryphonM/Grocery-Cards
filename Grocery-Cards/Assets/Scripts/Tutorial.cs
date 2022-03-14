using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public GameObject tutorial;
    public GameObject posToMove;
    Vector3 posToReturn;
    // Start is called before the first frame update
    void Start()
    {
        posToReturn = tutorial.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowTutorial()
    {       
        Debug.Log("Show tutorial");
        tutorial.GetComponent<CameraMovement>().StartMovingBetter(this.transform.position, posToMove.transform.rotation, 2,true);
    }
    public void HideTutorial()
    {
        tutorial.GetComponent<CameraMovement>().StartMovingBetter(posToReturn, posToMove.transform.rotation, 2);
    }
}
