using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    bool m_onGround = true;
    bool m_isJumping = false;
    float m_jumpCounter = 0;
    Rigidbody m_rigid = null;
    int m_layerGround = 0;
    int m_layerObstacle = 0;

    public float jumpSpeed = 6f;
    public AudioSource audioSource;
    public AudioClip audioClipJump;
    public AudioClip audioClipHit;

    // Start is called before the first frame update
    void Start ()
    {
    }

    void Awake ()
    {
        m_rigid = GetComponent<Rigidbody>();
        //m_layerGround = LayerMask.GetMask("Level");
        m_layerGround = LayerMask.NameToLayer("Level");
        m_layerObstacle = LayerMask.NameToLayer("Obstacles");
    }

    // Update is called once per frame
    void Update()
    {
        //m_rigid.useGravity = false;
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    m_rigid.velocity += Vector3.up * jumpSpeed;
        //}

        //if (m_rigid.velocity.y < 0)
        //{
        //    m_rigid.velocity += Vector3.up * Physics2D.gravity.y * (jumpSpeed - 1) * Time.deltaTime;
        //}
        //else if (m_rigid.velocity.y > 0 && !Input.GetKeyDown(KeyCode.Space))
        //{
        //    m_rigid.velocity += Vector3.up * Physics2D.gravity.y * (jumpSpeed - 0.5f - 1) * Time.deltaTime;
        //}

        if (m_onGround && Input.GetKeyDown(KeyCode.Space))
        {
            audioSource.clip = audioClipJump;
            audioSource.Play();

            //m_rigid.AddForce(new Vector3(0, 1, 0) * jumpSpeed, ForceMode.Impulse);
            m_rigid.velocity = Vector3.up * jumpSpeed;
            m_isJumping = true;
            m_jumpCounter = 0.3f;
            m_onGround = false;
        }

        if (Input.GetKey(KeyCode.Space) && m_isJumping == true)
        {
            if (m_jumpCounter > 0)
            {
                m_rigid.velocity = Vector3.up * jumpSpeed;
                m_jumpCounter -= Time.deltaTime;
            }
            else
            {
                m_isJumping = false;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            m_rigid.velocity = Vector3.down * jumpSpeed;
            m_isJumping = false;
        }
    }

    void OnCollisionEnter (Collision col)
    {
        if (col.gameObject.layer == m_layerObstacle)
        {
            audioSource.clip = audioClipHit;
            audioSource.Play();

            // stop the game
            Debug.Log("GameOver!");
            Debug.Log("Hit " + col.gameObject.name);

            //Time.timeScale = 0; // this will freeze the game, stop

            //Application.Quit(); // game exit

            //Application.LoadLevel(Application.loadedLevel); // or reload current level
        }
        else if (//col.gameObject.tag == ("Ground") // use layer instead of Tags -> better performance
                 col.gameObject.layer == m_layerGround &&
                 m_onGround == false)
        {
            m_onGround = true;
        }
    }
}
