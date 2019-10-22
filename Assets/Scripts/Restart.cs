using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Restart : MonoBehaviour
{
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(TaskOnButtonClick);
    }

    void TaskOnButtonClick ()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("T-Rex Runner");
    }
}
