using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CT_GM : MonoBehaviour
{
    public GetCoin getCoin;

    public Text goCoinText;

    public GameObject Cat;
    //SpriteRenderer Cspr;
    public Sprite[] Csp = new Sprite[3];

    public GameObject[] shelf = new GameObject[20];
    public GameObject[] shelfJoint = new GameObject[3];

    Vector2[] partition = new Vector2[3]; //왼쪽 가운데 오른쪽
    int partIndex;

    float h = 1.5f;

    Vector2[] shelfPos = new Vector2[19]; // 실제 선반이 생기는 위치

    int nowIndex;
    int nextIndex;
    int catPos;

    int[] life = new int[3]; //하트
    public GameObject[] heart = new GameObject[3];

    public GameObject GO;

    public GameObject[] Coin = new GameObject[6];//게임 상에 나타날 코인
    bool[] coinActive = new bool[6];

    public GameObject ready;
    public GameObject go;
    public bool isStart = false;

    private float halfWidth;

    // Use this for initialization
    void Start()
    {
        nextIndex = 0;
        catPos = -1;

        halfWidth = Screen.width * 0.5f;

        for (int i = 0; i < 3; i++)
            life[i] = 1;

        nowIndex = 0;
        partition[0] = new Vector2(-1.9f, -2.8f);
        partition[1] = new Vector2(0f, -2.8f);
        partition[2] = new Vector2(1.9f, -2.8f);

        //0번째 선반 생성
        partIndex = Random.Range(0, 2);
        if (partIndex == 0)
        {
            shelfPos[0] = partition[0];
            shelf[0].transform.position = shelfPos[0];
        }

        else if (partIndex == 1)
        {
            partIndex = 2;
            shelfPos[0] = partition[2];
            shelf[0].transform.position = shelfPos[0];
        }

        //선반 10개 배치
        nowIndex = 1;
        for (; nowIndex < 10; nowIndex++)
        {
            if (partIndex == 0)
            {
                partIndex = Random.Range(1, 3);
                shelfPos[nowIndex] = new Vector2(partition[partIndex].x, shelfPos[nowIndex - 1].y + h);
                shelf[nowIndex].transform.position = shelfPos[nowIndex];
            }
            else if (partIndex == 1)
            {
                int testIndex = Random.Range(1, 3);
                if (testIndex == 1) partIndex = 0;
                else partIndex = 2;
                shelfPos[nowIndex] = new Vector2(partition[partIndex].x, shelfPos[nowIndex - 1].y + h);
                shelf[nowIndex].transform.position = shelfPos[nowIndex];
            }
            else
            {
                partIndex = Random.Range(0, 2);
                shelfPos[nowIndex] = new Vector2(partition[partIndex].x, shelfPos[nowIndex - 1].y + h);
                shelf[nowIndex].transform.position = shelfPos[nowIndex];
            }
        }
        //nowIndex = 10로 끝남
        //partIndex는 인덱스 9인 선반일때의 값으로 끝남
        Invoke("Ready", 1.5f);  //Ready() 메소드로
    }

    Vector2 tPos;
    float tPosX;
    void Update()
    {
        
        float Cy = 0.9f;
        if (!isStart) return;       //isStart 상태 아니면 return

        bool test = true;
        
        if (Input.touchCount > 0)
        {
            tPos = Input.GetTouch(0).position;
            tPosX = tPos.x - halfWidth;
        }
        else
        {
            test = false;
        }

        if (test == true && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            //int nextIndex = 0;
            if (shelf[nextIndex].transform.position.x < Cat.transform.position.x) // 왼쪽으로 가야할 때
            {
                if (tPosX < 0)
                {
                    //Cspr.sprite = Csp[0];
                    Cat.transform.position = new Vector2(shelfPos[nextIndex].x, shelfPos[nextIndex].y + Cy);
                    nextIndex++;
                    catPos++;
                    if (catPos == 3 || catPos == 13)
                        CreateShelf();
                }
                else if (tPosX > 0)
                {
                    //Cspr.sprite = Csp[1];
                    if (life[0] == 1)
                    {
                        life[0] = 0;
                        heart[0].SetActive(false);
                    }
                    else if (life[1] == 1)
                    {
                        life[1] = 0;
                        heart[1].SetActive(false);
                    }
                    else if (life[2] == 1)
                    {
                        life[2] = 0;
                        heart[2].SetActive(false);
                        Debug.Log("GameOver");
                        Gameover();
                    }
                }
            }
            else if (shelf[nextIndex].transform.position.x > Cat.transform.position.x) // 현재 고양이 위치에서 이동해야 할 다음 선반이 오른쪽일 때
            {
                if (tPosX > 0)
                {
                    //Cspr.sprite = Csp[0];
                    Cat.transform.position = new Vector2(shelfPos[nextIndex].x, shelfPos[nextIndex].y + Cy);
                    nextIndex++;
                    catPos++;
                    if (catPos == 3 || catPos == 13)
                        CreateShelf();
                }
                else if (tPosX < 0)
                {
                    //Cspr.sprite = Csp[1];
                    if (life[0] == 1)
                    {
                        life[0] = 0;
                        heart[0].SetActive(false);
                    }
                    else if (life[1] == 1)
                    {
                        life[1] = 0;
                        heart[1].SetActive(false);
                    }
                    else if (life[2] == 1)
                    {
                        life[2] = 0;
                        heart[2].SetActive(false);
                        Debug.Log("GameOver");
                        Gameover();
                    }
                }
            }
        }
        if (nextIndex > 18) nextIndex = 0;
        if (catPos > 18) catPos = 0;
    }

    int j;
    int k;
    void CreateShelf()
    {
        if (catPos == 3)
        {
            j = 10;
            k = 19;
        }
        else if (catPos == 13)
        {
            j = 0;
            k = 10;
        }
        for (int i = j; i < k; i++)
        {
            if (partIndex == 0) // partIndex는 9번째 선반 생성할 때의 값
            {
                partIndex = Random.Range(1, 3);
                if (i == 0) shelfPos[i] = new Vector2(partition[partIndex].x, shelfPos[18].y + h);
                else shelfPos[i] = new Vector2(partition[partIndex].x, shelfPos[i - 1].y + h);
                shelf[i].transform.position = shelfPos[i];
                CreateCoin(i);
            }
            else if (partIndex == 1)
            {
                int testIndex = Random.Range(1, 3);
                if (testIndex == 1) partIndex = 0;
                else partIndex = 2;
                if (i == 0) shelfPos[i] = new Vector2(partition[partIndex].x, shelfPos[18].y + h);
                else shelfPos[i] = new Vector2(partition[partIndex].x, shelfPos[i - 1].y + h);
                shelf[i].transform.position = shelfPos[i];
                CreateCoin(i);
            }
            else if(partIndex == 2)
            {
                partIndex = Random.Range(0, 2);
                if (i == 0) shelfPos[i] = new Vector2(partition[partIndex].x, shelfPos[18].y + h);
                else shelfPos[i] = new Vector2(partition[partIndex].x, shelfPos[i - 1].y + h);
                shelf[i].transform.position = shelfPos[i];
                CreateCoin(i);
            }
        }
    }

    float coinY;
    void CreateCoin(int Index)
    {
        coinY = 0.55f;
        int coinRand = 0;
        coinRand = Random.Range(0, 101);

        if (coinRand > 0 && coinRand <= 20)
        {
            if (coinActive[5])
            {
                for (int i = 0; i < 6; i++)
                    coinActive[i] = false;
            }
            for (int i = 0; i < 6; i++)
            {
                if (!coinActive[i])
                {
                    coinActive[i] = true;
                    Coin[i].transform.position = new Vector2(partition[partIndex].x, shelfPos[Index].y + coinY);
                    StartCoroutine(gone(2f, i));
                    break;
                }
            }
        }
    }
    
    IEnumerator gone(float waitSeconds, int n)
    {
        yield return new WaitForSeconds(waitSeconds);
        Coin[n].transform.position = new Vector2(-4.62f, -2.15f);
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

    void Gameover()
    {
        //Cont.SetActive(true);
        goCoinText.text = getCoin.coin.ToString();
        PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") + getCoin.coin);
        PlayerPrefs.SetInt("EXP", PlayerPrefs.GetInt("EXP") + 20);
        isStart = false;
        GO.SetActive(true);
    }
}
