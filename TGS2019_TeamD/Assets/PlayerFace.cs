using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFace : MonoBehaviour {

    [SerializeField] public GameObject Center;
    [SerializeField] public GameObject playerBody;

    float yaw, pitch, RotateSpeed;

    Vector3 roteuler;

    // Use this for initialization
    void Start () {
        RotateSpeed = 1;
        roteuler = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 0f);
    }

    // Update is called once per frame
    void Update()
    { 
        yaw = Input.GetAxis("Mouse X");
        pitch = Input.GetAxis("Mouse Y");

        roteuler = new Vector3(Mathf.Clamp(roteuler.x - pitch * Time.deltaTime * 200f, -80, 60), /*roteuler.y + yaw*/0f, 0f);
        transform.localEulerAngles = roteuler;

        playerBody.transform.Rotate(0, roteuler.y + yaw, 0);
    }
}
