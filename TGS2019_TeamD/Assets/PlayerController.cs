using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    float inputHorizontal;
    float inputVertical;
    Rigidbody rb;

    float moveSpeed = 3f;

    [SerializeField] private float offset;
    [HideInInspector] public bool CheckFlg = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!CheckFlg)
        {
            inputHorizontal = Input.GetAxisRaw("Horizontal");
            inputVertical = Input.GetAxisRaw("Vertical");

            transform.rotation = Quaternion.LookRotation(Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, Camera.main.nearClipPlane + offset)));
        }
    }

    void FixedUpdate()
    {
        // カメラの方向から、X-Z平面の単位ベクトルを取得
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

        // 方向キーの入力値とカメラの向きから、移動方向を決定
        Vector3 moveForward = cameraForward * inputVertical + Camera.main.transform.right * inputHorizontal;

        // 移動方向にスピードを掛ける。ジャンプや落下がある場合は、別途Y軸方向の速度ベクトルを足す。
        rb.velocity = moveForward * moveSpeed + new Vector3(0, rb.velocity.y, 0);


        // キャラクターの向きを進行方向に
        //if (moveForward != Vector3.zero)
        //{
        //    //transform.rotation = Quaternion.LookRotation(moveForward);
        //    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveForward), Time.deltaTime*3.0f);
        //}
    }
}
