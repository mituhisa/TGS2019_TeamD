using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_PlayerContllor : MonoBehaviour {

    public float moveSpeed = 5f;
    private Vector3 moveDir;
    private Animator anim;

    float inputHorizontal;
    float inputVertical;

    AudioSource aud;
    public AudioClip WalkSe;
    public AudioClip ShotSe;
    private bool ShotFlg = false;

    [HideInInspector] public bool CheckFlg = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        aud = GetComponent<AudioSource>();
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

        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.nameHash != Animator.StringToHash("Base Layer.Shot"))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                moveSpeed = 15;
                anim.speed = 2;
                aud.pitch = 2.0f;
            }
            else if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                moveSpeed = 5;
                anim.speed = 1;
                aud.pitch = 1.0f;
            }
        }

        if (!aud.isPlaying)
        {
            if(stateInfo.nameHash == Animator.StringToHash("Base Layer.Walk"))
            {
                aud.PlayOneShot(WalkSe);
            }
        }
        else
        {
            if(stateInfo.nameHash == Animator.StringToHash("Base Layer.Stay"))
            {
                aud.Stop();
            }
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

        Debug.Log(CheckFlg);
    }

    private void FixedUpdate()
    {
        if (!CheckFlg)
        {
            GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + transform.TransformDirection(moveDir) * moveSpeed * Time.deltaTime);
        }
    }
}
