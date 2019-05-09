using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour {

    public enum State{
        Firing,
        Landing,
        Return,
        Pull,
        Normal,
    }
   [HideInInspector] public State state;

    private float speed = 30.0f;
    private Vector3 InitPos;
    private Quaternion InitRot;
    private GameObject ItemObj;
    [HideInInspector] public bool firingFlg = false;
    [HideInInspector] public Vector3 targetPos;

    public GameObject text;
    public GameObject player;
    public UIscript Ui;

    // Use this for initialization
    void Start () {
        InitPos = transform.localPosition;
        InitRot = transform.localRotation;
        state = State.Normal;
    }
	
	// Update is called once per frame
	void Update () {
       
        switch (state)
        {
            case State.Firing:
                transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime * 0.8f);
                Debug.Log(targetPos);
                speed += -Time.deltaTime * 10.0f;
                break;
            case State.Landing:
                if (Input.GetMouseButtonDown(0))
                {
                    transform.parent = null;
                    text.SetActive(false);
                    state = State.Pull;
                }
                if (Input.GetMouseButtonDown(1))
                {
                    text.SetActive(false);
                    state = State.Return;
                }
                break;
            case State.Return:
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, InitPos, speed * Time.deltaTime * 0.8f);
                ItemObj.transform.parent = this.gameObject.transform;
                speed += -Time.deltaTime * 10.0f;
                text.SetActive(false);
                if (transform.localPosition == InitPos)
                {
                    ItemCnt(ItemObj);
                    speed = 30.0f;
                    state = State.Normal;
                }
                break;
            case State.Pull:
                player.transform.position = Vector3.MoveTowards(player.transform.position, transform.position, speed * Time.deltaTime * 0.8f);
                float dis = Vector3.Distance(transform.position, player.transform.position);
                if (dis < 1.405f)
                {
                    transform.parent = GameObject.Find("Player").transform;
                    transform.localPosition = InitPos;
                    speed = 30.0f;
                    state = State.Normal;
                }
                Debug.Log(dis);
                break;
            case State.Normal:
                transform.localPosition = InitPos;
                transform.localRotation = InitRot;
                player.GetComponent<Test_PlayerContllor>().CheckFlg = false;
                break;
        }
        Debug.Log(state);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(state == State.Firing)
        {
            speed = 30.0f;
            state = State.Landing;
            text.SetActive(true);
            Debug.Log("touch");
        }
       
        if(other.tag == "S_Item" || other.tag == "M_Item" || other.tag == "L_Item" || other.tag == "XL_Item")
        {
            ItemObj = other.gameObject;
        }
    }

    void ItemCnt(GameObject Item)
    {
        if(Item.tag == "XL_Item")
        {
            Ui.xl++;
        }
        else if(Item.tag == "L_Item")
        {
            Ui.l++;
        }else if(Item.tag == "M_Item")
        {
            Ui.m++;
        }else if(Item.tag == "S_Item")
        {
            Ui.s++;
        }
        Destroy(Item);
    }
}
