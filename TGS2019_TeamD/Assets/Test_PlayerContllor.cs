using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_PlayerContllor : MonoBehaviour {

    public float moveSpeed = 15f;
    private Vector3 moveDir;

    float inputHorizontal;
    float inputVertical;

    [SerializeField] public GameObject Center;
    [HideInInspector] public bool CheckFlg = false;

    void Update()
    {
        if (!CheckFlg)
        {
            moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;

            inputHorizontal = Input.GetAxisRaw("Horizontal");
            inputVertical = Input.GetAxisRaw("Vertical");
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
        GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + transform.TransformDirection(moveDir) * moveSpeed * Time.deltaTime);

    }
}
