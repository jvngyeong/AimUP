using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetCtrl : MonoBehaviour
{
    private Transform Left_Up;
    private Transform Left_Down;
    private Transform Right_Up;
    private Transform Right_Down;
    private Transform Left_Middle;
    private Transform Right_Middle;
    private Transform Center_Up;
    private Transform Center_Down;
    public Transform[] transformArray;
    public static float speed = 3f;
    private int random_End;
    void Awake()
    {
        Left_Up = GameObject.Find("Left_Up").transform;
        Left_Down = GameObject.Find("Left_Down").transform;
        Right_Up = GameObject.Find("Right_Up").transform;
        Right_Down = GameObject.Find("Right_Down").transform;
        Left_Middle = GameObject.Find("Left_Middle").transform;
        Right_Middle = GameObject.Find("Right_Middle").transform;
        Center_Up = GameObject.Find("Center_Up").transform;
        Center_Down = GameObject.Find("Center_Down").transform;
        transformArray = new Transform[8];
        transformArray[0] = Left_Up;
        transformArray[1] = Left_Down;
        transformArray[2] = Right_Up;
        transformArray[3] = Right_Down;
        transformArray[4] = Left_Middle;
        transformArray[5] = Right_Middle;
        transformArray[6] = Center_Up;
        transformArray[7] = Center_Down;
    }

    void Start()
    {
        random_End = UnityEngine.Random.Range(0, 4);
    }

    void Update()
    {
        while (transformArray[random_End].position == this.transform.position)
        {
            random_End = UnityEngine.Random.Range(0, 4);
        }
        this.transform.position = Vector3.MoveTowards(this.transform.position, transformArray[random_End].position, speed * Time.deltaTime);
        if (this.transform.position == transformArray[random_End].position)
        {
            gameObject.SetActive(false);
            Fire.MissCount += 1;    // ��ģ ����
        }
    }

}
