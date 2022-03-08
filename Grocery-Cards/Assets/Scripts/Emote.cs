using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emote : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject emote1;
    public GameObject emote2;
    public GameObject emote3;
    public GameObject emote4;
    public GameObject emote5;
    public GameObject emote6;
    public ScoreDisplay score;
    int emote;
    public float satisfaction;
    public void disableEmotes()
    {
        emote1.SetActive(false);
        emote2.SetActive(false);
        emote3.SetActive(false);
        emote4.SetActive(false);
        emote5.SetActive(false);
        emote6.SetActive(false);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        satisfaction = score.ammountToFill;
        if (satisfaction >= .83f)
        {
            emote = 1;
        }
        else if (satisfaction >= .66f)
        {
            emote = 2;
        }
        else if (satisfaction >= .49f)
        {
            emote = 3;
        }
        else if (satisfaction >= .32f)
        {
            emote = 4;
        }
        else if (satisfaction >= .15f)
        {
            emote = 5;
        }
        else
        {
            emote = 6;
        }
        switch (emote)
        {
            case 1:                
                emote2.SetActive(false);
                emote3.SetActive(false);
                emote4.SetActive(false);
                emote5.SetActive(false);
                emote6.SetActive(false);
                emote1.SetActive(true);
                break;
            case 2:
                emote1.SetActive(false);
                emote3.SetActive(false);
                emote4.SetActive(false);
                emote5.SetActive(false);
                emote6.SetActive(false);
                emote2.SetActive(true);
                break;
            case 3:
                emote1.SetActive(false);
                emote2.SetActive(false);
                emote4.SetActive(false);
                emote5.SetActive(false);
                emote6.SetActive(false);
                emote3.SetActive(true);
                break;
            case 4:
                emote1.SetActive(false);
                emote2.SetActive(false);
                emote3.SetActive(false);
                emote5.SetActive(false);
                emote6.SetActive(false);
                emote4.SetActive(true);
                break;
            case 5:
                emote1.SetActive(false);
                emote2.SetActive(false);
                emote3.SetActive(false);
                emote4.SetActive(false);
                emote6.SetActive(false);
                emote5.SetActive(true);
                break;
            case 6:
                emote1.SetActive(false);
                emote2.SetActive(false);
                emote3.SetActive(false);
                emote4.SetActive(false);
                emote5.SetActive(false);
                emote6.SetActive(true);
                break;
        }
    }
}
