using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Card : MonoBehaviour
{
    //CardType
    // 0 = normal
    // 1 = cold
    // 2 = hot
    public int cost;
    public int cardType;
    public int cardID;

    private void Start()
    {
        if (transform.childCount > 0)
            transform.GetChild(0).GetComponent<TextMeshPro>().SetText(cost.ToString());
    }
}
