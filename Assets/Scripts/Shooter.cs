using System.Collections;
using UnityEngine;
using Unity.Mathematics;

public class Shooter : MonoBehaviour
{
    [Header("General")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifetime = 5f;
    [SerializeField] float baseFiringRate = 0.2f;

    [Header("AI")]
    [SerializeField] bool useAI;
    [SerializeField] float firingRateVariance = 0f;
    [SerializeField] float minimumFiringRate = 0.1f;

    [HideInInspector] public bool isFiring;

    Coroutine firingCoroutine;
    AudioPlayer audioPlayer;

    void Awake()
    {
       audioPlayer = FindAnyObjectByType<AudioPlayer>(); 
    }

    void Start()
    {
        if (useAI)
        {
            isFiring = true;
        }
    }

    void Update()
    {
       Fire(); 
    }

    void Fire()
    {
        if(isFiring && firingCoroutine == null)
        {
            firingCoroutine = StartCoroutine(FireContinously());
        }
        else if(!isFiring && firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
    }

    IEnumerator FireContinously()
    {
        while(true)
        {
            GameObject instance = Instantiate(projectilePrefab,
                                            transform.position, 
                                            Quaternion.identity);

            
            Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
            if(rb != null)
            {
                rb.linearVelocity = transform.up * projectileSpeed;
            }
            
            Destroy(instance, projectileLifetime);

            float timeToNextProjectile = UnityEngine.Random.Range(baseFiringRate - firingRateVariance,
                                                    baseFiringRate + firingRateVariance); 

            timeToNextProjectile = Mathf.Clamp(timeToNextProjectile, minimumFiringRate, float.MaxValue);
            
            audioPlayer.PlayShootingClip();
            
            yield return new WaitForSeconds(timeToNextProjectile);
        }
    }
}
