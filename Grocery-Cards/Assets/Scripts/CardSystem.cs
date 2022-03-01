using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//banking cards
public class CardSystem : MonoBehaviour
{
    public Bag[] bags;
    [HideInInspector] public int currentBankableID = 999; //999 is value when cannot bank
    [HideInInspector] public GameObject heldCard;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        //banking cards into bags
        if (currentBankableID != 999 && Input.GetKeyUp(0) && heldCard.tag != "bag")
        {
            if (bags[currentBankableID].bagSpace - heldCard.GetComponent<Card>().cost < 0)
            {
                //make void the bag
            }
            else
            {
                bags[currentBankableID].bagSpace -= heldCard.GetComponent<Card>().cost;
                bags[currentBankableID].cardsDeposited += 1;
            }            
        }


    }
}
