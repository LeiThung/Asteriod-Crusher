using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BeamShipMover : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float rotationSpeed;
    private float rotation;
    [SerializeField] float shootingRange;
    [SerializeField] float attackTimer;
    [SerializeField] GameObject beam;
    private GameObject player;
    private Vector3 lastPlayerPos;
    private bool startAttack = false;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>().gameObject;
    }

    private void Update()
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance <= shootingRange && !startAttack)
        {
            startAttack = true;

            rotation = rotationSpeed / 10;

            transform.position = transform.position;
            
            lastPlayerPos = player.transform.position;

            StartCoroutine(StartBeam());
        }
        if(!startAttack) 
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            rotation = rotationSpeed;
        }
        Vector3 direction = (player.transform.position - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, direction);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotation * Time.deltaTime); // Use rotationSpeed for smoother rotation
    }

    private IEnumerator StartBeam()
    {
        yield return new WaitForSeconds(1);
        beam.SetActive(true);
      
        StartCoroutine(StopBeam());
    }

    private IEnumerator StopBeam()
    {
        yield return new WaitForSeconds(attackTimer);
        beam.SetActive(false);
        startAttack = false;
    }
}
