using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_PlayerCamera : MonoBehaviour
{

    [SerializeField] public GameObject player;
    [SerializeField] public GameObject Hand;
    [SerializeField] public GameObject Right;
    [SerializeField] public GameObject Left;
    [SerializeField] public GameObject LockOnMarker;

    [HideInInspector] public Animator anim;

    Vector3 targetPos;
    private bool RayHitFlg = false;
    private bool Fire = false;
    RaycastHit hit;

    private Transform InitParentR;
    private Transform InitParentL;

    [HideInInspector] public bool animFlg = false;

    AudioSource aud;
    public AudioClip ShotSe;
    public AudioClip RockOnSe;
    private bool RockOnFlg = false;

    // Use this for initialization
    void Start()
    {
        InitParentR = Right.transform.parent;
        InitParentL = Left.transform.parent;

        anim = player.GetComponent<Animator>();
        aud = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && RayHitFlg && Hand.GetComponent<PlayerHand>().state == PlayerHand.State.Normal)
        {
            targetPos = hit.point;
            anim.speed = 1;
            anim.SetBool("Shot", true);
            aud.PlayOneShot(ShotSe);
            //Right.transform.parent = Hand.transform;
            //Left.transform.parent = Hand.transform;
            //Hand.GetComponent<PlayerHand>().state = PlayerHand.State.Firing;
            //Hand.GetComponent<PlayerHand>().targetPos = hit.point;
            //Debug.Log(hit.point);
            //player.GetComponent<Test_PlayerContllor>().CheckFlg = true;
        }
        else
        {
            Ray();
        }

        if (Hand.GetComponent<PlayerHand>().state == PlayerHand.State.Normal)
        {
            Right.transform.parent = InitParentR;
            Left.transform.parent = InitParentL;
        }
        if (Fire)
        {
            if (!aud.isPlaying)
            {
                Right.transform.parent = Hand.transform;
                Left.transform.parent = Hand.transform;
                Hand.GetComponent<PlayerHand>().state = PlayerHand.State.Firing;
                Hand.GetComponent<PlayerHand>().targetPos = targetPos;
                player.GetComponent<Test_PlayerContllor>().CheckFlg = true;
                Fire = false;
            }
        }

        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.nameHash == Animator.StringToHash("Base Layer.Shot") && !animFlg)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.5f)
            {
                anim.speed = 0;
                Fire = true;
                animFlg = true;
            }
        }

        if (stateInfo.nameHash == Animator.StringToHash("Base Layer.Stay")) animFlg = false;
        //Debug.Log(Fire);
    }

    void Ray()
    {
        Vector3 center = new Vector3(Screen.width / 2, Screen.height / 2);
        Ray ray = Camera.main.ScreenPointToRay(center);
        Debug.DrawRay(ray.origin, ray.direction * 30, Color.red);
        if (Physics.Raycast(ray, out hit, 30.0f))
        {
            if (hit.collider.tag != "GameObj")
            {
                // hit.point が正面方向へRayをとばした際の接触座標.
                RayHitFlg = true;
                LockOnMarker.SetActive(true);
                if (!aud.isPlaying)
                {
                    if (!RockOnFlg)
                    {
                        aud.PlayOneShot(RockOnSe);
                        RockOnFlg = true;
                    }
                }
            }
            else
            {
                RockOnFlg = false;
                LockOnMarker.SetActive(false);
            }
        }
        else
        {
            RayHitFlg = false;
            LockOnMarker.SetActive(false);
        }
    }
}
