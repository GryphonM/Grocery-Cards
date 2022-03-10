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
    [SerializeField] AudioClip pausedMenu;
    
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
            GameObject.FindGameObjectWithTag("Jukebox").GetComponent<AudioSource>().clip = pausedMenu;
            GameObject.FindGameObjectWithTag("Jukebox").GetComponent<AudioSource>().Play();
            GameManager.paused = true;
        }
    }
}
