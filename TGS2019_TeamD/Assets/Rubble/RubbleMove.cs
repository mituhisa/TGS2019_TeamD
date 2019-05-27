using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubbleMove : MonoBehaviour {
    public GameObject Planet;
    [SerializeField] private float Distance;
    [SerializeField] private bool SinkFlg;
    [SerializeField] int TimeCount;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Planet")
        {
            SinkFlg = true;
        }
    }

    // Use this for initialization
    void Start () {
        SinkFlg = false;
        TimeCount = 0;
      //  Distance = (transform.position - Planet.transform.position).magnitude;

	}
	
	// Update is called once per frame
	void Update () {
		//if(Distance > 500)
  //      {
  //          Destroy(this.gameObject);
  //      }

        if(SinkFlg == true)
        {
            /// this.GetComponent<Rigidbody>().isKinematic = true;
            this.GetComponent<BoxCollider>().enabled = false;
            TimeCount++;
            if(TimeCount >= 40)
            {

                this.GetComponent<RubbleGravity>().RubbleGravityFLg = false;
                this.GetComponent<Rigidbody>().isKinematic = true;
                this.GetComponent<BoxCollider>().enabled = true;
                SinkFlg = false;
            }

            

        }
	}
}
