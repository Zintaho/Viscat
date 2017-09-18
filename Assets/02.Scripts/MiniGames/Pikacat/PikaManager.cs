using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PikaManager : MonoBehaviour {

    public CatController CC;

    public GameObject cat;
    public GameObject ready;
    public GameObject go;
    public bool isStart = false;

    void Start()
    {
        Invoke("Ready", 1.5f);
    }

    void Update()
    {
        if (CC.gameOver) isStart = false;
        if (!isStart) return;
    }

    void Ready()
    {
        ready.SetActive(false);
        go.SetActive(true);

        Invoke("GameStart", 0.7f);
    }

    void GameStart()
    {
        go.SetActive(false);
        isStart = true;
    }

}
