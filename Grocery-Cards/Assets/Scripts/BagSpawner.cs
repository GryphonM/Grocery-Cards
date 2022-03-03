using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagSpawner : MonoBehaviour
{
    CardSystem cardSys;
    public int randomMin;
    public int randomMax;
    public int forceBag;
    public GameObject[] bags;
    int lastBagGotten = 999;
    int[] timeSinceBag = {0, 0, 0};

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
                for (int j = 0; j < timeSinceBag.Length; j++)
                {
                    if (timeSinceBag[j] >= forceBag)
                    {
                        Debug.Log("bag forced");
                        Destroy(createdCard.gameObject);
                        createdCard = Instantiate(bags[j]);
                        bagScript = createdCard.gameObject.GetComponent<Bag>();
                        timeSinceBag[j] = 0;
                    }
                }
                if (bagScript.bagID == 0)
                {
                    timeSinceBag[0] = 0;
                    timeSinceBag[1] += 1;
                    timeSinceBag[2] += 1;
                }
                else if (bagScript.bagID == 1)
                {
                    timeSinceBag[0] += 1;
                    timeSinceBag[1] = 0;
                    timeSinceBag[2] += 1;
                }
                else if (bagScript.bagID == 2)
                {
                    timeSinceBag[0] += 1;
                    timeSinceBag[1] += 1;
                    timeSinceBag[2] = 0;
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