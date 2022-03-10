using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    [Space(5)]
    [SerializeField] float dialogueTime;
    [SerializeField] GameObject Manager;
    GameObject manager;
    GameObject gameCam;
    bool playedFired = false;
    [Space(10)]
    [SerializeField] Vector3 WinLookUpPos;
    [SerializeField] Vector3 WinLookUpRot;
    [SerializeField] float WinLookUpTime;
    [Space(5)]
    [SerializeField] Vector3 WinLeavePos;
    [SerializeField] Vector3 WinLeaveRot;
    [SerializeField] float WinLeaveTime;
    [Space(5)]
    [SerializeField] float blackoutTime;
    [SerializeField] GameObject StoreLeave;
    [Space(10)]
    [SerializeField] AudioClip loseClip;
    [SerializeField] AudioClip winClip;
    RandomContainer myAudio;
    [Space(10)]
    [SerializeField] Material[] NPCMaterials;
    int lastMaterial = -1;
    [Tooltip("Goes From Easiest Customer to Hardest Customer")]
    [SerializeField] Customer[] Customers;
    int customerIndex = 0;
    GameObject currentCustomer;
    [HideInInspector] public Customer currentCustomerScript;
    CardSystem cardSyst;
    Timer gameTimer;

    [HideInInspector] public bool betweenCustomers = true;
    [HideInInspector] public bool allCards;
    [HideInInspector] public bool cardsDone;
    bool characterLeft = false;
    [HideInInspector] public bool fired;
    bool spawnManager = true;
    bool gameWon = false;
    bool leftStore = false;
    
    // Start is called before the first frame update
    void Start()
    {
        betweenCustomers = true;
        gameCam = FindObjectOfType<Camera>().gameObject;
        cardSyst = FindObjectOfType<CardSystem>();
        gameTimer = FindObjectOfType<Timer>(true);
        manager = Manager;
        myAudio = GetComponent<RandomContainer>();
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
                int material;
                do
                    material = Random.Range(0, NPCMaterials.Length);
                while (material == lastMaterial);
                currentCustomer.transform.GetChild(0).GetComponent<MeshRenderer>().material = NPCMaterials[material];
                currentCustomer.GetComponent<CameraMovement>().StartMoving(Vector3.zero, Vector3.zero, EnterTime);
                currentCustomerScript = currentCustomer.GetComponent<Customer>();
                currentCustomerScript.totalCards = Random.Range(currentCustomerScript.minTotalCards, currentCustomerScript.maxTotalCards);
                FindObjectOfType<CardSpawner>().timeDelayBetweenCards = currentCustomerScript.CardSpawn;
                gameTimer.time = 0;
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
                        {
                            customerIndex = Customers.Length - 1;
                            gameWon = true;
                            gameCam.GetComponent<CameraMovement>().StartMoving(WinLookUpPos, WinLookUpRot, WinLookUpTime);
                            myAudio.clips[0] = winClip;
                            myAudio.PlaySound(false);
                            GameManager.paused = true;
                        }
                    }
                }
            }

            if (currentCustomerScript.Satisfaction <= 0 && spawnManager)
            {
                allCards = true;
                cardsDone = true;
                GameManager.paused = true;
                fired = true;
                GameObject.FindGameObjectWithTag("Jukebox").GetComponent<AudioSource>().Stop();
                myAudio.clips[0] = loseClip;
                myAudio.PlaySound(false);
                currentCustomer.GetComponent<CameraMovement>().StartMoving(FinalPos, Vector3.zero, LeaveTime, true);
                gameCam.GetComponent<CameraMovement>().StartMoving(LookUpCameraPos, LookUpCameraRot, CameraEndTime);
                manager = Instantiate(Manager);
                manager.transform.position = SpawnPos;
                manager.GetComponent<CameraMovement>().StartMoving(Vector3.zero, Vector3.zero, EnterTime);
                spawnManager = false;
            }
        }

        if (gameWon)
        {
            CameraMovement gameCamMove = gameCam.GetComponent<CameraMovement>();
            if (!gameCamMove.inCoroutine)
            {
                if (!leftStore)
                {
                    gameCamMove.startY = gameCam.transform.position.y;
                    gameCamMove.isCustomer = true;
                    gameCamMove.StartMoving(WinLeavePos, WinLeaveRot, WinLeaveTime);
                    leftStore = true;
                }
                else
                {
                    StoreLeave.SetActive(true);
                    GameObject.FindGameObjectWithTag("Jukebox").GetComponent<AudioSource>().Stop();
                    if (blackoutTime <= 0)
                        SceneManager.LoadScene(0);
                    else
                        blackoutTime -= Time.deltaTime;
                }
            }
        }

        if (fired && !manager.GetComponent<CameraMovement>().inCoroutine)
        {
            if (!playedFired)
            {
                manager.GetComponent<RandomContainer>().PlaySound(false);
                playedFired = true;
            }

            if (dialogueTime <= 0)
                SceneManager.LoadScene(0);
            else
                dialogueTime -= Time.deltaTime;
        }
    }
}
