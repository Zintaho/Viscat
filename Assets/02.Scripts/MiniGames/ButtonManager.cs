using UnityEngine;
using System.Collections;

public class ButtonManager : MonoBehaviour
{

    public GameObject pausePop;
    public bool pauseState = false;

    bool pauseOn = false;

    //int 현재코인 = (CT_GM 스크립트에서 불러온 코인) 

    public void Start()
    {
        pauseOn = false;
        Time.timeScale = 1f;
    }

    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            pauseOn = !pauseOn;

            if (!pauseOn)
            {
                Pause();
            }
            else if(pauseOn)
            {
                Continue();
            }
        }
    }

    public void Pause()
    {
        pausePop.SetActive(true);
        pauseState = true;
        Time.timeScale = 0f;
    }

    public void Continue()
    {
        pausePop.SetActive(false);
        pauseState = false;
        Time.timeScale = 1f;
    }

    public void Playground()
    {
        //PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") + 현재코인);
        //PlayerPrefs.SetInt("TowerScore", ,...);
        Application.LoadLevel("SelectGame");
    }

    public void Retry()
    {
        //PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") + 현재코인);
        //PlayerPrefs.SetInt("TowerScore", ,...);
        Application.LoadLevel(Application.loadedLevel);
    }

    public void ToMain()
    {
        Application.LoadLevel("Main");
    }

    public void DeadContinue()
    {
        //루비 1감소
    }

}
