using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookUp : MonoBehaviour
{
    [SerializeField] Vector3 upPos;
    [SerializeField] Vector3 upRot;
    [SerializeField] float upTime;
    [Space(10)]
    [SerializeField] Vector3 downPos;
    [SerializeField] Vector3 downRot;
    [SerializeField] float downTime;
    [Space(10)]
    [SerializeField] KeyCode upKey = KeyCode.W;
    [SerializeField] KeyCode downKey = KeyCode.S;

    CameraMovement camMove;
    
    // Start is called before the first frame update
    void Start()
    {
        camMove = GetComponent<CameraMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.paused)
        {
            if (Input.GetKeyDown(upKey))
                camMove.StartMoving(upPos, upRot, upTime);
            else if (Input.GetKeyDown(downKey))
                camMove.StartMoving(downPos, downRot, downTime);
        }
    }
}
