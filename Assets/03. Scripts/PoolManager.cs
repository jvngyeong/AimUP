using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public GameObject[] prefabs;
    List<GameObject>[] pools;

    public GameObject GameManager;

    void Awake()
    {
        GameManager.SendMessage("LevelControl", SendMessageOptions.DontRequireReceiver);

        pools = new List<GameObject>[prefabs.Length];

        for (int index = 0; index < pools.Length; index++)
        {
            pools[index] = new List<GameObject>();
        }
    }

    // Vector3 scale 매개변수를 추가해 크기를 조절할 수 있도록 합니다.
    public GameObject Get(int index)
    {
        GameObject select = null;

        foreach (GameObject item in pools[index])
        {
            if (!item.activeSelf)
            {
                select = item;
                // 선택된 오브젝트의 크기를 조절합니다.
                select.transform.localScale = new Vector3(StartGame.currentwidth,0.1f,StartGame.currentheight);
                select.SetActive(true);
                break;
            }
        }

        if (select == null)
        {
            select = Instantiate(prefabs[index], transform);
            // 생성되는 새 오브젝트의 크기를 설정합니다.
            select.transform.localScale = new Vector3(StartGame.currentwidth,0.1f,StartGame.currentheight);
            pools[index].Add(select);
        }
        return select;
    }
}
