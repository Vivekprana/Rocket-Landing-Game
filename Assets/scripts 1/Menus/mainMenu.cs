using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    public GameObject mainMenuButtons;
    public GameObject levelMenu;

    void Start ()
    {
        if (levelMenu != null)
        {
            levelMenu.SetActive(false);
        }
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void returnToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void replayLevel()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.buildIndex);
    }

    public void nextLevel()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (SceneManager.sceneCountInBuildSettings > scene.buildIndex)
        {
           
            SceneManager.LoadScene(scene.buildIndex + 1);
        }

        
    }
    public void levelsMenu()
    {
        mainMenuButtons.SetActive(false);
        levelMenu.SetActive(true);
    }

    public void exitlevelsMenu()
    {
        levelMenu.SetActive(false);
        mainMenuButtons.SetActive(true);

    }
}
