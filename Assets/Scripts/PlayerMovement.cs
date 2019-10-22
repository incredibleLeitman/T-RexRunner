using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    bool m_onGround = false;
    bool m_isJumping = false;
    bool m_start = false;
    float m_jumpCounter = 0;
    Rigidbody m_rigid = null;
    int m_layerGround = 0;
    int m_layerObstacle = 0;
    ParticleSystem m_particles = null;

    public float jumpSpeed = 20f;
    public AudioSource audioSource;
    public AudioClip audioClipJump;
    public AudioClip audioClipHit;
    public GameObject restartButton;

    void Awake ()
    {
        m_rigid = GetComponent<Rigidbody>();
        //m_layerGround = LayerMask.GetMask("Level");
        m_layerGround = LayerMask.NameToLayer("Level");
        m_layerObstacle = LayerMask.NameToLayer("Obstacles");
        m_particles = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        //m_rigid.useGravity = false;
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    m_start = true; // start after first jump

        //    audioSource.clip = audioClipJump;
        //    audioSource.Play();

        //    m_rigid.velocity += Vector3.up * jumpSpeed;
        //    m_onGround = false;
        //}

        if (m_onGround && Input.GetKeyDown(KeyCode.Space))
        {
            m_start = true; // start after first jump

            audioSource.clip = audioClipJump;
            audioSource.Play();

            //m_rigid.AddForce(new Vector3(0, 1, 0) * jumpSpeed, ForceMode.Impulse);
            m_rigid.velocity = Vector3.up * jumpSpeed;
            m_isJumping = true;
            m_jumpCounter = 0.4f;
            m_onGround = false;
        }
        else if (Input.GetKey(KeyCode.Space) && m_isJumping == true)
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
        else if (Input.GetKeyUp(KeyCode.Space) || m_isJumping == false)
        {
            m_rigid.velocity = Vector3.down * jumpSpeed * 0.8f; // * 1.5f;
            m_isJumping = false;
        }

        if (m_start)
        {
            if (m_onGround)
            {
                if (m_particles.isPlaying == false)
                {
                    m_particles.Play();
                }
            }
            else if (m_particles.isPlaying)
            {
                m_particles.Stop();
            }
        }
    }

    void OnCollisionEnter (Collision col)
    {
        if (col.gameObject.layer == m_layerObstacle)
        {
            //Debug.Log("collision with " + col.gameObject.name);

            //audioSource.clip = audioClipHit;
            //audioSource.Play(); // does not play if timeScale is set so zero
            AudioSource.PlayClipAtPoint(audioClipHit, Camera.main.transform.position, 1.0f);

            restartButton.SetActive(true); // false to hide, true to show

            // set highscore
            if (LevelController.PlayerScore > PlayerPrefs.GetInt("highscore"))
                PlayerPrefs.SetInt("highscore", LevelController.PlayerScore);

            // stop the game
            //Application.Quit(); // game exit
            Time.timeScale = 0; // this will freeze the game, stop
        }
        else if (//col.gameObject.tag == ("Ground") // use layer instead of Tags -> better performance
                 col.gameObject.layer == m_layerGround &&
                 m_onGround == false)
        {
            m_onGround = true;
        }
    }
}
