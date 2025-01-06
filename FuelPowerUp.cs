using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelPowerUp : MonoBehaviour
{
    [SerializeField] int fuelinPercent = 30;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Collector"))
        {
            collision.gameObject.GetComponentInParent<PlayerController>().AddFuel(fuelinPercent);
            Destroy(this.gameObject);
        }
    }
}
