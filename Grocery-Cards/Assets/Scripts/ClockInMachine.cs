using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClockInMachine : MonoBehaviour
{
    [HideInInspector] public int bagID = 4; // set bag id equal to its index in the bags array in CardSystem.cs
    CameraMovement mainCamera;
    MenuCards menuCardController;
    enum Action {Start, Quit, Null};
    Action currentAction = Action.Null;
    [SerializeField] GameObject gameUI;
    [Space(10)]
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
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            switch(currentAction)
            {
                case Action.Start:
                    StartGame();
                    break;
                case Action.Quit:
                    Debug.Log("Quit Game");
                    Application.Quit();
                    break;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Start":
                currentAction = Action.Start;
                break;
            case "Quit":
                currentAction = Action.Quit;
                break;
            default:
                currentAction = Action.Null;
                break;
        }
    }

    public void StartGame()
    {
        mainCamera.StartMoving(StartPosition, StartRotation, duration);
        GameObject.FindGameObjectWithTag("Conveyor").GetComponent<AudioSource>().Play();
        gameUI.SetActive(true);
        GameManager.paused = false;
    }

    private void OnTriggerExit(Collider other)
    {
        currentAction = Action.Null;
    }
}
