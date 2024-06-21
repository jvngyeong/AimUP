using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartGame : MonoBehaviour
{
    public static float currentwidth = 1.0f;
    public static float currentheight = 1.0f; 
    public void Start()
    {
        SceneManager.LoadScene("Lobby");
    }
}
