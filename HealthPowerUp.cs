using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthPowerUp : MonoBehaviour
{
    [SerializeField] int healthAmount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Collector"))
        {
            collision.gameObject.GetComponent<PlayerController>().Addhealth(healthAmount);
            Destroy(this.gameObject);
        }
    }
}
