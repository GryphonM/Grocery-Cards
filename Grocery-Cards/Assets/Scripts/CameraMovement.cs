using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Vector3 FinalPosition;
    public Vector3 FinalRotation;
    public float durationOfMovement;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    StartMoving(FinalPosition, FinalRotation, durationOfMovement);
        //}
    }

    public void StartMoving(Vector3 finalPosition, Vector3 finalRotation, float duration, bool destroyOnEnd = false)
    {
        if (transform.childCount > 0 && transform.GetChild(0).GetComponent<Animator>() != null)
            transform.GetChild(0).GetComponent<Animator>().SetBool("Walking", true);
        StartCoroutine(LerpPosition(finalPosition, finalRotation, duration, destroyOnEnd));
    }

    IEnumerator LerpPosition(Vector3 targetPosition, Vector3 targetRotation, float duration, bool destroyOnEnd = false)
    {
        float time = 0;
        Vector3 startPosition = transform.position;
        Vector3 startRotation = transform.rotation.eulerAngles;
        while (time < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            transform.rotation = Quaternion.Slerp(Quaternion.Euler(startRotation), Quaternion.Euler(targetRotation), time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.SetPositionAndRotation(targetPosition, Quaternion.Euler(targetRotation));
        if (transform.childCount > 0 && transform.GetChild(0).GetComponent<Animator>() != null)
            transform.GetChild(0).GetComponent<Animator>().SetBool("Walking", false);
        if (destroyOnEnd)
            Destroy(gameObject);
    }
}
