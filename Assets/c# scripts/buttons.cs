using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttons : MonoBehaviour
{
  public void restart_level()
    {
        level_2bossplayer.checkpoint = 0;
        LevelEvents.checkpoint = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);   
    }

    public void MenuScreen()
    {
       level_2bossplayer.checkpoint = 0;
        LevelEvents.checkpoint = 0;
        SceneManager.LoadScene("Main Menu");
        //SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
    }

    public static void respawn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
