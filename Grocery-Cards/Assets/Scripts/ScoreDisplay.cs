using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    CustomerSpawner custSpawn;
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
        myImage.fillAmount = Mathf.Lerp(0, 1, ((float)custSpawn.currentCustomerScript.Satisfaction) / ((float)custSpawn.currentCustomerScript.MaxSatisfaction));
    }
}
