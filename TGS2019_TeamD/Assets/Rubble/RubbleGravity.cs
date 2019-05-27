using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubbleGravity : MonoBehaviour {

    Rigidbody myRigidbody;
    public Transform Planet;               //惑星
    float accelerationScale = 5;      // 加速度の大きさ
    public bool RubbleGravityFLg;

    Vector3 direction;  //惑星へのベクトル


    // Use this for initialization
    void Start()
    {

        RubbleGravityFLg = true;
        myRigidbody = GetComponent<Rigidbody>();

       // Planet = GameObject.Find.gameObject.tag("Ground2_umitora").GetComponent<Transform>();  //惑星取得


    }

    // Update is called once per frame
    //void Update () {


    //   }

    private void FixedUpdate()
    {
        if (RubbleGravityFLg == true)
        {
            direction = Planet.position - transform.position;       //惑星へのベクトル取得
            myRigidbody.AddForce(direction.normalized * accelerationScale /4, ForceMode.Acceleration);     //AddForceで惑星に向かって力を加える
        }
    }


    public Vector3 GetDirection()
    {
        return direction.normalized;
    }

}
