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
        return obj;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}
