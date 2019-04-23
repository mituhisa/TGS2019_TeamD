using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2_umitora : MonoBehaviour {



    float speed = 100;




    // Use this for initialization

    void Start () {
	
        





	}
	
	// Update is called once per frame
	void Update () {

        Vector3 down = transform.TransformDirection(Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, down,out hit, 20))
        {
            if (hit.transform.tag == "Ground_umitora")
            {
                transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
            }




        }


        float zAxis = Input.GetAxis("Vertical");
        float xAxis = Input.GetAxis("Horizontal");


        Transform mainCamera = Camera.main.transform;
        Vector3 diff = (mainCamera.forward * zAxis + mainCamera.right * xAxis);
        diff.Normalize();
        diff = transform.InverseTransformDirection(diff);
        diff.y = 0;
        diff = transform.TransformDirection(diff);
        transform.LookAt(transform.position + diff, transform.up);
        
        transform.Translate(new Vector3(0, 0, speed* (Mathf.Abs(xAxis) + Mathf.Abs(zAxis))) * Time.deltaTime, Space.Self);





	}








}
