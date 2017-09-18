using UnityEngine;
using System.Collections;

public class BG_Move : MonoBehaviour {

    public GameObject Cat;
    //public GameObject bgTop;
    //public GameObject spareBG;
    //float bgPos;
    public GameObject[] BG = new GameObject[4];

    Vector2[] bgTr = new Vector2[4];
    int catbg;
    float bgH;

    void Start()
    {
        catbg = -1;
        bgH = 31.9f;
        for(int i =0; i < 4; i++)
        {
            bgTr[i] = BG[i].transform.position;
        }
    }

    void Update()
    {
        if (Cat.transform.position.y >= BG[0].transform.position.y)
        {
            if (catbg == 0) BgLoop();
        }
        if (Cat.transform.position.y >= BG[1].transform.position.y) catbg++;
        if (Cat.transform.position.y >= BG[2].transform.position.y)
        {
            catbg++;
            BgLoop();
        }
        if (Cat.transform.position.y >= BG[3].transform.position.y) catbg++;

        if (catbg >= 2) catbg = 0;
    }

    void BgLoop()
    {

        if (catbg == 2)
        {

            for (int j = 0; j < 2; j++)
            {
                if (j == 0)
                    bgTr[j] = new Vector2(0, bgTr[3].y + bgH);
                else
                {
                    bgTr[j] = new Vector2(0, bgTr[j - 1].y + bgH);
                }
                BG[j].transform.position = bgTr[j];
            }
        }
        else if (catbg == 0)
        {
            for (int j = 2; j < 4; j++)
            {
                bgTr[j] = new Vector2(0, bgTr[j - 1].y + bgH);
                BG[j].transform.position = bgTr[j];
            }
        }
    }
}
