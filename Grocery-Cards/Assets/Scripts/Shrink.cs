using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrink : MonoBehaviour
{
    // Start is called before the first frame update
    public bool shrink = false;
    float finalSizeY = 0.01f;
    float startSizeY = 0;
    float finalSizeX = 0.01f;
    float startSizeX = 0;
    public float duration = 1f;
    public float speed = 2f;
    void Start()
    {
        startSizeX = this.transform.localScale.x;
        startSizeY = this.transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (shrink == true)
        {
            this.transform.localScale = (new Vector3(Mathf.Lerp(finalSizeX, startSizeX, duration), Mathf.Lerp(finalSizeY, startSizeY, duration), this.transform.localScale.z));
            duration -= Time.deltaTime * speed;
        }
        if (this.transform.localScale.x < .02 && this.transform.localScale.y < .02)
            Destroy(this.gameObject);
    }
}
