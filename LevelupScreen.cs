using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelupScreen : MonoBehaviour
{
    public List<GameObject> buttons = new List<GameObject>();
    public List<GameObject> btnSpots = new List<GameObject>();
    private List<GameObject> activeBtns = new List<GameObject>();

    public void LevelUP()
    {
        foreach (GameObject spot in btnSpots)
        {
            int i = Random.Range(0, buttons.Count);
            GameObject btn = Instantiate(buttons[i], spot.transform.position, Quaternion.identity);
            btn.transform.parent = this.transform;
            activeBtns.Add(btn);
        }
    }

    public void ClearBtns()
    {
        foreach(GameObject btn in activeBtns)
        {
            Destroy(btn);
        }
    }
}
