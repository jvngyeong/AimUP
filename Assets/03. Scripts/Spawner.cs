using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public PoolManager poolManager;
    public GameObject[] targetPrefabs;
    public float spawnInterval = 2f; // 타겟 생성 간격

    // 빈 오브젝트들의 참조를 설정합니다.
    public Transform Left_Up;
    public Transform Left_Down;
    public Transform Right_Up;
    public Transform Right_Down;
    public Transform Left_Middle;
    public Transform Right_Middle;
    public Transform Center_Up;
    public Transform Center_Down;

    void Start()
    {
        StartCoroutine(SpawnTargets());
    }

    IEnumerator SpawnTargets()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            int index = Random.Range(0, targetPrefabs.Length);
            GameObject target = poolManager.Get(index);
            SetTargetPosition(target);
        }
    }

    void SetTargetPosition(GameObject target)
    {
        int positionIndex = Random.Range(0, 8);
        switch (positionIndex)
        {
            case 0:
                target.transform.position = Left_Up.position;
                break;
            case 1:
                target.transform.position = Left_Down.position;
                break;
            case 2:
                target.transform.position = Right_Up.position;
                break;
            case 3:
                target.transform.position = Right_Down.position;
                break;
            case 4:
                target.transform.position = Left_Middle.position;
                break;
            case 5:
                target.transform.position = Right_Middle.position;
                break;
            case 6:
                target.transform.position = Center_Up.position;
                break;
            case 7:
                target.transform.position = Center_Down.position;
                break;
        }
    }
}
