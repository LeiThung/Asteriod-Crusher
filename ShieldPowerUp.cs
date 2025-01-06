using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPowerUp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Collector"))
        {
            collision.gameObject.GetComponentInParent<PlayerController>().SetShieldActive();
            Destroy(this.gameObject);
        }           
    }
}
