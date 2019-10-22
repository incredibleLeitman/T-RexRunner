using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    bool m_start = false;
    float m_distance = 0;
    float m_next = 2;
    float m_startPosX = 0;
    float m_length = 0;
    int m_scoreCount = 0;
    GameObject m_light = null;

    public static int PlayerScore = 0;
    public float scrollSpeed = 10f;
    public AudioSource audioSource;
    public AudioClip audioClipScore;
    public Text textScore;
    public Text textHScore;

    void Start()
    {
        m_startPosX = transform.position.x;
        m_light = GameObject.Find("Directional Light");
        audioSource.clip = audioClipScore;

        int hScore = PlayerPrefs.GetInt("highscore");
        if (hScore > 0)
            textHScore.text = "highscore: " + hScore;
    }

    void FixedUpdate ()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            m_start = true;
        }

        if (m_start == false) return;

        //Debug.Log("fixedTime " + Time.fixedTime);
        //Debug.Log("currentTime: " + Time.time);
        //Debug.Log("deltaTime: " + Time.deltaTime);

        // day and night cycle
        m_light.transform.RotateAround(Vector3.zero, Vector3.forward, 2 * Time.deltaTime);

        // auto move
        float delta = scrollSpeed * Time.deltaTime;
        transform.position += (Vector3.left * delta);
        if (transform.position.x < m_startPosX - m_length / 2)
        {
            transform.position = new Vector3(transform.position.x + m_length, transform.position.y, transform.position.z);
        }
        m_distance += delta;
    }

    void Update ()
    {
        if (m_start == false) return;

        // score
        int score = (int)(m_distance * 10);
        //GetComponentInChildren<Text>().text = "score: " + score;
        textScore.text = "score: " + score;
        PlayerScore = score;

        // set difficulty
        scrollSpeed = (score / 1000f) + 10f;

        // play sound
        score /= 1000;
        if (score > m_scoreCount)
        {
            m_scoreCount = score;
            audioSource.Play();
        }

        // spawn obsticles
        if (Time.time > m_next)
        {
            int count = Random.Range(1, 4);
            for (int i = 0; i < count; ++i)
            {
                GameObject obstacle = ObstacleSpawner.Instance().SpawnObstacle();
                obstacle.transform.position += Vector3.right * (20.0f + i*2);
                obstacle.transform.SetParent(transform, true);
            }
            m_next = Time.time + 4;
        }
    }
}
