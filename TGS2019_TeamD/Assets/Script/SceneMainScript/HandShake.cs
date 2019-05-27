using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandShake : MonoBehaviour
{

    private Vector3 initPos;

    // Use this for initialization
    void Start()
    {
        initPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = new Vector3(Mathf.Sin(Time.time), 0, 0);
    }
}
