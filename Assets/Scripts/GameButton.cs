using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.UI;
using TMPro;

internal class GameButton : MonoBehaviour
{
    [SerializeField]
    public TextMeshProUGUI levelText;

    private void Awake()
    {

    }

    public void InitScene()
    {
        var level = Game.Instance.config.level + 1;
        levelText.text = "- " + level.ToString() + " -";
    }

    public void Restart()
    {
        Debug.Log("Restart");
        if(Game.Instance)
        {
            Game.Instance.RestartLevel();
        }
    }

    public void ChooseLevel()
    {
        Debug.Log("Choose Level");
        SceneManager.LoadScene("LevelSelector");
    }

    public void NextLevel()
    {
        Debug.Log("Next Level");
        Game.Instance.config.level++;
        SceneManager.LoadScene("Game");
    }

    public void ShowPanelScore()
    {
        Debug.Log("test");
        foreach (Transform child in gameObject.transform)
        {
            if (child.name == "Panel_Score")
            {
                Debug.Log("has panel score");
                child.gameObject.SetActive(true);
            }            
            if (child.name == "Panel_Game")
            {
                Debug.Log("has panel game");
                child.gameObject.SetActive(false);
            }            
            if (child.name == "Panel_Level")
            {
                Debug.Log("has panel level");
            }
        }
    }
}
