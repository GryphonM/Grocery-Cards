using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    float lifetime = 0;
    bool normalize = false;
    [HideInInspector] public bool bob = false;
    public bool isCustomer = false;
    public float bobStrength = 1;
    public float bobSpeed = 1;
    float startY;
    
    // Start is called before the first frame update
    void Start()
    {
        startY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (bob)
        {
            lifetime += Time.deltaTime;
            transform.position = new Vector3(this.transform.position.x, startY + (Mathf.Sin(lifetime * bobSpeed) * bobStrength), this.transform.position.z);
        }
        if (normalize)
        {
            Vector3 newPos = transform.position;
            newPos.y -= bobSpeed * Time.deltaTime;
            if (newPos.y <= 0)
            {
                newPos.y = 0;
                normalize = false;
            }
            transform.position = newPos;
        }
    }

    public void StartMoving(Vector3 finalPosition, Vector3 finalRotation, float duration, bool destroyOnEnd = false)
    {
        if (isCustomer)
            bob = true;
        StartCoroutine(LerpPosition(finalPosition, finalRotation, duration, destroyOnEnd));
    }

    IEnumerator LerpPosition(Vector3 targetPosition, Vector3 targetRotation, float duration, bool destroyOnEnd = false)
    {
        float time = 0;
        Vector3 startPosition = transform.position;
        Vector3 startRotation = transform.rotation.eulerAngles;
        while (time < duration)
        {
            Vector3 newPos = Vector3.Lerp(startPosition, targetPosition, time / duration);
            if (bob)
                newPos.y = transform.position.y;
            transform.position = newPos;
            transform.rotation = Quaternion.Slerp(Quaternion.Euler(startRotation), Quaternion.Euler(targetRotation), time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        if (bob)
        {
            targetPosition.y = transform.position.y;
            normalize = true;
        }
        transform.SetPositionAndRotation(targetPosition, Quaternion.Euler(targetRotation));
        if (isCustomer)
            bob = false;
        if (destroyOnEnd)
            Destroy(gameObject);
    }
}
