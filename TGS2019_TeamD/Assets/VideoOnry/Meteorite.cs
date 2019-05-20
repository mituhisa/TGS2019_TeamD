using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteorite : MonoBehaviour {
    public Transform target;
    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Rocket")
        {
            Debug.Log("隕石退出");
            Destroy(this.gameObject);
        }
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 EnemyTarget = target.position - this.transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(EnemyTarget.x, EnemyTarget.y, EnemyTarget.z)), 0.3f);
        transform.Translate(0,0,0.1f);
    }
}
