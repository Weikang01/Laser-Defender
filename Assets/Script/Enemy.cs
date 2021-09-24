using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [Header("General property")]
    [SerializeField] float health = 100;
    [SerializeField] int scoreValue = 100;

    [Header("Projectile")]
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShots = 1f;
    [SerializeField] float maxTimeBetweenShots = 5f;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] GameObject explodeEffectPrefab;

    [Header("Audio")]
    [SerializeField] float waitForSeconds = 1f;
    [SerializeField] AudioClip shootSound;
    [SerializeField] [Range(0, 1)] float shootVolume = 0.4f;
    [SerializeField] AudioClip deadSound;
    [SerializeField] [Range(0, 1)] float deathVolume = 0.6f;

    // Use this for initialization
    void Start () {
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }
	
	// Update is called once per frame
	void Update ()
    {
        CountDownAndShoot();
        Die();
    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0)
        {
            EnemyFire();
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void EnemyFire()
    {
        GameObject bullet = Instantiate(
            bulletPrefab, 
            gameObject.transform.position, 
            Quaternion.identity
            ) as GameObject;
        AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootVolume);
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
    }

    private void Die()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
            GameObject explodeEffect = Instantiate(explodeEffectPrefab, transform.position, transform.rotation);
            Destroy(explodeEffect, waitForSeconds);
            AudioSource.PlayClipAtPoint(deadSound, Camera.main.transform.position, deathVolume);
            FindObjectOfType<GameSession>().Score+= scoreValue;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        health -= damageDealer.GetDamage();
        Destroy(other.gameObject);
    }

}
