using UnityEngine;
using System.Collections;

public class RightArrow : MonoBehaviour {

    public GameObject cat;
    float speed = 2.0f;
    float maxPos = 2.0f;

    void Update()
    {
        if (cat.transform.localPosition.x >= maxPos)
            cat.transform.position = new Vector3(maxPos, cat.transform.position.y, 0.0f);
    }

    public void OnMouseOver()
    {
        cat.transform.Translate(speed * Time.deltaTime, 0f, 0f);
        
    }
}
