using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagSpawner : MonoBehaviour
{
    CardSystem cardSys;
    public int randomMin;
    public int randomMax;
    public GameObject[] bags;
    int lastBagGotten = 999;
    // Start is called before the first frame update
    void Start()
    {
        cardSys = FindObjectOfType<CardSystem>();
    }
    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < cardSys.bags.Length; i++)
        {
            if(cardSys.bags[i] == null)
            {                
                GameObject createdCard = Instantiate(bags[Random.Range(0, bags.Length)]);               
                Bag bagScript = createdCard.gameObject.GetComponent<Bag>();
                while (bagScript.bagID == lastBagGotten)
                {
                    Destroy(createdCard.gameObject);
                    createdCard = Instantiate(bags[Random.Range(0, bags.Length)]);
                    bagScript = createdCard.gameObject.GetComponent<Bag>();
                }
                lastBagGotten = bagScript.bagID;
                bagScript.bagID = i;
                bagScript.bagSpace = Random.Range(randomMin, randomMax);
                cardSys.bags[i] = bagScript;
                createdCard.transform.position = cardSys.bagSnapPoints[i].transform.position;
            }
        }
    }
}