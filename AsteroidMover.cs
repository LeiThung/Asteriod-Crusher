using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidMover : MonoBehaviour
{
    private Vector3 targetPos;
    [SerializeField] float speed;
    [SerializeField] float rotationSpeed;
    private Vector3 direction;

    void Start()
    {
        targetPos = FindObjectOfType<PlayerController>().gameObject.transform.position;
        direction = (targetPos - transform.position).normalized;
    }

    // Update is called once per frame
    void Update()
    {     
        transform.position += direction * speed * Time.deltaTime;
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }

    private IEnumerator AutoDestruction()
    {
        yield return new WaitForSeconds(30);
        Destroy(this.gameObject);   
    }
}
