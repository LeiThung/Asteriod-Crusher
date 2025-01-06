using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainUI : MonoBehaviour
{
    public void SinglePlayerBtn()
    {
        SceneManager.LoadScene(2);
    }

    public void ArmoryBtn()
    {
        SceneManager.LoadScene(3);
    }
}
