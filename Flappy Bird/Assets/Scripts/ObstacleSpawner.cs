using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{

    [SerializeField] private float waitTime;
    [SerializeField] private GameObject[] obstaclePrefabs;
    private float tempTime;
    int[] levelOfDifficult = new int[] { 0, 0, 0, 1, 1 };
    int indexLevel = 0;
    int preItem = -1;
    void Start()
    {
        tempTime = waitTime - Time.deltaTime;
    }

    void LateUpdate()
    {
        if (GameManager.Instance.GameState())
        {
            tempTime += Time.deltaTime;
            if (tempTime > waitTime)
            {
                // Wait for some time, create an obstacle, then set wait time to 0 and start again
                tempTime = 0;

                int indexObs = GetIndexObs();
                GameObject pipeClone = Instantiate(obstaclePrefabs[indexObs], transform.position, transform.rotation);

                if (preItem != -1)
                {
                    indexLevel += 1;
                    if (indexLevel >= levelOfDifficult.Length)
                    {
                        indexLevel = 0;
                    }
                }

                preItem = indexObs;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.transform.parent != null)
        {
            Destroy(col.gameObject.transform.parent.gameObject);
        }
        else
        {
            Destroy(col.gameObject);
        }
    }

    int[] indexObs1 = new int[] { 1, 3, 0 };
    // int[] indexObs2 = new int[] { 2, 3, 0 };
    int GetIndexObs()
    {
        if (preItem == -1)
        {
            return Random.Range(0, obstaclePrefabs.Length);
        }
        else
        {
            if (levelOfDifficult[indexLevel] == 0)
            {
                if (levelOfDifficult[indexLevel + 1] == 1)
                {
                    return indexObs1[Random.Range(0, indexObs1.Length)];
                }
                else
                {
                    int[] indexObsReturn;
                    switch (preItem)
                    {
                        case 0:
                        case 1:
                            indexObsReturn = new int[] { 0, 2, 1 };
                            return indexObsReturn[Random.Range(0, indexObsReturn.Length)];
                        case 2:
                            indexObsReturn = new int[] { 0, 2, 1, 3 };
                            return indexObsReturn[Random.Range(0, indexObsReturn.Length)];
                        case 3:
                            indexObsReturn = new int[] { 2, 3 };
                            return indexObsReturn[Random.Range(0, indexObsReturn.Length)];
                    }
                }
            }
            else
            {
                int[] indexObsReturn;
                switch (preItem)
                {
                    // case 0:
                    case 0:
                        // indexObsReturn = new int[] { 0, 2, 1 };
                        return 3;
                    case 1:
                        // indexObsReturn = new int[] { 0, 2, 1, 3 };
                        return 3;
                    case 3:
                        indexObsReturn = new int[] { 1, 0 };
                        return indexObsReturn[Random.Range(0, indexObsReturn.Length)];
                }
                return 2;
            }
        }

        return -1;
    }

    //Kho 2,4 - 4,2 - 1,4 - 4,1
}
