using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    public int level;

    public GameConfig gameConfig;

    public void ChooseLevel()
    {
        gameConfig.level = level; 
        Debug.Log("Level is " + gameConfig.level);
        SceneManager.LoadScene("Game");
    }

    public void BackToMenu()
    {
        Debug.Log("Back to menu");
        SceneManager.LoadScene("Menu");
    }
}
