using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {
    public Transform Rocket;
    Vector3 CameraPosition;
	// Use this for initialization
	void Start () {
        CameraPosition = new Vector3(-1, -0.5f, 1.5f);
        this.transform.rotation = Quaternion.Euler(217.761f, -50.76801f, 138.435f);
        //this.transform.rotation = Rocket.rotation;
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.position = Rocket.position + CameraPosition;
        transform.Rotate(0, 0.1f, 0.05f);
    }
}
