using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoketMove : MonoBehaviour {
    public Transform target;
    public int Checker;     //Checkerはロケットの行動パターン　０・・・通常移動      １・・・墜落      ２・・・停止
    public GameObject Smoke;
    public GameObject Fire_Red;
    public GameObject Fire_Orange;

 public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Terra")
        {
            Debug.Log("不時着");
            Checker = 2;    //墜落停止
           // GameObject.Find("Rocket_Fire_Smoke").SetActive(false);
            Smoke.SetActive(false);
        }
        if(collision.gameObject.tag == "Meteorite")
        {
            Debug.Log("墜落");
            Checker = 1;    //墜落開始
            Fire_Red.SetActive(false);
            Fire_Orange.SetActive(false);
            Smoke.SetActive(true);
            //GameObject.Find("Rocket_Fire_Orange").SetActive(false);
            //GameObject.Find("Rocket_Fire_Red").SetActive(false);
            //GameObject.Find("Rocket_Fire_Smoke").SetActive(true);
        }
    }
    // Use this for initialization
    void Start () {
        Checker = 0;

	}

    // Update is called once per frame
    void Update()
    {
        if (Checker == 0)
        {
            this.transform.Translate(0, 0, 0.1f);
        }
        if (Checker == 1)
        {
            Vector3 EnemyTarget = target.position - this.transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(EnemyTarget.x, EnemyTarget.y, EnemyTarget.z)), 0.3f);
            transform.Translate(0, 0, 0.05f);
        }
    }
}
