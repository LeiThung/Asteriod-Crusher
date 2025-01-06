using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpDrops : MonoBehaviour
{
    public List<GameObject> powerUps = new List<GameObject>();
    [SerializeField] int dropRate;   

    public void SpawnPowerUp(Transform pos)
    {
        int i = Random.Range(0, 100);
        if(i <= dropRate)
        {        
            int num = Random.Range(0, powerUps.Count);
            Instantiate(powerUps[num], pos.position, Quaternion.identity);
        }      
    }
}
