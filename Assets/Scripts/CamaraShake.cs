using System.Collections;
using UnityEngine;

public class CamaraShake : MonoBehaviour
{
    [SerializeField] float shakeDuration = 1f;
    [SerializeField] float shakeMagniture = 0.5f;
    Vector3 initualPosition;

    void Start()
    {
        initualPosition = transform.position;
    }

    public void Play()
    {
        StartCoroutine(Shake());
    }

    IEnumerator Shake()
    {
        Debug.Log("test 2");
        float elapsedTime = 0;
        while(elapsedTime < shakeDuration)
        {
            transform.position = initualPosition + (Vector3)Random.insideUnitCircle * shakeMagniture;
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        transform.position = initualPosition;
    }


}
