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
    [HideInInspector] public GameObject positionToMoveTo;
    IEnumerator LerpPosition(Vector3 targetPosition, float duration)
    {
        float time = 0;
        Vector3 startPosition = this.transform.position;
        while (time < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;
    }

    public void startMoving()
    {
        StartCoroutine(LerpPosition(positionToMoveTo.transform.position, .5f));
    }
    private void Start()
    {
        if (transform.childCount > 0)
            transform.GetChild(0).GetComponent<TextMeshPro>().SetText(cost.ToString());
    }
}
