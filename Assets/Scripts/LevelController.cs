using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    float m_distance = 0;
    float m_next = 2;
    bool m_start = false;
    GameObject m_light = null;

    public float scrollSpeed = 10;
    public AudioSource audioSource;
    public AudioClip audioClipScore;

    // Start is called before the first frame update
    void Start()
    {
        m_light = GameObject.Find("Directional Light");
        audioSource.clip = audioClipScore;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            m_start = true;

        if (m_start == false) return;

        //Debug.Log("currentTime: " + Time.time);
        //Debug.Log("deltaTime: " + Time.deltaTime);

        // day and night cycle
        m_light.transform.RotateAround(Vector3.zero, Vector3.forward, 2 * Time.deltaTime);

        transform.position += (Vector3.left * scrollSpeed * Time.deltaTime);
        m_distance += scrollSpeed * Time.deltaTime;

        int score = ((int)m_distance) * 10;
        Text textScore = GetComponentInChildren<Text>();
        textScore.text = "score: " + score;
        if (score % 1000 == 0)
            audioSource.Play();

        // set difficulty
        scrollSpeed = (score / 1000) + 10;

        // spawn obsticles
        if (Time.time > m_next)
        {
            //if (Random.Range(0, 100) > 80)
            int count = Random.Range(1, 4);
            Debug.Log(Time.time + " spawning " + count + "items...");
            for (int i = 0; i < count; ++i)
            {
                GameObject obstacle = ObstacleSpawner.Instance().SpawnObstacle();
                obstacle.transform.position += Vector3.right * (60.0f + i*2);
                obstacle.transform.SetParent(transform, true);
            }
            m_next += 4;
        }
    }
}
