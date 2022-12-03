using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

internal class MainMenu : MonoBehaviour
{
    public void LoadGame()
    {
        Debug.Log("Load Game");
        SceneManager.LoadScene("Game");
    }    

    public void ChooseLevel()
    {
        Debug.Log("Choose Level");
        SceneManager.LoadScene("LevelSelector");
    }  
    
    public void Setting()
    {
        Debug.Log("Setting");
        SceneManager.LoadScene("Setting");
    }


}
