using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage;
    [SerializeField] GameObject bulletBody;
    [SerializeField] GameObject explosion;
    [SerializeField] float speed;
    [SerializeField] float selfdestructionTime;

    void FixedUpdate()
    {
        transform.position += transform.up * speed * Time.deltaTime;
        selfdestructionTime -= Time.deltaTime;
        if (selfdestructionTime < 0) Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(damage);
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
