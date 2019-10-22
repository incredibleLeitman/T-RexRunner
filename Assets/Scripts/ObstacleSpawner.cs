using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    static ObstacleSpawner instance;
    GameObject[] obstacles;

    void Awake ()
    {
        instance = this;
        obstacles = new GameObject[3];
        for (int i = 0; i < 3; i++)
        {
            obstacles[i] = (GameObject)Resources.Load("Prefabs/Cactus" + (i + 1));
        }
    }

    public static ObstacleSpawner Instance ()
    {
        return instance;
    }

    public GameObject SpawnObstacle ()
    {
        GameObject obj = Instantiate(obstacles[Random.Range(0, 3)]);
        Destroy(obj, 5); // TODO: better use for object pool or destroy on position check
        return obj;
    }
}
