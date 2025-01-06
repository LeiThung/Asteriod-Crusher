using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnitMover : MonoBehaviour
{
    [SerializeField] float chargeRange;
    [SerializeField] float speed;
    private GameObject player;
    private bool charging = false;
    private bool startCharge = false;
    private bool posCap = false;
    private Vector3 direction;

    void Start()
    {
        player = FindObjectOfType<PlayerController>().gameObject;
    }

    private void Update()
    {
        if(player != null)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);

            if (distance > chargeRange && !startCharge)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            }
            else if (!startCharge)
            {
                StartCoroutine(StartCharge());
            }

            if (startCharge) transform.position = transform.position;

            if (charging)
            {
                if (!posCap)
                {
                    posCap = true;
                    direction = (player.transform.position - transform.position).normalized;
                }
                transform.position += direction * (speed * 2.5f) * Time.deltaTime;
            }
        }      
    }

    private IEnumerator StartCharge()
    {
        startCharge = true;
        yield return new WaitForSeconds(2f);
        charging = true;
        yield return new WaitForSeconds(0.8f);
        charging = false;
        startCharge = false;
        posCap = false;
    }
}
