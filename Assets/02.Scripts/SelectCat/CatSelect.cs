using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;

public class CatSelect : MonoBehaviour
{

    //int phase = 0; // 0 : 통상. 1: 고양이 선택됨

    public GameObject textBalloon;
    GameObject balloonPrefab;

    public GameObject BackGround;
    public Sprite[] BackGrounds; // 배경이미지 전환용

    public GameObject thisCat;

    public GameObject FirstCat;
    public GameObject[] Icons;

    int curIndex = 0;
    public Sprite[] CatSprites;

    //Vector2 curScale;
    //Collider2D formerObject;

    /*말풍선 관련된 변수들. 클래스나 스트럭트로 만드는게 좋을듯!*/
    const int MAX_TEXT = 3;

    string[] Texts; //말풍선 대사집
    int offset = 0;
    bool isTextEnd = false;
    /*************************************************/

    enum Cats { White, Yellow, Black };

    /****SFX***/
    public AudioClip click;
    public AudioClip ok;

    // Use this for initialization
    void Start()
    {
        //phase = 0;
        setBackGround(0);
        setVisible(FirstCat, false);
        setVisible(Icons, false);
        setVisible(thisCat, false);
        setTexts();
        makeBalloon();
    }

    // Update is called once per frame
    void Update()
    {
        TouchEvent();
    }

    void setBackGround(int k)
    {
        BackGround.GetComponent<SpriteRenderer>().sprite = BackGrounds[k];
    }

    /*************Overloaded*************/
    void setVisible(GameObject obj, bool value)
    {
        obj.SetActive(value);
    }
    void setVisible(GameObject[] Objs, bool value)
    {
        foreach (GameObject obj in Objs)
        {
            obj.SetActive(value);
        }
    }
    /************************************************/

    void setTexts()
    {
        Texts = new string[MAX_TEXT];
        offset = 0;

        /*원래는 DB에서 불러와야함!*/
        Texts[0] = "안녕하세요!";
        Texts[1] = "날 키울거냥에 잘 오셨어요!";
        Texts[2] = "고양이를 골라주세요!";
    }

    void test(string s)
    {
        PlayerPrefs.SetString("Name", s);
    }

    void makeBalloon()
    {
        balloonPrefab = Instantiate(textBalloon) as GameObject;
        balloonPrefab.transform.SetParent(GameObject.Find("Canvas").transform);
        balloonPrefab.transform.localScale = new Vector3(1, 1, 1);
        var tempPosition = balloonPrefab.transform.localPosition;
        balloonPrefab.transform.localPosition = new Vector3(tempPosition.x, tempPosition.y, 0);
        balloonPrefab.transform.FindChild("Text").GetComponent<Text>().text = Texts[offset++];
    }

    void SpriteChange(int k)
    {
        FirstCat.GetComponent<SpriteRenderer>().sprite = CatSprites[k];
    }


    public void LeftClick()
    {
        int len = CatSprites.Length;
        if (curIndex > 0 && curIndex <= len - 1)
        {
            SpriteChange(--curIndex);
        }
        else if (curIndex == 0)
        {
            curIndex = len - 1;
            SpriteChange(curIndex);
        }
        AudioSource.PlayClipAtPoint(click, transform.position);
    }
    public void RightClick()
    {
        int len = CatSprites.Length;
        if (curIndex >= 0 && curIndex < len - 1)
        {
            SpriteChange(++curIndex);
        }
        else if (curIndex == len - 1)
        {
            curIndex = 0;
            SpriteChange(curIndex);
        }
        AudioSource.PlayClipAtPoint(click, transform.position);
    }
    public void confirmClick()
    {
        AudioSource.PlayClipAtPoint(ok, transform.position);
        foreach (Cats kitty in Enum.GetValues(typeof(Cats)))
        {
            if(Convert.ToInt32(kitty) == curIndex)
            {
                PlayerPrefs.SetString("Cat", kitty.ToString());
                Debug.Log(kitty.ToString());
                //phase = 0;
                break;
            }
        }
        SceneManager.LoadScene("Main");
    }

    void TouchEvent()
    {
        /*Debug*/
        if (Input.GetMouseButtonUp(0))
        {
            if (offset < Texts.Length)
            {
                AudioSource.PlayClipAtPoint(click, transform.position);
                balloonPrefab.transform.FindChild("Text").GetComponent<Text>().text = Texts[offset++];
            }
            else
            {
                if (!isTextEnd)
                {

                    AudioSource.PlayClipAtPoint(ok, transform.position);
                    Destroy(balloonPrefab);

                    setBackGround(1);

                    setVisible(FirstCat, true);
                    setVisible(Icons, true);
                    setVisible(thisCat, true);

                    SpriteChange(curIndex);

                    isTextEnd = true;
                }

                /*
                else
                {
                    switch (phase)
                    {
                        case 0:
                            {
                                PhaseZero();
                                break;
                            }
                        case 1:
                            {
                                PhaseOne();
                                break;
                            }
                    }
                }
                 */
            }

            /*
            if (Input.touchCount == 1) //손가락 1개 터치
            {

                foreach (Touch touch in Input.touches)
                {

                    switch (touch.phase)
                    {
                        case TouchPhase.Began: //터치 시작시
                            break;
                        case TouchPhase.Moved: //드래그 시
                            break;
                        case TouchPhase.Canceled: //터치 취소시
                            break;
                        case TouchPhase.Ended: //터치 종료시 (이때 이벤트 발생시킬것)
                            break;
                    }
                }
            }
            */
        }


    }

    /*
    bool PhaseZero()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit.collider != null)
        {
            curScale = hit.collider.transform.localScale;
            hit.collider.transform.localScale = new Vector2(curScale.x + curScale.x / 4, curScale.y + curScale.y / 4);

            formerObject = hit.collider;
            phase = 1;
            setVisible(Icons, true);
            return true;
        }

        return false;
    }

    void PhaseOne()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit.collider != null && hit.collider.name == "O")
        {
            PlayerPrefs.SetString("Cat", formerObject.name);
            phase = 0;
            SceneManager.LoadScene("Main");
        }
        else
        {
            formerObject.transform.localScale = new Vector2(curScale.x, curScale.y);
            phase = 0;
            setVisible(Icons, false);
        }
    }
*/
}
