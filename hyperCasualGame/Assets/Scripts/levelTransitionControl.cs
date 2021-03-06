using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class levelTransitionControl : MonoBehaviour
{
    public Canvas nextLevelUI;
    public Canvas gameOverUI;
    private void Start()
    {
        nextLevelUI.enabled = false;
        gameOverUI.enabled = false; 
        if(PlayerPrefs.GetInt("finishLevel") == 0)
        {
            gameOverUI.enabled = true;
            gameOverUI.GetComponent<AudioSource>().Play();
        }
        else
        {
            nextLevelUI.enabled = true; 
            nextLevelUI.GetComponent<AudioSource>().Play();
        }
    }
 
    public void backToTheMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void goLevel()
    {
        SceneManager.LoadScene(1);
    }
}
