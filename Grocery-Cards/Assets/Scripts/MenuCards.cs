using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCards : MonoBehaviour
{
    Camera mainCamera;
    int layerMask = 1 << 6;
    [SerializeField] GameObject mouseCursor3d;
    bool parentCardToMouse = false;
    GameObject cardToParentGameObject;
    
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = FindObjectOfType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.paused)
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
            }

            //Release card
            if (Input.GetKeyUp(KeyCode.Mouse0) && parentCardToMouse != false)
            {
                cardToParentGameObject.transform.position = cardToParentGameObject.GetComponent<StartCard>().StartPos;
                parentCardToMouse = false;
            }
        }
    }
}
