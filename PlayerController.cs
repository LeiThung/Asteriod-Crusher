using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] CircleCollider2D collectorCol;
    [SerializeField] SavedSkin skin;
    [Header("Projectiles")]
    [SerializeField] GameObject projectile;
    [SerializeField] Transform projectileSpawn;
    [Header("RocketFlames")]
    [SerializeField] GameObject rocketflames;
    [Header("Shield")]
    [SerializeField] GameObject shield;
    [Header("Stats")]
    [SerializeField] public float fuel;
    [SerializeField] public int health;
    [SerializeField] public int damage;
    [SerializeField] public float speed;
    [SerializeField] public float fireRate;
    [SerializeField] float powerUpCollectRadius;
    private int maxHealth;
    public float maxFuel;

    // Powerup stuff
    public int damageMultipliar = 1;
    public float speedMultipliar = 1;
    private bool shieldOn = false;
    public float fuelReductionMultipliar;

    private float timer;
    private Camera cam;
    private UI ui;
    public int killed;
    private float playTime;
    private float deadzone = 0.25f;

    private void Start()
    {
        Skin obj = Instantiate(skin.savedSkin);
        obj.transform.SetParent(this.transform);

        Time.timeScale = 1f;
        cam = Camera.main;
        ui = FindObjectOfType<UI>();

        // rocket flames
        GameObject obj1 = Instantiate(rocketflames, obj.rocketspot.position, Quaternion.identity);
        obj1.transform.parent = transform;
       
        maxFuel = fuel;
        maxHealth = health;
        ui.fuelSlider.maxValue = maxFuel;

        collectorCol.radius = collectorCol.radius * PlayerPrefs.GetFloat("MagnetMultipliar");
    }

    void FixedUpdate()
    {
        Vector3 moveDirection = new Vector3(ui.joystickLeft.Horizontal, ui.joystickLeft.Vertical, 0).normalized;
        transform.position += moveDirection * (speed * speedMultipliar) * Time.deltaTime;

        // player Pos
        float playerClampX = Mathf.Clamp(transform.position.x, -36.25f, 36.25f);
        float playerClampY = Mathf.Clamp(transform.position.y, -28.75f, 28.75f);
        transform.position = new Vector3(playerClampX, playerClampY, transform.position.z);
       
        // cam movement
        float clampX = Mathf.Clamp(transform.position.x, -24.5f, 24.5f);
        float clampY = Mathf.Clamp(transform.position.y, -22f, 22f);
        Vector3 newPos = new Vector3 (clampX, clampY, cam.transform.position.z);
        cam.transform.position = Vector3.Lerp(cam.transform.position, newPos, speed * speedMultipliar);
        
        
        if(ui.joystickRight.Horizontal != 0 || ui.joystickRight.Vertical != 0)
        {
            RotatePlayer();
        }

        ui.shootBtn.onClick.AddListener(ShootBullet);

        if(timer > 0) timer -= Time.deltaTime;

        // fuel bar
        ui.fuelSlider.value = fuel;
        fuelReductionMultipliar = PlayerPrefs.GetFloat("EngineMultipliar");
        if (fuel > 0)
        {
            float percentage = (fuel / maxFuel) * 100f;
            ui.fueltxt.text = Mathf.RoundToInt(percentage).ToString();
        }
        fuel -= Time.deltaTime * fuelReductionMultipliar;   

        // shield
        if (shieldOn) shield.SetActive(true);
        else shield.SetActive(false);

        playTime += Time.deltaTime;
    }

    private void RotatePlayer()
    {
        // Apply deadzone to Right Joystick input
        float horizontal = Mathf.Abs(ui.joystickRight.Horizontal) > deadzone ? ui.joystickRight.Horizontal : 0f;
        float vertical = Mathf.Abs(ui.joystickRight.Vertical) > deadzone ? ui.joystickRight.Vertical : 0f;

        if (horizontal != 0f || vertical != 0f)
        {
            float angle = Mathf.Atan2(vertical, horizontal) * Mathf.Rad2Deg - 90;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    private void ShootBullet()
    {
        if (timer <= 0)
        {
            GameObject obj = Instantiate(projectile, projectileSpawn.position, Quaternion.identity);
            obj.transform.rotation = transform.rotation;
            obj.GetComponent<Projectile>().damage = damage * damageMultipliar;
            timer = fireRate;
        }
    }

    public void AddFuel(int percentage)
    {
        if (fuel < (maxFuel / 100) * percentage)
        {
            fuel += (maxFuel / 100) * percentage;
        }
        else fuel = maxFuel;  
    }

    public void AddDamage(int amount, float waitTime, Sprite icon)
    {
        StartCoroutine(AddDamageMultipliar(amount, waitTime, icon));
    }

    private IEnumerator AddDamageMultipliar(int amount, float waittime, Sprite icon)
    {
        damageMultipliar = amount;
        ui.SetPowerUpIcon(icon);
        yield return new WaitForSeconds(waittime);
        ui.SetPowerUpIcon(ui.transparentimg);
        damageMultipliar = 1;
    }

    public void AddSpeed(float amount, float waitTime, Sprite icon)
    {
        StartCoroutine(AddSpeedBoost(amount, waitTime, icon));
    }

    private IEnumerator AddSpeedBoost(float amount, float waitTime, Sprite icon)
    {
        speedMultipliar = amount;
        ui.SetPowerUpIcon(icon);
        yield return new WaitForSeconds(waitTime);
        ui.SetPowerUpIcon(ui.transparentimg);
        speedMultipliar = 1;
    }

    public void Addhealth(int amount)
    {
        if(health != maxHealth) health += 1;
        else health = maxHealth;
        ui.UpdateHeartUI(health);
    }

    public void SetShieldActive()
    {
        shieldOn = true;
    }

    public void AddKill()
    {
        killed++;
    }

    public void TakeDamage()
    {
        if(!shieldOn) health -= 1;
        else shieldOn = false;
        ui.UpdateHeartUI(health);
        if (health <= 0) Die();
    }

    private void Die()
    {
        ui.UpdateHeartUI(0);
        ui.gameOver = true;
        Score score = GetComponent<Score>();
        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + score.score);
        ui.GameOverUI(score.score, killed, playTime);
        Destroy(gameObject);
    }
}
