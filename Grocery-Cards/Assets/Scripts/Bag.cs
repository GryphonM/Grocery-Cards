using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Bag : MonoBehaviour
{
    public int bagID; // set bag id equal to its index in the bags array in CardSystem.cs
    public int bagSpace;
    public int bagType;
    public int cardsDeposited;
    private CardSystem cardSyst;
    public Material fullBag;

    private void Start()
    {
        cardSyst = FindObjectOfType<CardSystem>();
    }
    private void Update()
    {
        if (transform.childCount > 0)
            transform.GetChild(0).GetComponent<TextMeshPro>().SetText(bagSpace.ToString());
        if (cardsDeposited != 0 && fullBag != null)
            this.GetComponent<Renderer>().material = fullBag;

        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "card")
        {
            if (bagType == other.gameObject.GetComponent<Card>().cardType)
            {
                cardSyst.currentBankableID = bagID;
            }
            else
            {
                cardSyst.currentBankableID = 998;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        cardSyst.currentBankableID = 999;//999 is non bankable number
    }
}
