using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class Main_Menu : MonoBehaviour
{
    public static float volumemaster = 1;
    private void Awake()
    {
        
        Time.timeScale = 1;
        static_variable.checkpointtoload = 0;
    }

    public void LoadTutorial()
    {
        static_variable.time_played = 0;
        static_variable.times_ded = 0;
        SceneManager.LoadScene("tutorial");
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
    }

    public void Loadlevel1()
    {
        static_variable.time_played = 0;
        static_variable.times_ded = 0;
        SceneManager.LoadScene("level1");
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
    }

    public void LoadLevel2()
    {
        static_variable.time_played = 0;
        static_variable.times_ded = 0;
        SceneManager.LoadScene("level2Boss");
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
    }
    public void Volume(float volume)
    {
        volumemaster = volume;
    }
}
