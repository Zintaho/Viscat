using UnityEngine;
using System.Collections;

public class CatFollower : MonoBehaviour {

    public Transform cat;

	void Update () {
        this.transform.position = new Vector2(0.0f, cat.position.y + 2f);
	}
}
