using UnityEngine;
using System.Collections;

public class HideController : MonoBehaviour {

    public GameObject cat;
    public bool hideState = false;

    Animator anim;
    AnimatorStateInfo aInfo;

    void Start()
    {
        anim = cat.GetComponent<Animator>();
    }

    void Update()
    {
        aInfo = anim.GetCurrentAnimatorStateInfo(0);

        if (aInfo.IsTag("c_hide"))
        {
            hideState = true;
        }
        else if (aInfo.IsTag("c_walk"))
        {
            hideState = false;
        }
    }

    public void OnMouseDown()
    {
        anim.SetTrigger("Hide");
    }
}
