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

    [SerializeField] public GameObject text;
    [SerializeField] public GameObject player;

    // Use this for initialization
    void Start () {
        InitPos = transform.localPosition;
        InitRot = transform.localRotation;
        Debug.Log(InitPos);
        state = State.Normal;
    }
	
	// Update is called once per frame
	void Update () {
       
        switch (state)
        {
            case State.Firing:
                transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime * 0.8f);
                speed += -Time.deltaTime * 10.0f;
                break;
            case State.Landing:
                if (Input.GetMouseButtonDown(0))
                {
                    transform.parent = null;
                    state = State.Pull;
                }
                if (Input.GetMouseButtonDown(1))
                {
                    state = State.Return;
                }
                break;
            case State.Return:
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, InitPos, speed * Time.deltaTime * 0.8f);
                ItemObj.transform.position = transform.position;
                speed += -Time.deltaTime * 10.0f;
                text.SetActive(false);
                if (transform.localPosition == InitPos) state = State.Normal;
                break;
            case State.Pull:
                player.transform.position = Vector3.MoveTowards(player.transform.position, transform.position, speed * Time.deltaTime * 0.8f);
                if (player.transform.position == transform.position)
                {
                    transform.parent = GameObject.Find("Player").transform;
                    transform.localPosition = InitPos;
                    state = State.Normal;
                }
                text.SetActive(false);
                break;
            case State.Normal:
                transform.localPosition = InitPos;
                transform.localRotation = InitRot;
                player.GetComponent<PlayerController>().CheckFlg = false;
                break;
        }
        //Debug.Log(state);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(state == State.Firing)
        {
            speed = 30.0f;
            state = State.Landing;
            text.SetActive(true);
        }
        else
        {

        }
       
        if(other.tag == "S_Item" || other.tag == "M_Item" || other.tag == "L_Item" || other.tag == "XL_Item")
        {
            ItemObj = other.gameObject;
        }
    }
}
