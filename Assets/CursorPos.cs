using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorPos : MonoBehaviour {

    public Transform location;
	// Use this for initialization
	void Start () {
        gameObject.transform.position = location.position;
	}
	
	// Update is called once per frame
	void Update () {
        gameObject.transform.position = location.position;
    }
}
