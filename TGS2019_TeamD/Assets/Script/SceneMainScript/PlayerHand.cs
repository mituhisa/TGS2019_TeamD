using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{

    public enum State
    {
        Firing,
        Landing,
        Return,
        Pull,
        Release,
        Normal,
    }
    [HideInInspector] public State state;

    private bool CanPushFlg;
    private float speed = 30.0f;
    private Vector3 InitPos;
    private Quaternion InitRot;
    private GameObject ItemObj;
    private bool SoundFlg = false;
    [HideInInspector] public bool firingFlg = false;
    [HideInInspector] public Vector3 targetPos;

    public GameObject player;
    public UIscript Ui;
    public GameObject PlayerCamera;
    public PlayerItemManager PIManager;
    public AudioClip BackArmSe;

    // Use this for initialization
    void Start()
    {
        InitPos = transform.localPosition;
        InitRot = transform.localRotation;
        state = State.Normal;
    }

    // Update is called once per frame
    void Update()
    {

        switch (state)
        {
            case State.Firing:
                transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime * 0.8f);
                //speed += -Time.deltaTime * 10.0f;
                break;
            case State.Landing:
                if (CanPushFlg)
                {
                    ItemObj.transform.parent = this.gameObject.transform;
                    state = State.Return;
                }
                else
                {
                    state = State.Release;
                }
                break;
            //if (ItemTex == "Player")
            //{
            //    ItemObj.transform.parent = this.gameObject.transform;
            //    state = State.Return;
            //}
            //else if (ItemTex == "Item")
            //{
            //    state = State.Release;
            //}
            //else if (ItemTex == "Same")
            //{
            //    state = State.Release;
            //}
            //break;
            case State.Return:
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, InitPos, speed * Time.deltaTime * 0.8f);
                if (transform.localPosition == InitPos)
                {
                    if (!this.gameObject.GetComponent<AudioSource>().isPlaying && !SoundFlg)
                    {
                        this.gameObject.GetComponent<AudioSource>().PlayOneShot(BackArmSe);
                        SoundFlg = true;
                    }
                    speed = 30.0f;
                    PlayerCamera.GetComponent<Test_PlayerCamera>().anim.SetBool("Shot", false);
                    PlayerCamera.GetComponent<Test_PlayerCamera>().anim.speed = 1;
                    if(ItemObj != null) PIManager.PushItem(ItemObj.tag);
                    Destroy(ItemObj);
                    player.GetComponent<Test_PlayerContllor>().CheckFlg = false;
                    if (!this.gameObject.GetComponent<AudioSource>().isPlaying && SoundFlg)
                    {
                        SoundFlg = false;
                        state = State.Normal;
                    }
                }
                break;
            case State.Pull:
                player.transform.position = Vector3.MoveTowards(player.transform.position, transform.position, speed * Time.deltaTime * 0.8f);
                float dis = Vector3.Distance(transform.position, player.transform.position);
                if (dis < 1.405f)
                {
                    PlayerCamera.GetComponent<Test_PlayerCamera>().anim.SetBool("Shot", false);
                    PlayerCamera.GetComponent<Test_PlayerCamera>().anim.speed = 1;
                    transform.parent = GameObject.Find("Player").transform;
                    transform.localPosition = InitPos;
                    speed = 30.0f;
                    player.GetComponent<Test_PlayerContllor>().CheckFlg = false;
                    state = State.Normal;
                }
                Debug.Log(dis);
                break;
            case State.Release:
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, InitPos, speed * Time.deltaTime * 0.8f);
                if (transform.localPosition == InitPos)
                {
                    if (!this.gameObject.GetComponent<AudioSource>().isPlaying && !SoundFlg)
                    {
                        this.gameObject.GetComponent<AudioSource>().PlayOneShot(BackArmSe);
                        SoundFlg = true;
                    }
                    speed = 30.0f;
                    PlayerCamera.GetComponent<Test_PlayerCamera>().anim.SetBool("Shot", false);
                    PlayerCamera.GetComponent<Test_PlayerCamera>().anim.speed = 1;
                    player.GetComponent<Test_PlayerContllor>().CheckFlg = false;
                    if (!this.gameObject.GetComponent<AudioSource>().isPlaying && SoundFlg)
                    {
                        SoundFlg = false;
                        state = State.Normal;
                    }
                }
                break;
            case State.Normal:
                transform.localPosition = InitPos;
                transform.localRotation = InitRot;
                break;
        }
        Debug.Log(state);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (state == State.Firing)
        {
            speed = 30.0f;
            state = State.Landing;
            CanPushFlg = PIManager.CanPushItem(other.tag);
        }

        if (other.tag == "S_Item" || other.tag == "M_Item" || other.tag == "L_Item" || other.tag == "XL_Item")
        {
            ItemObj = other.gameObject;
        }
        else
        {
            Debug.Log("ダメ");
        }
    }
}
