using UnityEngine;
using System.Collections;

public class LeftArrow : MonoBehaviour {

    public HideController hc;

    public GameObject cat;
    float speed = 2.0f;
    float minPos = -2.0f;

    void Update()
    {
        if (hc.hideState)
        {
        }

        if (cat.transform.localPosition.x <= minPos)
            cat.transform.position = new Vector3(minPos, cat.transform.position.y, 0.0f);
    }

    public void OnMouseOver()
    {
        cat.transform.Translate(-speed * Time.deltaTime, 0f, 0f);
        
    }
}
