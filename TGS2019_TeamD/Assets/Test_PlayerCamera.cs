using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_PlayerCamera : MonoBehaviour {

    [SerializeField] public GameObject player;
    [SerializeField] public GameObject Hand;
    [SerializeField] public GameObject LockOnMarker;

    Vector3 targetPos;
    private bool RayHitFlg = false;
    RaycastHit hit;

    // Use this for initialization
    void Start () {
        
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && RayHitFlg && Hand.GetComponent<PlayerHand>().state == PlayerHand.State.Normal)
        {
            Hand.GetComponent<PlayerHand>().state = PlayerHand.State.Firing;
            Hand.GetComponent<PlayerHand>().targetPos = hit.point;
            Debug.Log(hit.point);
            player.GetComponent<Test_PlayerContllor>().CheckFlg = true;
        }
        else
        {
            Ray();
        }
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
            }
            else
            {
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
