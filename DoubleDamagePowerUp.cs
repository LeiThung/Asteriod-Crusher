using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoubleDamagePowerUp : MonoBehaviour
{
    [SerializeField] int damageMultipliar = 2;
    [SerializeField] float cooldown = 15f;
    [SerializeField] Sprite icon;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponentInParent<PlayerController>().AddDamage(damageMultipliar, cooldown, icon);
            Destroy(this.gameObject);
        }
    }
}
