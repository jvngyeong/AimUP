using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
    private Text HitCount;
    private Text MissCount;
    private Text Score;
    private Text Round;
    private Text Size;
    private Text Speed;

    int RoundCount;

    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        HitCount = GameObject.Find("HitCount").GetComponent<Text>();
        MissCount = GameObject.Find("MissCount").GetComponent<Text>();
        Score = GameObject.Find("Score").GetComponent<Text>();
        Round = GameObject.Find("RoundCount").GetComponent<Text>();
        Size = GameObject.Find("Size").GetComponent<Text>();
        Speed = GameObject.Find("Speed").GetComponent<Text>();
        RoundCount = PlayerPrefs.GetInt("Round");
        
    }

    // Update is called once per frame
    void Update()
    {
        HitCount.text = Fire.HitCount.ToString();
        MissCount.text = Fire.MissCount.ToString();
        Score.text = (Fire.HitCount * 100 - Fire.MissCount * 10).ToString();
        Round.text = RoundCount.ToString();
        Speed.text = TargetCtrl.speed.ToString();
        Size.text = (StartGame.currentwidth * 100).ToString();
    }
}
