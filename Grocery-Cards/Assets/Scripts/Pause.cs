using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] Vector3 pausePos;
    [SerializeField] Vector3 pauseRot;
    [SerializeField] float moveTime;
    [SerializeField] KeyCode pauseKey = KeyCode.Escape;
    CameraMovement camMove;
    
    // Start is called before the first frame update
    void Start()
    {
        camMove = GetComponent<CameraMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.paused && Input.GetKeyDown(pauseKey))
        {
            camMove.StartMoving(pausePos, pauseRot, moveTime);
            GameManager.paused = true;
        }
    }
}
