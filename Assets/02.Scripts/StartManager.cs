using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class StartManager : MonoBehaviour
{

    public int expValue;
    public int coinValue;
    public int bellValue;

    BoxCollider2D boxCollider;
    AudioSource audioCat;

    void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        audioCat = GetComponent<AudioSource>();
    }

    void Start()
    {
        boxCollider.enabled = false;
        Invoke("ColliderOn", 1f);

    }

    public void ColliderOn()
    {
        boxCollider.enabled = true;
    }

    public void OnMouseUp()
    {
        audioCat.Play();
        MakePrefs();
        SceneChange();
    }

    void SceneChange()
    {
        SceneManager.LoadScene("Main");
    }

    void MakePrefs()
    {
        PlayerPrefs.DeleteAll();
        if (!PlayerPrefs.HasKey("Start"))
        {
            PlayerPrefs.SetInt("Start", 1); //

            PlayerPrefs.SetInt("Coin", coinValue); //
            PlayerPrefs.SetInt("Bell", bellValue); //

            PlayerPrefs.SetInt("FiveScore", 0); //
            PlayerPrefs.SetInt("TowerScore", 0); //
            PlayerPrefs.SetInt("PikaScore", 0); //

            PlayerPrefs.SetInt("EXP", expValue); //

            PlayerPrefs.SetFloat("BGM", 0); //
            PlayerPrefs.SetFloat("SFX", 0); //

            PlayerPrefs.SetInt("Affine", 50);
            PlayerPrefs.SetInt("Hunger", 50);
            PlayerPrefs.SetInt("Health", 50);

            PlayerPrefs.SetInt("Item0", 0);//
            PlayerPrefs.SetInt("Item1", 0);//
            PlayerPrefs.SetInt("Item2", 0);//
            PlayerPrefs.SetInt("Item3", 0);//
            PlayerPrefs.SetInt("Item4", 0);//
            PlayerPrefs.SetInt("Item5", 0);//
            PlayerPrefs.SetInt("Item6", 0);//
            PlayerPrefs.SetInt("Item7", 0);//
            PlayerPrefs.SetInt("Item8", 0);//

            PlayerPrefs.Save();
        }
    }

}
