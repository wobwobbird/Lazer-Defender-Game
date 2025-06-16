
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] bool isPlayer;
    [SerializeField] int health = 50;
    [SerializeField] int enemyKillPoints = 50;
    [SerializeField] ParticleSystem hitEffect;
    [SerializeField] bool applyCamaraShake;

    CamaraShake camaraShake;

    ScoreKeeper scoreKeeper;
    AudioPlayer audioPlayer;
    LevelManager levelManager;

    void Awake()
    {
        camaraShake = Camera.main.GetComponent<CamaraShake>();
        Debug.Log("awake");
        scoreKeeper = FindAnyObjectByType<ScoreKeeper>();
        audioPlayer = FindAnyObjectByType<AudioPlayer>(); 
        levelManager = FindAnyObjectByType<LevelManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();

        if(damageDealer != null)
        {
            TakeDamage(damageDealer.GetDamage());
            PlayHitEffect();
            audioPlayer.PlayDamageClip();
            ShakeCamara();
            damageDealer.Hit();
        }
    }

    public int GetHealth()
    {
        return health;
    }

    void TakeDamage(int damage)
    {
        health -= damage;
        if(health <=0)
        {
            Die();
        }
    }

    void Die()
    {
        if(!isPlayer)
        {
            scoreKeeper.ModifyScore(enemyKillPoints);
        }
        else
        {
            levelManager.LoadGameOver();
        }
        Destroy(gameObject);
    }

    void PlayHitEffect()
    {
        if(hitEffect !=null)
        {
            ParticleSystem instance = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        }
    }

    void ShakeCamara()
    {
        Debug.Log("Test ShakeCamara()");
        if(camaraShake != null && applyCamaraShake)
        {
            camaraShake.Play();

            Debug.Log("test 1");
        }
    }
}
