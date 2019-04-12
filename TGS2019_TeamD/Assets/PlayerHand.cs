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
    [HideInInspector] public bool firingFlg = false;
    [HideInInspector] public Vector3 targetPos;

    [SerializeField] public GameObject text;
    [SerializeField] public GameObject player;

    // Use this for initialization
    void Start () {
        InitPos = transform.localPosition;
        state = State.Normal;
    }
	
	// Update is called once per frame
	void Update () {
        //if (firingFlg)
        //{
        //    transform.position = Vector3.MoveTowards(transform.position, targetPos, speed*Time.deltaTime*0.8f);
        //    speed += -Time.deltaTime * 10.0f;
        //}

        switch (state)
        {
            case State.Firing:
                transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime * 0.8f);
                speed += -Time.deltaTime * 10.0f;
                break;
            case State.Landing:
                if (Input.GetMouseButtonDown(0))
                {
                    state = State.Pull;
                }
                if (Input.GetMouseButtonDown(1))
                {
                    state = State.Return;
                }
                break;
            case State.Return:
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, InitPos, speed * Time.deltaTime * 0.8f);
                speed += -Time.deltaTime * 10.0f;
                text.SetActive(false);
                if (transform.localPosition == InitPos) state = State.Normal;
                break;
            case State.Pull:
                player.transform.position = Vector3.MoveTowards(player.transform.position, transform.position, speed * Time.deltaTime * 0.8f);
                if (player.transform.position == transform.position) state = State.Normal;
                text.SetActive(false);
                Debug.Log(player.transform.position);
                Debug.Log(transform.position);
                break;
            case State.Normal:
                player.GetComponent<PlayerController>().CheckFlg = false;
                break;
        }
        Debug.Log(state);
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (firingFlg)
        //{
        //    speed = 30.0f;
        //    firingFlg = false;
        //    text.SetActive(true);
        //}
        if(state == State.Firing)
        {
            speed = 30.0f;
            state = State.Landing;
            text.SetActive(true);
        }
        //if(state == State.Return)
        //{
        //    speed = 30.0f;
        //    state = State.Normal;
        //    transform.localPosition = InitPos;
        //}
    }
}
