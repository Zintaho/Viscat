using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SelectGame : MonoBehaviour {

    //public CoinValue CV;
    //public Text coinText;

    /*
    void Update()
    {
        coinText.text = CV.value.ToString();
    }
    */

	public void clickTower()
    {
        Application.LoadLevel("CatTower");
    }

    public void clickBack()
    {
        Application.LoadLevel("Main");
    }

    public void clickPika()
    {
        Application.LoadLevel("PikaCatScene");
    }

    public void clickFive()
    {
        Application.LoadLevel("Five");
    }
}
