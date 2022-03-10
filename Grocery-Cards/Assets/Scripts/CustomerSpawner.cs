using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    [SerializeField] Vector3 SpawnPos;
    [SerializeField] float EnterTime;
    [Space(10)]
    [SerializeField] Vector3 FinalPos;
    [SerializeField] float LeaveTime;
    [Space(10)]
    [SerializeField] Vector3 LookUpCameraPos;
    [SerializeField] Vector3 LookUpCameraRot;
    [SerializeField] float CameraEndTime;
    [SerializeField] GameObject Manager;
    GameObject gameCam;
    [Space(10)]
    [SerializeField] Material[] NPCMaterials;
    [Tooltip("Goes From Easiest Customer to Hardest Customer")]
    [SerializeField] Customer[] Customers;
    int customerIndex = 0;
    GameObject currentCustomer;
    [HideInInspector] public Customer currentCustomerScript;
    CardSystem cardSyst;

    [HideInInspector] public bool betweenCustomers = true;
    [HideInInspector] public bool allCards;
    [HideInInspector] public bool cardsDone;
    bool characterLeft = false;
    [HideInInspector] public bool fired;
    bool spawnManager = true;
    
    // Start is called before the first frame update
    void Start()
    {
        betweenCustomers = true;
        gameCam = FindObjectOfType<Camera>().gameObject;
        cardSyst = FindObjectOfType<CardSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.paused)
        {
            // Get Number of Cards in Play
            int cardNumber = 0;
            foreach (Card card in cardSyst.cards)
            {
                if (card != null)
                    cardNumber++;
            }
            // New Customer
            if (currentCustomer == null && betweenCustomers)
            {
                currentCustomer = Instantiate(Customers[customerIndex].gameObject);
                currentCustomer.transform.position = SpawnPos;
                currentCustomer.transform.GetChild(0).GetComponent<MeshRenderer>().material = NPCMaterials[Random.Range(0, NPCMaterials.Length)];
                currentCustomer.GetComponent<CameraMovement>().StartMoving(Vector3.zero, Vector3.zero, EnterTime);
                currentCustomerScript = currentCustomer.GetComponent<Customer>();
                currentCustomerScript.totalCards = Random.Range(currentCustomerScript.minTotalCards, currentCustomerScript.maxTotalCards);
                FindObjectOfType<CardSpawner>().timeDelayBetweenCards = currentCustomerScript.CardSpawn;
                betweenCustomers = false;
                allCards = false;
                cardsDone = false;
                characterLeft = false;
            }
            else if (currentCustomerScript.cardCount + cardNumber >= currentCustomerScript.totalCards)
            {
                allCards = true;
                if (cardNumber == 0)
                {
                    bool bagsLeft = false;
                    cardsDone = true;
                    if (!characterLeft)
                    {
                        currentCustomer.GetComponent<CameraMovement>().StartMoving(FinalPos, Vector3.zero, LeaveTime, true);
                        characterLeft = true;
                    }
                    foreach (Bag bag in FindObjectsOfType<Bag>())
                    {
                        if (bag.cardsDeposited > 0)
                            bagsLeft = true;
                    }
                    if (!bagsLeft)
                    {
                        betweenCustomers = true;
                        customerIndex++;
                        if (customerIndex >= Customers.Length)
                            customerIndex = Customers.Length - 1;
                    }
                }
            }

            if (currentCustomerScript.Satisfaction <= 0 && spawnManager)
            {
                allCards = true;
                cardsDone = true;
                GameManager.paused = true;
                fired = true;
                currentCustomer.GetComponent<CameraMovement>().StartMoving(FinalPos, Vector3.zero, LeaveTime, true);
                gameCam.GetComponent<CameraMovement>().StartMoving(LookUpCameraPos, LookUpCameraRot, CameraEndTime);
                GameObject manager = Instantiate(Manager);
                manager.transform.position = SpawnPos;
                manager.GetComponent<CameraMovement>().StartMoving(Vector3.zero, Vector3.zero, EnterTime);
                spawnManager = false;
            }
        }
    }
}
