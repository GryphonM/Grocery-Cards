using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InconspicusPixel : MonoBehaviour
{
    CameraMovement mainCamera;
    public GameObject posToMove;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = FindObjectOfType<Camera>().GetComponent<CameraMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToRoom()
    {
        mainCamera.StartMoving(posToMove.transform.position, posToMove.transform.rotation.eulerAngles, 3);
    }
}
