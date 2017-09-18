using UnityEngine;
using System.Collections;

public class BackgroundLoop : MonoBehaviour {

    public float offset;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.name == "BG")
        {
            Vector3 pos;

            offset = col.GetComponent<SpriteRenderer>().bounds.size.y;

            pos = col.transform.position;

            pos.y += offset * 3;

            col.transform.position = pos;
        }
    }
}
