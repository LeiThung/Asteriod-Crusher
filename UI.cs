using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public FixedJoystick joystickRight;
    public FixedJoystick joystickLeft;
    public Button shootBtn;
    public TextMeshProUGUI scoreTxt;
    public Slider fuelSlider;
    public TextMeshProUGUI fueltxt;
    [SerializeField] GameObject pauseScreen;
    [SerializeField] GameObject ingameUI;
    [SerializeField] GameObject heart1;
    [SerializeField] GameObject heart2;
    [SerializeField] GameObject heart3;
    [SerializeField] Slider volumeSlider;
    public Sprite transparentimg;
    [SerializeField] Image powerUpIcon;
    public Slider xpSlider;
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] TextMeshProUGUI deadscoretxt;
    [SerializeField] TextMeshProUGUI enemysKilledtxt;
    [SerializeField] TextMeshProUGUI playTimeTxt;
    public bool gameOver;

    private void Start()
    {
        LoadVolume();
    }

    private void Update()
    {
        if (gameOver)
        {
            Time.timeScale = 0f;
            gameOverScreen.SetActive(true);
            
        }

        AudioListener.volume = volumeSlider.value;
        SaveVolume();
    }

    private void SaveVolume()
    {
        PlayerPrefs.SetFloat("Volume", AudioListener.volume);
    }

    private void LoadVolume()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("Volume");
    }

    public void GameOverUI(int score, int killed, float playTime)
    {
        deadscoretxt.text = "Score: " + score.ToString();
        enemysKilledtxt.text = "Enemy Killed: " + killed.ToString();
        playTimeTxt.text =  "Playtime: " + playTime.ToString();
    }

    public void UpdateHeartUI(int amount)
    {
        if(amount == 3)
        {
            heart3.SetActive(true);
            heart2.SetActive(true);
            heart1.SetActive(true);
        }
        if(amount == 2)
        {
            heart3.SetActive(false);
            heart2.SetActive(true);
            heart1.SetActive(true);
        }
        if(amount == 1)
        {
            heart3.SetActive(false);
            heart2.SetActive(false);
            heart1.SetActive(true);
        }
        if(amount == 0)
        {
            heart3.SetActive(false);
            heart2.SetActive(false);
            heart3.SetActive(false);
        }
    }

    public void SetPowerUpIcon(Sprite sprite)
    {
        powerUpIcon.sprite = sprite;
    }

    public void PauseBtn()
    {
        Time.timeScale = 0f; 
        ingameUI.SetActive(false);
        pauseScreen.SetActive(true);
    }

    public void PlayBtn()
    {
        Time.timeScale = 1f;
        ingameUI.SetActive(true);
        pauseScreen?.SetActive(false);
    }

    public void RestartBtn()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }

    public void BackBtn()
    {
        SceneManager.LoadScene(0);
    }

    public void ExitBtn()
    {
#if UNITY_EDITOR
        // If running in the Unity editor, stop play mode
        UnityEditor.EditorApplication.isPlaying = false;
#else
    // If running in a built game, close the application
    Application.Quit();
#endif
    }
}
