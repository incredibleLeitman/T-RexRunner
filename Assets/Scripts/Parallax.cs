using UnityEngine;

public class Parallax : MonoBehaviour
{
    float m_length, m_startPosX;

    public GameObject anchor;
    public float scrollSpeed;

    void Start()
    {
        m_startPosX = transform.position.x;
        m_length = GetComponent<SpriteRenderer>().bounds.size.x;
    }
    
    void FixedUpdate()
    {
        float dist = (anchor.transform.position.x * scrollSpeed)%m_length;
        if (dist != 0)
        {
            if (transform.position.x < m_startPosX - m_length)
            {
                dist += m_length;
            }
            transform.position = new Vector3(m_startPosX + dist, transform.position.y, transform.position.z);
        }
    }
}
