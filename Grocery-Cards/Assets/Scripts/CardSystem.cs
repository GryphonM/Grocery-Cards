using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
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
    private CustomerSpawner custSpawn;
    public Customer customer;
    [SerializeField] int upQueue = 3001;
    [SerializeField] int upOrder = 1;
    int normalQueue;
    int normalOrder;
    AudioClips myClips;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = FindObjectOfType<Camera>();
        custSpawn = FindObjectOfType<CustomerSpawner>();
        myClips = GetComponent<AudioClips>();
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
                //cards[i - 1].gameObject.transform.position = conveyorSnapPoints[i - 1].transform.position;   
                cards[i - 1].GetComponent<Card>().positionToMoveTo = conveyorSnapPoints[i - 1].gameObject;
                cards[i - 1].GetComponent<Card>().startMoving();
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (!GameManager.paused && !custSpawn.betweenCustomers)
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
                    cardToParentGameObject.GetComponent<RandomContainer>().clips = myClips.cardPickupClips;
                    cardToParentGameObject.GetComponent<RandomContainer>().PlaySound(false);
                    heightOfCard = raycastHitCards.transform.gameObject.transform.position.y + cardHeightOffset;
                    normalQueue = cardToParentGameObject.GetComponent<MeshRenderer>().material.renderQueue;
                    normalOrder = cardToParentGameObject.GetComponentInChildren<TextMeshPro>().sortingOrder;
                    parentCardToMouse = true;
                }
            }
            //pick up card
            if (parentCardToMouse == true && mouseCursor3d != null)
            {
                cardToParentGameObject.transform.position = new Vector3(mouseCursor3d.transform.position.x, heightOfCard, mouseCursor3d.transform.position.z);
                cardToParentGameObject.GetComponent<MeshRenderer>().material.renderQueue = upQueue;
                cardToParentGameObject.GetComponentInChildren<TextMeshPro>().sortingOrder = upOrder;
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
                    custSpawn.currentCustomerScript.WrongItem(cardToParentGameObject.GetComponent<Card>());
                }
                else if (bags[currentBankableID].bagSpace - cardToParentGameObject.GetComponent<Card>().cost < 0)
                {
                    //make void the bag
                }
                else
                {
                    //update the bag / delete card
                    cardToParentGameObject.GetComponent<RandomContainer>().clips = myClips.cardDepositClips;
                    cardToParentGameObject.GetComponent<RandomContainer>().PlaySound(false);
                    bags[currentBankableID].bagSpace -= cardToParentGameObject.GetComponent<Card>().cost;
                    bags[currentBankableID].cardsDeposited += 1;
                    custSpawn.currentCustomerScript.cardCount++;
                    cardToParentGameObject.GetComponent<Shrink>().shrink = true;
                    cards[cardToParentGameObject.GetComponent<Card>().cardID] = null;
                    Destroy(cardToParentGameObject.GetComponent<MeshCollider>());
                    cardToParentGameObject = null;
                    parentCardToMouse = false;
                    currentBankableID = 999;
                    currentCardID = 999;
                }
            }
            if (currentBankableID == 4 && Input.GetKeyUp(KeyCode.Mouse0) && cardToParentGameObject.tag == "bag")
            {
                //make bag connect to the satifaction system
                if (custSpawn.currentCustomerScript != null)
                    custSpawn.currentCustomerScript.BagDeposit(cardToParentGameObject.GetComponent<Bag>());
                cardToParentGameObject.GetComponent<RandomContainer>().clips = myClips.bagDepositClips;
                cardToParentGameObject.GetComponent<RandomContainer>().PlaySound(false);
                cardToParentGameObject.GetComponent<CameraMovement>().StartMovingBetter(new Vector3(cardToParentGameObject.transform.position.x + 10, cardToParentGameObject.transform.position.y, cardToParentGameObject.transform.position.z), cardToParentGameObject.transform.rotation, .25f, true);
                bags[cardToParentGameObject.GetComponent<Bag>().bagID] = null;
                //Destroy(cardToParentGameObject.transform.gameObject);
            }
            //Release card
            if (Input.GetKeyUp(KeyCode.Mouse0) && parentCardToMouse != false)
            {
                if (cardToParentGameObject.tag == "card")
                {
                    currentBankableID = 999;
                    cardToParentGameObject.transform.position = conveyorSnapPoints[cardToParentGameObject.GetComponent<Card>().cardID].transform.position;
                    cardToParentGameObject.GetComponent<Card>().positionToMoveTo = conveyorSnapPoints[cardToParentGameObject.GetComponent<Card>().cardID].gameObject;
                }
                else if (cardToParentGameObject.tag == "bag")
                {
                    currentBankableID = 999;
                    cardToParentGameObject.transform.position = bagSnapPoints[cardToParentGameObject.GetComponent<Bag>().bagID].transform.position;
                }
                parentCardToMouse = false;
                cardToParentGameObject.GetComponent<MeshRenderer>().material.renderQueue = normalQueue;
                cardToParentGameObject.GetComponentInChildren<TextMeshPro>().sortingOrder = normalOrder;
            }
        }
    }
}