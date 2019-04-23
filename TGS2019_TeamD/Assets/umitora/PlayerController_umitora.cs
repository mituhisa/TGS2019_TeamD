using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_umitora : MonoBehaviour
{

    //GravityController gravityController;

    Transform Planet;

    float speed = 100;
    float currentAngle = 0;


    // Use this for initialization
    void Start()
    {

        //gravityController = GetComponent<GravityController>();
        Planet = GameObject.Find("Ground2_umitora").GetComponent<Transform>();



    }



    // Update is called once per frame
    void Update()
    {

        //transform.Rotate(0, 10, 0);



    }

    private void FixedUpdate()
    {
        //移動
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        Vector3 forward = transform.forward * vertical * speed;
        Vector3 right = transform.right * horizontal * speed;
        transform.localPosition += (forward + right) * Time.deltaTime;


        //float mouseX = Input.GetAxis("Mouse X");
        //float mouseY = Input.GetAxis("Mouse Y");


        //currentAngle += mouseX;


        //ここから回転の文章********************************************

        if (Input.GetKey(KeyCode.N))
        {
            //transform.rotation = transform.rotation*  Quaternion.AngleAxis(10, -direction);
            currentAngle += -10 * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.M))
        {
            currentAngle += 10 * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.I))
        {
            transform.Rotate(0, -1f, 0);
        }
        if (Input.GetKey(KeyCode.O))
        {
            transform.Rotate(new Vector3(0, 1f, 0));
        }




        //Vector3 direction = gravityController.GetDirection();

        Vector3 direction = transform.position - Planet.position;
        //transform.up = direction;
        //transform.Rotate(0, 1, 0);
        //transform.rotation = Quaternion.Euler(new Vector3(0,0,0));
        //transform.eulerAngles = new Vector3(0, 0, 0);
        //transform.rotation = Quaternion.AngleAxis(currentAngle, new Vector3(0, 1, 0));
        //transform.localRotation = Quaternion.AngleAxis(currentAngle, transform.up);
        //transform.rotation = new Quaternion(0, 0, 0, 1);
        //Debug.Log("rotation " + transform.rotation);
        //Debug.Log("quaternion " + Quaternion.AngleAxis(currentAngle, -direction));





        //transform.localRotation = new Quaternion(direction.x, direction.y, direction.z, currentAngle);
        //transform.localRotation = new Quaternion(direction.x, direction.y, direction.z, 0);
        //direction = transform.TransformDirection(direction);


        //Quaternion q = Quaternion.AngleAxis(1, direction);
        //transform.rotation = transform.rotation * q;

        //transform.rotation = Quaternion.Euler(direction);

        //transform.rotation = Quaternion.LookRotation(direction,);

        Debug.DrawRay(transform.position, direction.normalized*3, Color.red, 5);

        //transform.LookAt(GameObject.Find("Ground2_umitora").GetComponent<Transform>().position);
        //transform.LookAt(GameObject.Find("Ground2_umitora").GetComponent<Transform>().position,direction*-1);

        //transform.rotation = Quaternion.Euler(0, currentAngle, 0);
        Debug.Log("currentangle " + currentAngle);


        //transform.Rotate(0, 10, 0);
    }





}