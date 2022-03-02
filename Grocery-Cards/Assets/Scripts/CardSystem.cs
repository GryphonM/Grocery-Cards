using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//banking cards
public class CardSystem : MonoBehaviour
{
    public float cardHeightOffset;
    public Bag[] bags;
    public Card[] cards;
    public GameObject[] conveyorSnapPoints;
    public GameObject[] bagSnapPoints;
    [HideInInspector] public int currentBankableID = 999; //999 is value when cannot bank
    [HideInInspector] public int currentCardID = 999; //999 is value when no card selected
    [HideInInspector] public GameObject heldCard;
    private Camera mainCamera;
    public GameObject mouseCursor3d;
    private bool parentCardToMouse = false;
    private int layerMask = 1 << 6;
    private GameObject cardToParentGameObject;
    private float heightOfCard;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = FindObjectOfType<Camera>();
        for (int i = 0; i < conveyorSnapPoints.Length; i++)
        {
            if(cards[i] != null)
                cards[i].gameObject.transform.position = conveyorSnapPoints[i].transform.position;
        }
        for (int i = 0; i < bags.Length; i++)
        {
            if(bags[i] != null)
                bags[i].gameObject.transform.position = bagSnapPoints[i].transform.position;
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
                heightOfCard = raycastHitCards.transform.gameObject.transform.position.y + cardHeightOffset;
                parentCardToMouse = true;
            }
        }
        //pick up card
        if (parentCardToMouse == true && mouseCursor3d != null)
        {
            cardToParentGameObject.transform.position = new Vector3(mouseCursor3d.transform.position.x, heightOfCard, mouseCursor3d.transform.position.z);
        }
        //Release card
        if (Input.GetKeyUp(KeyCode.Mouse0) && parentCardToMouse != false)
        {
            if (cardToParentGameObject.tag == "card")
            {
                currentBankableID = 999;
                cardToParentGameObject.transform.position = conveyorSnapPoints[cardToParentGameObject.GetComponent<Card>().cardID].transform.position;
            }
            else if (cardToParentGameObject.tag == "bag")
            {
                currentBankableID = 999;
                cardToParentGameObject.transform.position = bagSnapPoints[cardToParentGameObject.GetComponent<Bag>().bagID].transform.position;
            }
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
                //update the bag / delete card
                bags[currentBankableID].bagSpace -= raycastHitCards.transform.GetComponent<Card>().cost;
                bags[currentBankableID].cardsDeposited += 1;
                Destroy(raycastHitCards.transform.gameObject);
            }            
        }


    }
}