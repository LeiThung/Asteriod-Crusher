using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackBtn : MonoBehaviour
{
    [SerializeField] int sceneNum;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(Exitbtn);
    }

    private void Exitbtn()
    {
        SceneManager.LoadScene(sceneNum);
    }
}
