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
        gameOverPanel.SetActive(true);
        
        yield break;
    }

    public void Restart()
    {
        PlayerPrefs.SetInt("CurrentScore", 0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void SaveAndRestart()
    {
        SaveCurrentScore(); // Save current score before reloading
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void SaveCurrentScore()
    {
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
  
}
