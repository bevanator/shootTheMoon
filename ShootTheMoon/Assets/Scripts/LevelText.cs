using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelText : MonoBehaviour
{
    // Start is called before the first frame update
    public Text level;
    public Text Clevel;
    public Text Progress;
    public Ball ball;
    public GameObject LCPanel;
    public GameObject GOPanel;
    public GameObject PausePanel;
    public GameObject levelText;
    public GameObject PauseButton;
    public GameManager GameManager;
    void Start()
    {
        level.text = "LEVEL " + (SceneManager.GetActiveScene().buildIndex).ToString();
        Progress.text = PlayerPrefs.GetInt("LevelBeaten").ToString()+"/10";
    }

    // Update is called once per frame
    void Update()
    {
        LevelClear();
        PlayerDead();
        GamePaused();


    }

    void LevelClear()
    {
        if (ball.LC)
        {
            Clevel.text = (SceneManager.GetActiveScene().buildIndex).ToString();
            LCPanel.gameObject.SetActive(true);
            levelText.gameObject.SetActive(false);
            PauseButton.gameObject.SetActive(false);
        }
    }

    void PlayerDead()
    {
        if (ball.killed)
        {
            GOPanel.gameObject.SetActive(true);
            levelText.gameObject.SetActive(false);
            PauseButton.gameObject.SetActive(false);
        }

    }

    void GamePaused()
    {
        if (GameManager.gamePaused)
        {
            PausePanel.gameObject.SetActive(true);
            levelText.gameObject.SetActive(false);
        }
        else PausePanel.gameObject.SetActive(false);
    }

    public void ResetPrefs()
    {
        PlayerPrefs.DeleteKey("LevelBeaten");
        Progress.text = "0/10";
    }


}
