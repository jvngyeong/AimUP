using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Experimental.Rendering;

public class Fire : MonoBehaviour
{
    public GameObject Target;
    private AudioSource audioSource;
    private Text HitText;
    public static Text MissText;
    private Text CurrentTimeText;
    public static float MissCount = 0;
    public static float HitCount = 0;
    public float Accuracy;
    public float CurrentTime = 0;
    public GameObject GameManager;
    private int PrevExecuteTime = 0;

    void Start()
    {
        Accuracy = 0;
        HitText = GameObject.Find("HitText").GetComponent<Text>();
        MissText = GameObject.Find("MissText").GetComponent<Text>();
        CurrentTimeText = GameObject.Find("CurrentTime").GetComponent<Text>();
        audioSource = this.gameObject.GetComponent<AudioSource>();
        audioSource.mute = true;
    }
    void Update()
    {
        CurrentTime += Time.deltaTime;
        CurrentTimeText.text = FormatTime(CurrentTime);
        MissText.text = "Miss : " + MissCount.ToString();
        HitText.text = "Hit : " + HitCount.ToString();
        if (Input.GetMouseButtonDown(0))
        {
            FireRay();
        }
        if(CurrentTime > 60.0f)
        {
            SceneManager.LoadScene("Result");
        }
        int SaveTime = Mathf.FloorToInt(CurrentTime);
        if (SaveTime % 10 == 0 && SaveTime != 0 && SaveTime != PrevExecuteTime)
        {
            if(HitCount + MissCount != 0)
            {
                Accuracy = HitCount / (HitCount + MissCount) * 100;
                PlayerPrefs.SetFloat($"{SaveTime}Acc", Accuracy);
                Debug.Log(PlayerPrefs.GetFloat($"{SaveTime}Acc"));
                PrevExecuteTime = SaveTime;
            }
        }
    }

    void FireRay()
    {
        audioSource.mute = false;
        audioSource.Play();
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity))
        {
            Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.blue);
            Debug.Log(hit.collider.gameObject);
            if (hit.collider.gameObject.tag == "Target")
            {
                HitCount += 1;
                hit.collider.gameObject.SetActive(false);
                GameManager.SendMessage("DestroyTarget", SendMessageOptions.DontRequireReceiver);
            }
            else
            {
                MissCount += 1;
            }
        }
    }
    string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60F);
        int seconds = Mathf.FloorToInt(time - minutes * 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}