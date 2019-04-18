using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityController : MonoBehaviour
{

    Rigidbody myRigidbody;
    Transform Planet;               //惑星
    float accelerationScale = 100;      // 加速度の大きさ

    Vector3 direction;  //惑星へのベクトル


    // Use this for initialization
    void Start()
    {


        myRigidbody = GetComponent<Rigidbody>();

        Planet = GameObject.Find("Ground2_umitora").GetComponent<Transform>();  //惑星取得


    }

    // Update is called once per frame
    //void Update () {


    //   }

    private void FixedUpdate()
    {
        direction = Planet.position - transform.position;       //惑星へのベクトル取得
        myRigidbody.AddForce(direction.normalized * accelerationScale, ForceMode.Acceleration);     //AddForceで惑星に向かって力を加える
    }


    public Vector3 GetDirection()
    {
        return direction.normalized;
    }


}