using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController_umitora : MonoBehaviour {

    Transform player;

    float rotationSpeed = 10;

	// Use this for initialization
	void Start () {


        player = GameObject.Find("Player_umitora2").GetComponent<Transform>();



	}
	
	// Update is called once per frame
	void Update () {

        Vector3 down = transform.TransformDirection(Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, down, out hit, 20))
        {
            if (hit.transform.tag == "Ground_umitora")
            {
                transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
            }




        }






    }

    void LateUpdate()
    {
        transform.position = player.position;
        transform.Rotate(0f, Input.GetAxis("RotateY") * Time.deltaTime * rotationSpeed, 0f);
        if (Input.GetButtonDown("RotateY"))
        {
            Debug.Log("rotate");
        }
    }






}
