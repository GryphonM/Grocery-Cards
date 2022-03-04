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
    private Camera mainCamera;
    public GameObject mouseCursor3d;
    private bool parentCardToMouse = false;
    private int layerMask = 1 << 6;
    private GameObject cardToParentGameObject;
    private float heightOfCard;
    private Customer activeCustomer;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = FindObjectOfType<Camera>();
        activeCustomer = FindObjectOfType<Customer>();
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
    void moveConvayerBelt()
    {
        for (int i = cards.Length - 1; i > 0; i--)
        {
            if (cards[i - 1] == null && i - 1 >= 0 && cards[i] != null)
            {
                cards[i - 1] = cards[i];
                cards[i] = null;
                cards[i - 1].cardID = i - 1;
                cards[i - 1].gameObject.transform.position = conveyorSnapPoints[i - 1].transform.position;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        moveConvayerBelt();
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
        //parent card to point
        if (parentCardToMouse == true && currentCardID != 999)
        {
            cards[currentCardID].transform.position = mouseCursor3d.transform.position;
        }
        //banking cards into bags
        if (currentBankableID != 999 && Input.GetKeyUp(KeyCode.Mouse0) && cardToParentGameObject.tag != "bag" && currentBankableID != 4)
        {
            if (currentBankableID == 998)
            {
                Debug.Log("Wrong Card");
                activeCustomer.WrongItem(cardToParentGameObject.GetComponent<Card>());
            }
            else if (bags[currentBankableID].bagSpace - cardToParentGameObject.GetComponent<Card>().cost < 0)
            {
                //make void the bag
                Debug.Log("Bag Void");
            }
            else
            {
                //update the bag / delete card
                bags[currentBankableID].bagSpace -= cardToParentGameObject.GetComponent<Card>().cost;
                bags[currentBankableID].cardsDeposited += 1;
                Destroy(cardToParentGameObject.transform.gameObject);
            }           
        }
        if (currentBankableID == 4 && Input.GetKeyUp(KeyCode.Mouse0) && cardToParentGameObject.tag == "bag")
        {
            //make bag connect to the satifaction system
            activeCustomer.BagDeposit(cardToParentGameObject.GetComponent<Bag>());
            Destroy(cardToParentGameObject.transform.gameObject);
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
    }
}