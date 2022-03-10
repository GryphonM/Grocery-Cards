using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InconspicusPixel : MonoBehaviour
{
    CameraMovement mainCamera;
    public GameObject posToMove;
    public float timeToCrash;
    public bool CrashGame;
    float time = 0;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = FindObjectOfType<Camera>().GetComponent<CameraMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CrashGame == true)
        {
            time += Time.deltaTime;
            if (time >= timeToCrash)
            {
                Debug.Log("force crash");
                Application.Quit();
            }
        }
    }

    public void GoToRoom()
    {
        mainCamera.StartMoving(posToMove.transform.position, posToMove.transform.rotation.eulerAngles, 3);
        CrashGame = true;
    }
}
