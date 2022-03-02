using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//banking cards
public class CardSystem : MonoBehaviour
{
    public Bag[] bags;
    public Card[] cards;
    public GameObject[] conveyorSnapPoints;
    [HideInInspector] public int currentBankableID = 999; //999 is value when cannot bank
    [HideInInspector] public int currentCardID = 999; //999 is value when no card selected
    [HideInInspector] public GameObject heldCard;
    private Camera mainCamera;
    public GameObject mouseCursor3d;
    private bool parentCardToMouse = false;
    private int cardToParent = 999;
    private int layerMask = 1 << 6;
    private GameObject cardToParentGameObject;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = FindObjectOfType<Camera>();
        for (int i = 0; i < conveyorSnapPoints.Length; i++)
        {
            cards[i].gameObject.transform.position = conveyorSnapPoints[i].transform.position;
        }
    }
    // Update is called once per frame
    void Update()
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
        if (parentCardToMouse == true && mouseCursor3d != null)
        {
            cardToParentGameObject.transform.position = new Vector3(mouseCursor3d.transform.position.x, raycastHitCards.transform.position.y, mouseCursor3d.transform.position.z);
        }
        if (Input.GetKeyUp(KeyCode.Mouse0) && parentCardToMouse != false)
        {            
            parentCardToMouse = false;
        }
        //parent card to point
        if (parentCardToMouse == true && currentCardID != 999)
        {
            cards[currentCardID].transform.position = mouseCursor3d.transform.position;
        }
        //banking cards into bags
        if (currentBankableID != 999 && Input.GetKeyUp(KeyCode.Mouse0) && heldCard.tag != "bag")
        {
            if (bags[currentBankableID].bagSpace - heldCard.GetComponent<Card>().cost < 0)
            {
                //make void the bag
            }
            else
            {
                bags[currentBankableID].bagSpace -= heldCard.GetComponent<Card>().cost;
                bags[currentBankableID].cardsDeposited += 1;
            }            
        }


    }
}