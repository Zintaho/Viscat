using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CatController : MonoBehaviour {

    public int coinValue;

    public PikaManager gm;
    public DogController dc;
    public HideController hc;
    public ButtonManager bm;

    private float forwardSpeed = 3.0f;
    public GameObject[] heart;
    public bool gameOver = false;
    public GameObject GOPopup;
    public Text GOCoin;

    public Text coinText;

    int coin = 0;
    int finalCoin = 0;
    int heartNum = 2;    //하트 개수 설정. 하나씩 줄어듦

    bool leftCheck, rightCheck;
    public GameObject cat;
    float speed = 2.0f;

    

	void Update () {
        if (!gm.isStart) return;

        if (bm.pauseState)
            this.GetComponent<Rigidbody2D>().velocity = new Vector3(0.0f, 0.0f);

        this.GetComponent<Rigidbody2D>().velocity = new Vector3(0.0f, forwardSpeed);    //위로 직진
        
        if(dc.dogState && (!hc.hideState))
        {
            heart[heartNum].SetActive(false);
            heartNum--;
        }

        if (heartNum < 0)       //하트 개수 다 줄었을 때
        {
            gameOver = true;
            finalCoin = coin;
            PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") + finalCoin);
            PlayerPrefs.SetInt("EXP", PlayerPrefs.GetInt("EXP") + 20);
            GOCoin.text = finalCoin.ToString();
            GOPopup.SetActive(true);
        }

        if (gameOver)   //게임 오버 시 고양이 속도 멈춤
            this.GetComponent<Rigidbody2D>().velocity = new Vector3(0.0f, 0.0f);
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Coin")
        {
            Destroy(col.gameObject);
            coin += coinValue;
            coinText.text = coin.ToString();
        }
        else if (col.gameObject.tag == "Obstacle")
        {
            Destroy(col.gameObject);
            heart[heartNum].SetActive(false);
            heartNum--;
        }
    }

}
