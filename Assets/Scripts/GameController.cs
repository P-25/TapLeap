using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    public GameObject gameOverPanel;

    public TextMeshProUGUI currentScoreText;
    public TextMeshProUGUI bestScoreText;
    public TextMeshProUGUI startText;
    public GameObject logoImage;
    public GameObject musicStart;
    public GameObject musicStop;

     public SoundManager soundManager; 


    int currentScore;

    void Start()
    {
        LoadSavedScore();
        bestScoreText.text = PlayerPrefs.GetInt("BestScore", 0).ToString();
        SetScore();
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            startText.gameObject.SetActive(false);
            logoImage.gameObject.SetActive(false);
        }
    }

    public void CallGameOver()
    {
        StartCoroutine(GameOver());
    }

    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(0.5f);
        int isMusicPlaying = PlayerPrefs.GetInt("MusicPlaying", 1); // Default value of 1 (true)

        // Check if music is playing (1 represents true, 0 represents false)
        if (isMusicPlaying == 1)
        {
            musicStart.gameObject.SetActive(true);
            musicStop.gameObject.SetActive(false);
        }
        else
        {
            musicStart.gameObject.SetActive(true);
            musicStop.gameObject.SetActive(false);
        }
        gameOverPanel.SetActive(true);
        yield break;
    }

    public void Restart()
    {
        SaveCurrentScore(); // Save current score before reloading
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void SaveCurrentScore()
    {
        Debug.Log("currentScore: " + currentScore);
        PlayerPrefs.SetInt("CurrentScore", currentScore);
    }

    void LoadSavedScore()
    {
        int savedScore = PlayerPrefs.GetInt("CurrentScore", 0);
        if (savedScore > 0)
        {
            currentScore = savedScore;
            PlayerPrefs.DeleteKey("CurrentScore"); // Remove the saved score after loading
        }
    }

    public void AddScore()
    {
        currentScore++;
        if(currentScore > PlayerPrefs.GetInt("BestScore", 0))
        {
            PlayerPrefs.SetInt("BestScore", currentScore);
            bestScoreText.text = currentScore.ToString();
        }
        SetScore();
    }

    void SetScore()
    {
        currentScoreText.text = currentScore.ToString();
    }

    public void StopMusic()
    {
        PlayerPrefs.SetInt("MusicPlaying", 0);
        soundManager.PauseMusic(); 
        musicStop.gameObject.SetActive(true);
        musicStart.gameObject.SetActive(false);
    }
    public void PlayMusic()
    {
        PlayerPrefs.SetInt("MusicPlaying", 1);
        soundManager.ResumeMusic(); 
        musicStart.gameObject.SetActive(true);
        musicStop.gameObject.SetActive(false);
    }
  
}
