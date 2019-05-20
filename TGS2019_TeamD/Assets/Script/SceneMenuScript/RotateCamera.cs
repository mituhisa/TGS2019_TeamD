using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //transform.Rotate(0, 2*Time.deltaTime, 0);
        transform.Rotate(2 * Time.deltaTime, 2 * Time.deltaTime, 2 * Time.deltaTime);
	}
}
