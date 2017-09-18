using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GetCoin : MonoBehaviour {

    public Text CTxt;
    public int coinValue;

    public int coin;

    // Use this for initialization
    void Start ()
    {
        coin = 0;
	}
	
	// Update is called once per frame
	void Update () {
        CTxt.text = coin.ToString();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Coin")
        {
            col.gameObject.transform.position = new Vector2(-4.62f, -2.15f);
            coin += coinValue;
        }

    }
}
