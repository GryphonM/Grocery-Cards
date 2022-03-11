using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InconspicusPixel : MonoBehaviour
{
    CameraMovement mainCamera;
    public GameObject posToMove;
    public Vector3 closeUpPos;
    public GameObject Jukebox;
    public AudioClip bossMusic;
    public float timeToCrash;
    public bool CrashGame;
    public bool moveCloser;
    float time = 0;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = FindObjectOfType<Camera>().GetComponent<CameraMovement>();
        Jukebox = GameObject.FindGameObjectWithTag("Jukebox");
    }

    // Update is called once per frame
    void Update()
    {
        if (CrashGame == true)
        {
            time += Time.deltaTime;
            if (moveCloser && !mainCamera.inCoroutine)
                mainCamera.StartMoving(closeUpPos, posToMove.transform.rotation.eulerAngles, timeToCrash - time);

            if (time >= timeToCrash)
            {
                Debug.Log("force crash");
                if (Application.platform == RuntimePlatform.WindowsPlayer)
                    Application.Quit();
                else if (Application.platform == RuntimePlatform.WebGLPlayer)
                    SceneManager.LoadScene(0);
            }
        }
    }

    public void GoToRoom()
    {
        mainCamera.StartMoving(posToMove.transform.position, posToMove.transform.rotation.eulerAngles, 3);
        Jukebox.GetComponent<AudioSource>().clip = bossMusic;
        Jukebox.GetComponent<AudioSource>().Play();
        moveCloser = true;
        CrashGame = true;
    }
}
