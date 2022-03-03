using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [HideInInspector] public float time = 0.0f;
    TextMeshProUGUI myText;
    
    // Start is called before the first frame update
    void Start()
    {
        myText = this.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        myText.text = ((int)time).ToString();
    }
}
