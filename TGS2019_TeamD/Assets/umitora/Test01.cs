using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test01 : MonoBehaviour {

    Vector3 vector;


	// Use this for initialization
	void Start () {


        vector = new Vector3(10, 10, 0);


	}
	
	// Update is called once per frame
	void Update () {


        if (Input.GetKeyDown(KeyCode.B))
        {
            vector = new Vector3(vector.x * Mathf.Cos(90 * Mathf.Deg2Rad) - vector.y * Mathf.Sin(90 * Mathf.Deg2Rad), 
                                        vector.y * Mathf.Cos(Mathf.Deg2Rad * 90) + vector.x * Mathf.Sin(90 * Mathf.Deg2Rad), 0);
        transform.forward = new Vector3(1, 1, 1);
            transform.up= new Vector3(1, 1, 1);

        }


        Debug.DrawRay(transform.position, vector, Color.blue, 3);



	}





}
