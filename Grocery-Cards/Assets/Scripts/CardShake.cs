using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardShake : MonoBehaviour
{
    public bool shake = false;
    float strength = 1;
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
            float newRot = this.transform.rotation.z + (Mathf.Sin(time) * strength);
            this.transform.rotation = new Quaternion(this.transform.rotation.x, this.transform.rotation.y, newRot, this.transform.rotation.w);
        }
    }
}
