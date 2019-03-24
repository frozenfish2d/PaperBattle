using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {
    private float _speed = 30f;
    private float shieldLiveTime = 3f;
    //private Vector2 _startPos;

    [Header("Player")]
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] int health;
    [SerializeField] AudioClip dieClip;
    [SerializeField] AudioClip shootClip;
    [SerializeField] AudioClip powerClip;
    [SerializeField] AudioClip healthClip;
    [SerializeField] GameObject shield;
    [SerializeField] public float shieldTimer = 3f;
    [SerializeField] bool isActiveShield = false;

    [Header("Projectile")]
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] GameObject laserPrefab;
    float fireSpeed = 0.8f;

    [Header("Text's")]
    [SerializeField] Text startText;

    LevelManager levelManager;
    DataManager dataManager;
    float sfxVolume;
    float xMin, yMin, xMax, yMax;

    private void Awake()
    {
        StartCoroutine(Fire());
    }
    
    // Use this for initialization
    void Start() {
        SetUpMoveBoundaries();
        
        levelManager = FindObjectOfType<LevelManager>();
        dataManager = FindObjectOfType<DataManager>();
        dataManager.ReLoadAllData();
        shield.SetActive(isActiveShield);
        sfxVolume = DataManager.GetSfxVolume();
        startText.text += " " + levelManager.GetLevelIndex().ToString();
        fireSpeed = .2f;// - float.Parse(dataManager.playerData[3])*0.1f;
    }
    private void Update()
    {
        Move(); // Method for Editor
        MoveTouch(); // Method for Mobile
        if (isActiveShield) { ShieldCounter(); }

    }

    private void MoveTouch()// Method for Mobile
    {
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:

                    break;

                case TouchPhase.Moved:
                    Vector3 position = transform.position;
                    position = Vector3.Lerp(position, Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position), Time.deltaTime * _speed);
                    position.z = transform.position.z;
                    position.y = transform.position.y;
                    transform.position = position;
                    break;
            }
        }
    }

    private void ShieldCounter()
    {
        shieldTimer -= Time.deltaTime;
        if (shieldTimer <= 0)
        {
            isActiveShield = false;
            shield.SetActive(isActiveShield);
            shieldTimer = shieldLiveTime;
        }
    }

    private void Move()// Method for Editor
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        //var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        //var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);
        transform.position = new Vector2(newXPos, transform.position.y);
    }

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + .5f;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - .5f;
    }

    IEnumerator Fire()
    {
        
        GameObject laser = Instantiate(laserPrefab, transform.position+ new Vector3(0,1,0), Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
        AudioSource.PlayClipAtPoint(shootClip, Camera.main.transform.position, sfxVolume);
        yield return new WaitForSeconds(fireSpeed);
        StartCoroutine(Fire());
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        PowerUp powerUp = other.gameObject.GetComponent<PowerUp>();
        HealthUp healthUp = other.gameObject.GetComponent<HealthUp>();
        CoinUP coinUP = other.gameObject.GetComponent<CoinUP>();
        if (damageDealer && !isActiveShield) { ProcessHit(damageDealer); }
        if (powerUp) {
            if (isActiveShield) { shieldTimer += 5f; }
            Destroy(other.gameObject);
            isActiveShield = true;
            shield.SetActive(isActiveShield);
            AudioSource.PlayClipAtPoint(powerClip, Camera.main.transform.position, sfxVolume);
        }
        if (healthUp)
        {
            if ((dataManager.cur_health + healthUp.addHealth) <= dataManager.GetMaxHealth())
                {
                dataManager.cur_health += healthUp.addHealth;
                }
            Destroy(other.gameObject);
            AudioSource.PlayClipAtPoint(healthClip, Camera.main.transform.position, sfxVolume);
        }
        if (coinUP)
        {
            dataManager.AddToStars(coinUP.starCost);
            Destroy(other.gameObject);
            AudioSource.PlayClipAtPoint(healthClip, Camera.main.transform.position, sfxVolume);
        }
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        dataManager.cur_health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (dataManager.cur_health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(dieClip, Camera.main.transform.position, sfxVolume);
        dataManager.UpdateStars();
        dataManager.ReLoadAllData();
        levelManager.LoadGameOver();
    }

}