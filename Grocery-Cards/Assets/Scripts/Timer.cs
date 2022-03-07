using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [HideInInspector] public float time = 0.0f;
    TextMeshProUGUI myText;
    CustomerSpawner customerSpawn;
    
    // Start is called before the first frame update
    void Start()
    {
        myText = GetComponent<TextMeshProUGUI>();
        customerSpawn = FindObjectOfType<CustomerSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.paused && !customerSpawn.betweenCustomers)
        {
            time += Time.deltaTime;
            myText.text = ((int)time).ToString();
        }
    }
}
