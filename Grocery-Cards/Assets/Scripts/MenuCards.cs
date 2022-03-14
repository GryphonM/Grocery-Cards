using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCards : MonoBehaviour
{
    Camera mainCamera;
    int layerMask = 1 << 6;
    [SerializeField] GameObject mouseCursor3d;
    [SerializeField] int upQueue = 3001;
    int downQueue = 3000;
    bool parentCardToMouse = false;
    GameObject cardToParentGameObject;
    CustomerSpawner custSpawn;
    
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = FindObjectOfType<Camera>();
        custSpawn = FindObjectOfType<CustomerSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.paused && !custSpawn.fired)
        {
            //getting a point at the mouse
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit, Mathf.Infinity, ~layerMask))
            {
                mouseCursor3d.transform.position = raycastHit.point;
            }
            if (Physics.Raycast(ray, out RaycastHit raycastHitCards, Mathf.Infinity, layerMask))
            {
                if (Input.GetKeyDown(KeyCode.Mouse0) && parentCardToMouse != true)
                {
                    cardToParentGameObject = raycastHitCards.transform.gameObject;
                    parentCardToMouse = true;
                }
            }

            //pick up card
            if (parentCardToMouse == true && mouseCursor3d != null)
            {
                cardToParentGameObject.transform.position = new Vector3(cardToParentGameObject.transform.position.x, mouseCursor3d.transform.position.y, mouseCursor3d.transform.position.z);
                cardToParentGameObject.GetComponent<MeshRenderer>().material.renderQueue = upQueue;
                downQueue = cardToParentGameObject.GetComponent<MeshRenderer>().material.renderQueue;
            }

            //Release card
            if (Input.GetKeyUp(KeyCode.Mouse0) && parentCardToMouse != false)
            {
                cardToParentGameObject.transform.position = cardToParentGameObject.GetComponent<StartCard>().StartPos;
                cardToParentGameObject.GetComponent<MeshRenderer>().material.renderQueue = downQueue;
                parentCardToMouse = false;
            }
        }
    }
}
