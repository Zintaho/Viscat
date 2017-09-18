using UnityEngine;
using System.Collections;

public class ObjectCreate : MonoBehaviour {

    public GameObject cat;
    public GameObject coin;
    public GameObject ob;
    float pos;
    float spawnTime_c, spawnTime_o;

    private float minPos = -2f;
    private float maxPos = 2f;

    int count = 0;  //장애물 몇개 지났는지

    void Start()
    {
        StartCoroutine("CreateOb");
        StartCoroutine("CreateCoin");
    }

    void Update()
    {
        spawnTime_c = Random.Range(6f, 8f);

        if (count < 8)
        {
            spawnTime_o = Random.Range(6f, 9f);
        }
        else if(count >=8 && count < 15)
            spawnTime_o = Random.Range(4f, 7f);
        else
            spawnTime_o = Random.Range(2f, 4f);
    }

    IEnumerator CreateCoin()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTime_c);

            pos = Random.Range(minPos, maxPos);

            Instantiate(coin, new Vector2(pos, cat.transform.position.y + 15f), Quaternion.identity);

        }
    }

    IEnumerator CreateOb()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTime_o);

            pos = Random.Range(minPos, maxPos);

            Instantiate(ob, new Vector2(pos, cat.transform.position.y + 10f), Quaternion.identity);

            count++;
        }
    }
}
