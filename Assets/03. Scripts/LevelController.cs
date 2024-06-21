using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{

    // Start is called before the first frame update
    void LevelControl()
    {
        int CurrentLevel = PlayerPrefs.GetInt("Level");

        Fire.HitCount = 0;
        Fire.MissCount = 0;

        switch (CurrentLevel)
        {
            case 1:
                TargetCtrl.speed = 2f;
                StartGame.currentwidth = 1.2f;
                StartGame.currentheight = 1.2f;
                break;
            case 2:
                TargetCtrl.speed = 3f;
                StartGame.currentwidth = 1.0f;
                StartGame.currentheight = 1.0f;
                break;
            case 3:
                TargetCtrl.speed = 4f;
                StartGame.currentwidth = 0.8f;
                StartGame.currentheight = 0.8f;
                break;
            default:
                break;
        }
    }

}
