using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class mainMenuControl : MonoBehaviour
{
    public Text levelText; 
    void Start()
    {
        levelText.text = "LEVEL " + PlayerPrefs.GetInt("level");
    }

    public void goLevel(int level)
    {
        if (level == 0)
        {
            PlayerPrefs.SetInt("level",1);
        }

        SceneManager.LoadScene(1);

    }
    public void quitGame()
    {
        Application.Quit();
    }
}
