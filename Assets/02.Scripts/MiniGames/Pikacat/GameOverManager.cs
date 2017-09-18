using UnityEngine;
using System.Collections;

public class GameOverManager : MonoBehaviour {

    public void Stop()
    {
        Application.LoadLevel("SelectGame");
    }

    public void Retry()
    {
        Application.LoadLevel("PikaCatScene");
    }

    public void DeadContinue()
    {
        //루비 1감소
    }
}
