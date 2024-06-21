using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reset : MonoBehaviour
{
    void OnApplicationQuit()
    {
        PlayerPrefs.DeleteAll();
    }
}
