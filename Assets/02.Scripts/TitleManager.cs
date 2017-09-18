using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class TitleManager : MonoBehaviour
{
    AudioSource audio;

    void Awake()
    {
        audio = GetComponent<AudioSource>();
    }

    void Start()
    {
        audio.PlayDelayed(0.9f);
        Invoke("SceneChange",1.1f);

    }

    void SceneChange()
    {
        SceneManager.LoadScene("Start");
    }

}
