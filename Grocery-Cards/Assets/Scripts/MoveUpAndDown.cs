using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUpAndDown : MonoBehaviour
{
    // Start is called before the first frame update
    float time_ = 0;
    public bool move = true;
    public float strength = 1;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(move == true)
        {
            time_ += Time.deltaTime;
            this.transform.position = new Vector3(this.transform.position.x, Mathf.Sin(time_) * strength, this.transform.position.z);
        }
    }
}
