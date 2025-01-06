using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxhealth;
    public float health;
    public int score;
    public int xp;
    private Sprite state100P, state80P, state60P, state40P, state20P, state10P;
    private SpriteRenderer spriteRenderer;
    public float attackCD = 3f;
    private Score scoreScript;
    private ParticleSystem explosion;
    private PowerUpDrops powerUpDrops;
    private HealthBar healthBar;
    private XP playerXp;
    private PlayerController playerController;

    public void Initialize(NewEnemyData data)
    {
        health = data.health;
        maxhealth = data.health;
        score = data.score;
        xp = data.xp;
        state100P = data.state1;
        state80P = data.state2;
        state60P = data.state3;
        state40P = data.state4;
        state20P = data.state5;
        state10P = data.state6;
        explosion = data.explosion;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = state100P;
        PolygonCollider2D col = gameObject.AddComponent<PolygonCollider2D>();
        scoreScript = FindObjectOfType<Score>();
        powerUpDrops = GetComponent<PowerUpDrops>();
        healthBar = GetComponent<HealthBar>();  
        playerXp = FindObjectOfType<XP>();
        playerController = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(attackCD > 0) attackCD -= Time.deltaTime;
    }

    private void ObjState()
    {
        if(health <= (maxhealth * 80) / 100 && health >= (maxhealth * 60) / 100)
        {
            if(state80P != null) spriteRenderer.sprite = state80P;
        }          
        else if(health <= (maxhealth * 60) / 100 && health >= (maxhealth * 40) / 100)
        {
            if(state60P != null) spriteRenderer.sprite = state60P;
        }
        else if (health <= (maxhealth * 40) / 100 && health >= (maxhealth * 20) / 100)
        {
            if(state40P != null) spriteRenderer.sprite = state40P;
        }
        else if (health <= (maxhealth * 20) / 100 && health >= (maxhealth * 10) / 100)
        {
            if(state20P != null) spriteRenderer.sprite = state20P;

        }
        else if (health <= (maxhealth * 10) / 100)
        {
            if(state10P != null) spriteRenderer.sprite = state10P;
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        ObjState();
        if(healthBar != null) healthBar.UpdatehealthBar();
        if (health <= 0) Die();

    }

    private void Die()
    {
        playerController.AddKill();
        scoreScript.AddScore(score);
        playerXp.GainXP(xp);
        Instantiate(explosion, transform.position, Quaternion.identity);
        powerUpDrops.SpawnPowerUp(transform);
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            if(attackCD <= 0)
            {
                collision.gameObject.GetComponent<PlayerController>().TakeDamage();
                attackCD = 3f;
            }          
        }
    }
}
