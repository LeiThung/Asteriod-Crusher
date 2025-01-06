using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SpeedPowerUp : MonoBehaviour
{
    [SerializeField] float speedMultipliar = 2;
    [SerializeField] float cooldown = 15f;
    [SerializeField] Sprite icon;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Collector"))
        {
            collision.gameObject.GetComponentInParent<PlayerController>().AddSpeed(speedMultipliar, cooldown, icon);
            Destroy(this.gameObject);
        }
    }
}
