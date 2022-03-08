using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    CustomerSpawner custSpawn;
    public float ammountToFill;
    Image myImage;
    
    // Start is called before the first frame update
    void Start()
    {
        myImage = GetComponent<Image>();
        custSpawn = FindObjectOfType<CustomerSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        ammountToFill = Mathf.Lerp(0, 1, ((float)custSpawn.currentCustomerScript.Satisfaction) / ((float)custSpawn.currentCustomerScript.MaxSatisfaction));
        myImage.fillAmount = ammountToFill;
        //myImage.fillAmount = Mathf.Lerp(0, 1, ((float)custSpawn.currentCustomerScript.Satisfaction) / ((float)custSpawn.currentCustomerScript.MaxSatisfaction));
    }
}
