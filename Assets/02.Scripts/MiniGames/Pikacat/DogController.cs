using UnityEngine;
using System.Collections;

public class DogController : MonoBehaviour
{
    public PikaManager gm;
    public ButtonManager bm;

    private float forwardSpeed = 3.0f;
    float spawnTime = 10f;     //강아지 상태 변화 초기값. 10초 후 처음으로 변함

    public bool dogState = false;

    int count = 0;  //강아지 몇 번 돌았는지

    void Start()
    {
        StartCoroutine("StateChange");
    }

    void Update()
    {
        if (!gm.isStart) return;
           
        if (bm.pauseState) 
            this.GetComponent<Rigidbody2D>().velocity = new Vector3(0.0f, 0.0f);

        this.GetComponent<Rigidbody2D>().velocity = new Vector3(0.0f, forwardSpeed);

        if( count<15 )
            spawnTime = Random.Range(6f,10f);
        else
            spawnTime = Random.Range(4f, 7f);
    }

    IEnumerator StateChange()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTime);
            
            GetComponent<Animator>().SetTrigger("Warning");

            yield return new WaitForSeconds(1.2f);

            
            GetComponent<Animator>().SetTrigger("Danger");
            dogState = true;

            yield return new WaitForSeconds(2f);
            dogState = false;

            count++;
        }
    }
}
