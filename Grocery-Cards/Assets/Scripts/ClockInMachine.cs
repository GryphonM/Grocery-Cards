using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClockInMachine : MonoBehaviour
{
    [HideInInspector] public int bagID = 4; // set bag id equal to its index in the bags array in CardSystem.cs
    CameraMovement camera;
    [SerializeField] Vector3 FinalPosition;
    [SerializeField] Vector3 FinalRotation;
    [SerializeField] float duration;

    private void Start()
    {
        camera = FindObjectOfType<Camera>().GetComponent<CameraMovement>();
    }
    private void Update()
    {

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Start" && Input.GetKeyUp(KeyCode.Mouse0))
        {
            camera.StartMoving(FinalPosition, FinalRotation, duration);
        }
    }
    private void OnTriggerExit(Collider other)
    {

    }
}
