using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMove : MonoBehaviour {
    public bool ClearFlg;
	// Use this for initialization
	void Start () {
        ClearFlg = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (ClearFlg == true)
        {
            this.transform.Translate(0, 0, 0.1f);
        }
	}
}
