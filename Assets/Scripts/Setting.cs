using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Setting : MonoBehaviour
{
    [SerializeField]
    public TextMeshProUGUI textSFX;
    [SerializeField]
    public TextMeshProUGUI textMusic;
    [SerializeField]
    public TextMeshProUGUI textGraphic;
    [SerializeField]
    public TextMeshProUGUI textFPS;

    [SerializeField]
    public GameConfig gameConfig;

    // Start is called before the first frame update
    void Start()
    {
        ShowText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SFX()
    {
        int length = Enum.GetNames(typeof(Config.SFX)).Length - 1;
        gameConfig.sfx++;
        if ((int)gameConfig.sfx > length)
        {
            gameConfig.sfx = gameConfig.sfx - length - 1;
        }
        ShowText();
    }

    public void Music()
    {
        int length = Enum.GetNames(typeof(Config.Music)).Length - 1;
        gameConfig.music++;
        if ((int)gameConfig.music > length)
        {
            gameConfig.music = gameConfig.music - length - 1;
        }
        ShowText();
    }

    public void Graphic()
    {
        int length = Enum.GetNames(typeof(Config.Graphic)).Length - 1;
        gameConfig.graphic++;
        if ((int)gameConfig.graphic > length)
        {
            gameConfig.graphic = gameConfig.graphic - length - 1;
        }
        ShowText();
    }

    public void FPS()
    {
        gameConfig.fps += 30;
        if ((int)gameConfig.fps > 120)
        {
            gameConfig.fps = Config.FPS.Low;
        }
        ShowText();
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    private void ShowText()
    {
        textSFX.text = "SFX: " + gameConfig.sfx.ToString();
        textMusic.text = "Music: " + gameConfig.music.ToString();
        textGraphic.text = "Graphic: " + gameConfig.graphic.ToString();
        textFPS.text = "FPS: " + gameConfig.fps.ToString();
    }
}
