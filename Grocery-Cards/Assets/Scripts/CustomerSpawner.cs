using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    [SerializeField] Vector3 SpawnPos;
    [SerializeField] float EnterTime;
    [SerializeField] Vector3 FinalPos;
    [SerializeField] float LeaveTime;
    [SerializeField] Material[] NPCMaterials;
    [Tooltip("Goes From Easiest Customer to Hardest Customer")]
    [SerializeField] Customer[] Customers;
    int customerIndex = 0;
    GameObject currentCustomer;
    [HideInInspector] public Customer currentCustomerScript;

    [HideInInspector] public bool betweenCustomers;
    [HideInInspector] public bool allCards;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.paused)
        {
            // Get Number of Cards in Play
            int cardNumber = GameObject.FindGameObjectsWithTag("card").Length;
            // New Customer
            if (currentCustomer == null)
            {
                currentCustomer = Instantiate(Customers[customerIndex].gameObject);
                currentCustomer.transform.position = SpawnPos;
                currentCustomer.transform.GetChild(0).GetComponent<MeshRenderer>().material = NPCMaterials[Random.Range(0, 5)];
                currentCustomer.GetComponent<CameraMovement>().StartMoving(Vector3.zero, Vector3.zero, EnterTime);
                currentCustomerScript = currentCustomer.GetComponent<Customer>();
                betweenCustomers = false;
                allCards = false;
            }
            else if (currentCustomerScript.cardCount + cardNumber >= currentCustomerScript.totalCards)
            {
                allCards = true;
                if (cardNumber == 0)
                {
                    bool bagsLeft = false;
                    foreach (Bag bag in FindObjectsOfType<Bag>())
                    {
                        if (bag.cardsDeposited > 0)
                            bagsLeft = true;
                    }
                    if (!bagsLeft)
                    {
                        betweenCustomers = true;
                        currentCustomer.GetComponent<CameraMovement>().StartMoving(FinalPos, Vector3.zero, LeaveTime, true);
                    }
                }
            }
        }
    }
}
