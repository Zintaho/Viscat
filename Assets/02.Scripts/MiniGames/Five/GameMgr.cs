using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public enum GameState { frontSole, backSole }
public enum GameTime { wait, go }

public enum GameBlock { one, two, three, fourth }
public class GameMgr : MonoBehaviour
{

    [Header("Unit Creation Values")] //인스펙터 노출을 제어하는 어트리뷰트, 유닛이 생성되는 수치 표시용
    public bool isStart = false;

    public int stateJudge;
    public int timeJudge;
    public int blockJudge;
    public double delay; // judge값을 산출하는 딜레이
    public double delay_plus;

    //    public int heartlife;

    private double timeSaved;

    [Header("Control Values")] //인게임 조작에 관련한 변수 노출
    public bool isControllable; //이 변수가true일 때만 터치 조작 실행 가능

    [Header("heart")]
    public GameObject[] hearts = new GameObject[3]; //heart 오브젝트 세 개 

    [Header("Units")]
    public GameObject[] units = new GameObject[8];

    [Header("Touch Detection")] //터치 감지를 위한 변수
    public GameObject[] touches = new GameObject[2];

    [Header("Spark Error")]
    public GameObject[] sparkerrors = new GameObject[4];

    [Header("Buttons")]
    public GameObject oneButton;
    public GameObject twoButton;
    public GameObject threeButton;
    public GameObject fourButton;
    public GameObject frontButton;
    public GameObject backButton;

    [Header("UI")]
    private int score;
    private int heartlife;
    public Text scoreText;
    //public TextMesh scoreText;
    public TextMesh heartText;

    public GameObject ready;
    public GameObject go;

    bool gameOver = false;
    public GameObject GOPopup;

    void Start()
    { // 시작시 모든 unit오브젝트 비활성화
        Invoke("Ready", 1.5f);
        foreach (GameObject obj in units)
        {
            obj.SetActive(false);
        }

        foreach (GameObject obj in sparkerrors)
        {
            obj.SetActive(false);
        }

        heartlife = 3; //하트 개수 초기화 
        score = 0; //점수 초기화
        timeSaved = 0; //시간값 초기화
        isControllable = false; //bool값 초기화
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver) isStart = false;
        if (!isStart) return;
        delay -= delay_plus;
        if (!isControllable)
        { //isControllable이 false일 때 터치 감지 오브젝트의 collider를 비활성화
        }

        if (Time.time >= timeSaved)
        { //처음에만 1회 실행 후 delay만큼 의시간이 지났을 때 1회씩 실행
            isControllable = true; //터치 가능 상태로 변경

            timeSaved = Time.time + delay;
            stateJudge = Random.Range(0, 2);
            timeJudge = Random.Range(0, 2);
            blockJudge = Random.Range(0, 4); //1,2,3,4 판단하는 카운트입니다.

            UnitChange();
        }
        if (Input.touchCount >= 2)
        { //터치카운트가 2개 이상일 때 터치이벤트 발생
            Vector3 firstPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 10f));
            Vector3 secondPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.GetTouch(1).position.x, Input.GetTouch(1).position.y, 10f));

            //버그인지 의심되는 부분, y좌표가 스크린 높이만큼 위에서부터 시작되어 해당 수치만큼 빼줌
            //ScreenToWorldPoint는 스크린 좌표를 월드 좌표계로 변환시켜주는 메소드

            touches[0].transform.position = firstPos;//
            touches[1].transform.position = secondPos;

            if (isControllable)
            { //터치카운트가 2개 이상이고 isControllable이 true일 때
                RaycastHit2D firstTouch = Physics2D.CircleCast(touches[0].transform.position, 0.1f, Vector2.zero, 1f, 1 << 8);
                RaycastHit2D secondTouch = Physics2D.CircleCast(touches[1].transform.position, 0.1f, Vector2.zero, 1f, 1 << 8);
                //touches 오브젝트들의 위치에 0.2반지름에 8번 레이어(BUTTON)만을 감지하는 컬라이더 생성

                //		if (firstTouch && secondTouch)
                if (TouchCheck(firstTouch, secondTouch) == 10)
                { //둘 다 터치 상태이라면? 
                    score += TouchCheck(firstTouch, secondTouch); //현재 터치중인 버튼을 체크, 점수 증가
                    isControllable = false; //한번 터치 두번을 한 후에는 다음 유닛 변경까지 터치 불가


                }
                if (TouchCheck(firstTouch, secondTouch) == 2)
                {
                    heartlife -= 1;
                    Debug.Log(heartlife);

                    isControllable = false;
                    hearts[heartlife].SetActive(false);

                    if ( heartlife <= 0)
                    {
                        PlayerPrefs.SetInt("EXP", PlayerPrefs.GetInt("EXP") + 20);
                        gameOver = true;
                        GOPopup.SetActive(true);
                    }
                }

            }
        }

        scoreText.text = score.ToString(); //score를 scoreText의 텍스트로 변환
        heartText.text = heartlife.ToString();

    }


    void UnitChange()
    { //유닛 전환 메소드
        foreach (GameObject obj in units)
        {
            obj.SetActive(false);
        }

        switch (stateJudge)
        { //stateJudge 마저 랜덤으로 뿌려줍니다. 
            case 0: //앞
                switch (blockJudge)
                { //blockJudge는 랜덤으로 뿌려줍니다. 랜덤으로 뿌려준 값에 맞게 SetActive를 발동합니다.
                    case 0:
                        units[0].SetActive(true); //앞일때 방향입니다. units[0],1,2,3은 앞일 때의 위치입니다. 
                        break;
                    case 1:
                        units[1].SetActive(true);
                        break;
                    case 2:
                        units[2].SetActive(true);
                        break;
                    case 3:
                        units[3].SetActive(true);
                        break;
                }
                break;
            case 1: //뒤
                switch (blockJudge)
                { //blockJudge는 이제 판단 저지인데.. 4,5,6,7는 뒤의 자리입니다.
                    case 0:
                        units[4].SetActive(true);
                        break;
                    case 1:
                        units[5].SetActive(true);
                        break;
                    case 2:
                        units[6].SetActive(true);
                        break;
                    case 3:
                        units[7].SetActive(true);
                        break;
                }
                break;
        }
    }

    int TouchCheck(RaycastHit2D first, RaycastHit2D second)
    {

        int value = 0;
        switch (first.collider.tag)
        { //첫번째 컬라이더 확인
            case "1_button":
                value += 1;
                break;
            case "2_button":
                value += 2;
                break;
            case "3_button":
                value += 4;
                break;
            case "4_button":
                value += 8;
                break;
            case "front_button":
                value += 16;
                break;
            case "back_button":
                value += 32;
                break;
            default:
                value += 0;
                break;
        }

        switch (second.collider.tag)
        { //두번째 컬라이더 확인 , 두번째 까지 하는 건 터치시 플레이어에게 순서에 구애받지 않도록 합니다.
            case "1_button":
                value += 1;
                break;
            case "2_button":
                value += 2;
                break;
            case "3_button":
                value += 4;
                break;
            case "4_button":
                value += 8;
                break;
            case "front_button":
                value += 16;
                break;
            case "back_button":
                value += 32;
                break;
            default:
                value += 0;
                break;
        }
        if (value == (Mathf.Pow(2, blockJudge) + Mathf.Pow(2, stateJudge + 4))) 
            return 10;
        else if (value != (Mathf.Pow(2, blockJudge) + Mathf.Pow(2, stateJudge + 4)))
        {

            Handheld.Vibrate();
            Debug.Log(value);
            Debug.Log("진입!!");
            return 2;
        }
        else
        {

            return 2;
        }
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

    IEnumerator SetAvtiveObjinSecond(GameObject obj, float second)
    {

        obj.SetActive(true);

        yield return new WaitForSeconds(second);
        obj.SetActive(false);


    }
}