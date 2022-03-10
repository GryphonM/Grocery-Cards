using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardShake : MonoBehaviour
{
    public bool shake = false;
    float strength = 4f;
    float time = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (shake == true)
        {
            time += Time.deltaTime;
            float newRot = this.transform.rotation.z + Mathf.Sin(time * strength);
            this.transform.rotation = Quaternion.Euler(new Vector3(90.3f, 0, newRot));
        }
    }
}
