using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    public int Satisfaction;
    [SerializeField] int DepositIncrease;
    [SerializeField] float TimeLimit;
    [SerializeField] float TimeLimitDelay;
    [SerializeField] int RatingDecrease;
    [SerializeField] int MaxCards;
    [SerializeField] float MaxCardTimeDecrease;

    float maxDelayTime;
    float delayTimer;

    Timer timer;
    CardSystem cards;
    
    // Start is called before the first frame update
    void Start()
    {
        timer = FindObjectOfType<Timer>();
        cards = FindObjectOfType<CardSystem>();
        maxDelayTime = TimeLimitDelay;
        delayTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        delayTimer += Time.deltaTime;

        if (cards.cards.Length > MaxCards)
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

        if (Satisfaction <= 0)
        {
            // end the game
            Debug.Log("You're fired");
        }
    }

    void WrongItem(Card attemptedCard)
    {
        Satisfaction -= attemptedCard.cost;
    }

    void BagDeposit(Bag depositedBag)
    {
        int value = DepositIncrease - depositedBag.bagSpace;
        Satisfaction += value;
    }
}
