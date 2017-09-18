using UnityEngine;
using System.Collections;

public class layer : MonoBehaviour {

    public MeshRenderer score = null;

	// Use this for initialization
	void Start () {
        score.sortingOrder = 4;
        //renderer.sortingOrder 

       // particleSystem.renderer. = 2;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
