using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PowerUpRotator : MonoBehaviour
{
    [SerializeField] float rotationSpeed;
    [SerializeField] Transform pivit;

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(pivit.position, Vector3.forward, rotationSpeed * Time.deltaTime);
    }
}
