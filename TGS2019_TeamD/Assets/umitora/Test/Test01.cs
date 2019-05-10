using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test01 : MonoBehaviour
{

    Vector3 vector;

    float angle = 0;
    // Use this for initialization
    void Start()
    {


        //vector = new Vector3(10, 10, 0);


    }

    // Update is called once per frame
    void Update()
    {


        //if (Input.GetKeyDown(KeyCode.B))
        //{
        //    vector = new Vector3(vector.x * Mathf.Cos(90 * Mathf.Deg2Rad) - vector.y * Mathf.Sin(90 * Mathf.Deg2Rad), 
        //                                vector.y * Mathf.Cos(Mathf.Deg2Rad * 90) + vector.x * Mathf.Sin(90 * Mathf.Deg2Rad), 0);
        //transform.forward = new Vector3(1, 1, 1);
        //    transform.up= new Vector3(1, 1, 1);

        //}


        //Debug.DrawRay(transform.position, transform.up*5, Color.blue, 3);

        //Debug.DrawRay(transform.position, new Vector3(1, 1, 1) * 5, Color.green, 3);



        if (Input.GetKey(KeyCode.B))
        {
            angle += 1;

            //transform.rotation = Quaternion.AngleAxis(angle, transform.up);


            //transform.rotation = Quaternion.AngleAxis(angle, new Vector3(0, 1, 0));
            transform.rotation = Quaternion.AngleAxis(angle, new Vector3(1, 1, 1));

            //transform.Rotate(0, 1, 0);

        }

        //Vector3 diff = transform.up;
        //Debug.DrawRay(transform.position, diff*5, Color.red, 1);
        //transform.InverseTransformDirection(diff);
        //diff.y = 0;
        //Debug.DrawRay(transform.position,diff * 5, Color.blue, 1);

        //transform.TransformDirection(diff);
        //Debug.DrawRay(transform.position, diff* 5, Color.green, 1);



        //Debug.DrawRay(transform.position, transform.forward*100, Color.red);
        //Debug.DrawRay(transform.position,transform.TransformDirection( transform.forward)*5, Color.blue);
        //Debug.DrawRay(transform.position,transform.InverseTransformDirection( transform.forward) *50 , Color.green);



        //Debug.DrawRay(transform.position, transform.up * 5, Color.red, 1);
        //Debug.DrawRay(transform.position, Vector3.up * 5, Color.blue, 1);




        Transform test2 = GameObject.Find("Cube_test2").GetComponent<Transform>();


        Debug.DrawRay(transform.position, test2.up * 10, Color.green);
        Debug.DrawRay(transform.position, transform.InverseTransformDirection(test2.up) * 10, Color.blue);
        Debug.DrawRay(transform.position, transform.TransformDirection(test2.up) * 10, Color.black);






        //Debug.DrawRay(transform.position, transform.TransformDirection(transform.TransformDirection(test2.up)) * 10, Color.gray, 1);
        //Debug.Log("transform" + transform.up);
        //Debug.Log("vector3" + Vector3.up);

    }





}
