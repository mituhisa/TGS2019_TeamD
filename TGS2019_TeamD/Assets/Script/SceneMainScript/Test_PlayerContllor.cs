﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_PlayerContllor : MonoBehaviour {

    public float moveSpeed = 5f;
    private Vector3 moveDir;
    private Animator anim;

    float inputHorizontal;
    float inputVertical;

    [HideInInspector] public bool CheckFlg = false;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (!CheckFlg)
        {
            moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;

            inputHorizontal = Input.GetAxisRaw("Horizontal");
            inputVertical = Input.GetAxisRaw("Vertical");
        }

        if(inputHorizontal != 0 || inputVertical != 0)
        {
            anim.SetFloat("Speed", 1);
        }
        else
        {
            anim.SetFloat("Speed", 0);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = 15;
            anim.speed = 2;
        }else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            moveSpeed = 5;
            anim.speed = 1;
        }

        RaycastHit hit;

        // Transformの真下の地形の法線を調べる
        if (Physics.Raycast(
                    transform.position,
                    -transform.up,
                    out hit,
                    float.PositiveInfinity))
        {
            // 傾きの差を求める
            Quaternion q = Quaternion.FromToRotation(
                        transform.up,
                        hit.normal);

            // 自分を回転させる
            transform.rotation *= q;
        }
    }

    private void FixedUpdate()
    {
        if (!CheckFlg)
        {
            GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + transform.TransformDirection(moveDir) * moveSpeed * Time.deltaTime);
        }
    }
}