using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSpawner : MonoBehaviour
{
    CardSystem cardSys;
    public int randomMin;
    public int randomMax;
    public GameObject[] cards;
    // Start is called before the first frame update
    void Start()
    {
        cardSys = FindObjectOfType<CardSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cardSys.cards[cardSys.cards.Length - 1] == null)
        {
            GameObject createdCard = Instantiate(cards[Random.Range(0, cardSys.cards.Length - 1)]);
            Card cardScript = createdCard.gameObject.GetComponent<Card>();
            cardScript.cost = Random.Range(randomMin, randomMax);
            cardSys.cards[cardSys.cards.Length - 1] = cardScript;
            createdCard.transform.position = cardSys.conveyorSnapPoints[cardSys.conveyorSnapPoints.Length - 1].transform.position;
        }
    }
}
