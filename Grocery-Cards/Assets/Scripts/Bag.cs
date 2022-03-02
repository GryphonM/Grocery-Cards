using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bag : MonoBehaviour
{
    public int bagID; // set bag id equal to its index in the bags array in CardSystem.cs
    public int bagSpace;
    public int bagType;
    public int cardsDeposited;
    private CardSystem cardSyst;

    private void Start()
    {
        cardSyst = FindObjectOfType<CardSystem>();
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("card is in the bag");
        if (other.gameObject.tag == "card")
        {
            if (bagType == other.gameObject.GetComponent<Card>().cardType)
            {
                cardSyst.currentBankableID = bagID;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        cardSyst.currentBankableID = 999;//999 is non bankable number
    }
}
