using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    public List<GameObject> backGrounds = new List<GameObject>();

    void Start()
    {
        int i = Random.Range(0, backGrounds.Count);
        Instantiate(backGrounds[i], transform.position, Quaternion.identity);
    }
}
