using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField] float health = 100;
    [SerializeField] int scoreValue = 100;
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShots = .2f;
    [SerializeField] float maxTimeBetweenShots = 2f;
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] GameObject explosionVFX;
    [SerializeField] AudioClip dieClip;
    [SerializeField] AudioClip shootClip;

    [Header("Power UP's")]
    [SerializeField] GameObject powerUpPrefab;
    [SerializeField] float powerUpSpeed = 5f;
    [SerializeField] GameObject healthUpPrefab;
    [SerializeField] float healthUpSpeed = 5f;
    [SerializeField] GameObject starsPrefab;
    [SerializeField] float starsSpeed = 5f;

    DataManager dataManager;

    float sfxVolume;
    // Use this for initialization
    void Start () {

        dataManager = FindObjectOfType<DataManager>();
        shotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        sfxVolume = DataManager.GetSfxVolume();
    }
	
	// Update is called once per frame
	void Update () {
        CountDownAndShoot();
    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if(shotCounter <= 0f)
        {
            Fire();
            shotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void Fire()
    {
        GameObject laser = Instantiate(
            laserPrefab, 
            transform.position, 
            Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
        AudioSource.PlayClipAtPoint(shootClip, Camera.main.transform.position, sfxVolume);
    }

    private void PowerUpSpawn()
    {
        if (FindObjectsOfType<PowerUp>().Length < 1)
        {

            GameObject powerUp = Instantiate(
                powerUpPrefab,
                transform.position,
                Quaternion.identity) as GameObject;
            powerUp.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -powerUpSpeed);
        }
    }

    private void HealthUpSpawn()
    {
        if (FindObjectsOfType<HealthUp>().Length < 1)
        {
                GameObject healthUp = Instantiate(
                    healthUpPrefab,
                    transform.position,
                    Quaternion.identity) as GameObject;
                healthUp.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -healthUpSpeed);
        }
    }

    private void StarsUpSpawn()
    {
        if (FindObjectsOfType<CoinUP>().Length < 1)
        {
            GameObject healthUp = Instantiate(
                starsPrefab,
                transform.position,
                Quaternion.identity) as GameObject;
            healthUp.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -starsSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        SpawnBonuses();
        dataManager.AddToScore(scoreValue);
        AudioSource.PlayClipAtPoint(dieClip, Camera.main.transform.position, sfxVolume);
        Destroy(gameObject);
        GameObject explosionParticle = Instantiate(
            explosionVFX,
            transform.position,
            transform.rotation) as GameObject;
        Destroy(explosionParticle, 1f);
    }

    private void SpawnBonuses()
    {
        int powerUpChance = UnityEngine.Random.Range(1, 100);
        if (powerUpChance > 0 && powerUpChance <= 7)
        {
            PowerUpSpawn();
        }
        int healthUpChance = UnityEngine.Random.Range(1, 100);
        if (healthUpChance > 0 && healthUpChance <= 7)
        {
            HealthUpSpawn();
        }
        int starsUpChance = UnityEngine.Random.Range(1, 100);
        if (starsUpChance > 0 && starsUpChance <= 3)
        {
            StarsUpSpawn();
        }
    }
}
