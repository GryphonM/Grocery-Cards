using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    [Tooltip("Maximum Satisfaction to Be Shown on UI")]
    public int MaxSatisfaction;
    [Tooltip("Current/Starting Satisfaction Level")]
    public int Satisfaction;
    [Tooltip("Satisfaction Increase on Full Deposited Bag.\nNon-empty bag increases Satisfaction by this amount - leftover space")]
    [SerializeField] int DepositIncrease;
    [Space(10)]
    [Tooltip("Time before satisfaction goes down over time")]
    [SerializeField] float TimeLimit;
    [Tooltip("Time between time-based decreases of Satisfaction")]
    [SerializeField] float TimeLimitDelay;
    [Tooltip("Amount subtracted from Satisfaction after Time Limit Delay")]
    [SerializeField] int RatingDecrease;
    [Space(10)]
    [Tooltip("Maximum Cards on the conveyor belt before Time Decrease kicks in")]
    [SerializeField] int MaxCards;
    [Tooltip("Amount subtracted from Time Limit Delay there are Max Cards")]
    [SerializeField] float MaxCardTimeDecrease;
    [Space(10)]
    [Tooltip("Card Spawning Rate")]
    public float CardSpawn = 1.75f;
    [Space(10)]
    [Tooltip("Minimum Number of cards before end of Customer")]
    public int minTotalCards;
    [Tooltip("Maximum Number of cards before end of Customer")]
    public int maxTotalCards;
    /*[HideInInspector]*/ public int totalCards;
    /*[HideInInspector]*/ public int cardCount;

    float maxDelayTime;
    float delayTimer;

    Timer timer;
    CardSystem cards;
    CustomerSpawner custSpawn;
    
    // Start is called before the first frame update
    void Start()
    {
        timer = FindObjectOfType<Timer>(true);
        cards = FindObjectOfType<CardSystem>();
        custSpawn = FindObjectOfType<CustomerSpawner>();
        maxDelayTime = TimeLimitDelay;
        delayTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.paused)
        {
            delayTimer += Time.deltaTime;

            // Get number of cards
            int cardNum = 0;
            foreach (Card card in cards.cards)
            {
                if (card != null)
                    cardNum++;
            }

            if (cardNum > MaxCards)
            {
                maxDelayTime = TimeLimitDelay - MaxCardTimeDecrease;
            }
            else
            {
                maxDelayTime = TimeLimitDelay;
            }

            if (timer.time >= TimeLimit)
            {
                if (delayTimer >= maxDelayTime)
                {
                    Satisfaction -= RatingDecrease;
                    delayTimer = 0;
                }
            }
        }
    }

    public void WrongItem(Card attemptedCard)
    {
        Satisfaction -= attemptedCard.cost;
        GetComponent<RandomContainer>().PlaySound(true);
    }

    public void BagDeposit(Bag depositedBag)
    {
        int value = DepositIncrease - depositedBag.bagSpace;
        if (custSpawn.cardsDone)
            value = DepositIncrease;
        if (value <= 0)
            GetComponent<RandomContainer>().PlaySound(true);
        Satisfaction += value;
        if (Satisfaction > MaxSatisfaction)
            Satisfaction = MaxSatisfaction;
    }
}
