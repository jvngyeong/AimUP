using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public GameObject AudioSource;
    public void Awake()
    {
        AudioSource = GameObject.Find("Audio Source");
    }
    
    public void LevelScene()
    {
        SceneManager.LoadScene("Level Select");
    }

    public void SettingScene()
    {
        SceneManager.LoadScene("Setting");
    }

    public void LobbyScene()
    {
        SceneManager.LoadScene("Lobby");
    }

    public void Exit()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit(); // ���ø����̼� ����
        #endif
    }
    public void StopMusic()
    {
        Destroy(AudioSource);
    }
    public void EasyScene()
    {
        SceneManager.LoadScene("Game");
        PlayerPrefs.SetInt("Level",1);
        PlayerPrefs.SetInt("Round",0);

    }
        public void NormalScene()
    {
        SceneManager.LoadScene("Game");
        PlayerPrefs.SetInt("Level",2);
        PlayerPrefs.SetInt("Round",0);
    }
        public void HardScene()
    {
        SceneManager.LoadScene("Game");
        PlayerPrefs.SetInt("Level",3);
        PlayerPrefs.SetInt("Round",0);
    }
        public void GPTScene()
    {
        SceneManager.LoadScene("Game");
        PlayerPrefs.SetInt("Level",4);
    }

}
