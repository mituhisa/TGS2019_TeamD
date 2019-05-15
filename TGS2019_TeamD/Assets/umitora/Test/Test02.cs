using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test02 : MonoBehaviour {

    float angle = 0;



	// Use this for initialization
	void Start () {


        Debug.Log("up " + Vector3.up);


	}
	
	// Update is called once per frame
	void Update () {


        if (Input.GetKey(KeyCode.C))
        {
            angle += 1;

            //transform.rotation = Quaternion.AngleAxis(angle,transform.up);
            //transform.Rotate(transform.up, 1);
            //transform.Rotate(new Vector3(0,1,0), 1);
        }


        Debug.DrawRay(transform.position, transform.up * 5, Color.yellow, 1);
        //Debug.DrawRay(transform.position, transform.up * 5, Color.yellow, 3);


        //Debug.Log("local up " + transform.up);
        //Debug.Log("world up " +transform.TransformDirection( transform.up));

    }






}
