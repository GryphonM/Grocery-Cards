using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClockInMachine : MonoBehaviour
{
    [HideInInspector] public int bagID = 4; // set bag id equal to its index in the bags array in CardSystem.cs
    CameraMovement mainCamera;
    MenuCards menuCardController;
    [SerializeField] Vector3 StartPosition;
    [SerializeField] Vector3 StartRotation;
    [SerializeField] float duration;

    void Start()
    {
        mainCamera = FindObjectOfType<Camera>().GetComponent<CameraMovement>();
        menuCardController = FindObjectOfType<MenuCards>();
    }
    void Update()
    {
        if (menuCardController.onClockIn && Input.GetKeyUp(KeyCode.Mouse0))
            StartGame();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Start")
        {
            menuCardController.onClockIn = true;
        }
    }

    public void StartGame()
    {
        mainCamera.StartMoving(StartPosition, StartRotation, duration);
        menuCardController.ReturnCard();
        GameManager.paused = false;

    }

    private void OnTriggerExit(Collider other)
    {
        menuCardController.onClockIn = false;
    }
}
