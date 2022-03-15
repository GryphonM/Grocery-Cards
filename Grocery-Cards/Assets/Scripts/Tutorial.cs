using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public GameObject tutorial;
    public GameObject posToMove;
    Vector3 posToReturn;
    bool hideAfterTime;
    // Start is called before the first frame update
    void Start()
    {
        posToReturn = tutorial.transform.position;
        tutorial.GetComponent<CameraMovement>().inCoroutine = false;
        tutorial.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(tutorial.GetComponent<CameraMovement>().inCoroutine == false && hideAfterTime == true)
        {
            tutorial.SetActive(false);
            hideAfterTime = false;
        }
    }

    public void ShowTutorial()
    {
        hideAfterTime = false;
        tutorial.SetActive(true);
        tutorial.GetComponent<CameraMovement>().StartMovingBetter(this.transform.position, posToMove.transform.rotation, 2);
    }
    public void HideTutorial()
    {
        tutorial.GetComponent<CameraMovement>().StartMovingBetter(posToReturn, posToMove.transform.rotation, 2);
        hideAfterTime = true;
    }
}
