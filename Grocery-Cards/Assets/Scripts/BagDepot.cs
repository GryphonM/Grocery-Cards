using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BagDepot : MonoBehaviour
{
    [HideInInspector] public int bagID = 4; // set bag id equal to its index in the bags array in CardSystem.cs
    public int bagsDeposited;
    private CardSystem cardSyst;

    private void Start()
    {
        cardSyst = FindObjectOfType<CardSystem>();
    }
    private void Update()
    {
        if (transform.childCount > 0)
            transform.GetChild(0).GetComponent<TextMeshPro>().SetText(bagsDeposited.ToString());
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "bag")
        {
            cardSyst.currentBankableID = bagID;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        cardSyst.currentBankableID = 999;//999 is non bankable number
    }
}
