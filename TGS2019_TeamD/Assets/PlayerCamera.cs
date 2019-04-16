using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    RaycastHit hit;
    GameObject targetObj;
    Vector3 targetPos;

    [SerializeField] public GameObject player;
    [SerializeField] public GameObject Hand;
    [SerializeField] public GameObject LockOnMarker;

    private bool RayHitFlg = false;

    void Start()
    {
        //targetObj = GameObject.Find("Player");
        targetPos = player.transform.position;
    }

    void Update()
    {
        // targetの移動量分、自分（カメラ）も移動する
        transform.position += player.transform.position - targetPos;
        targetPos = player.transform.position;
        if (!player.GetComponent<PlayerController>().CheckFlg)
        {
            // マウスの移動量
            float mouseInputX = Input.GetAxis("Mouse X");
            float mouseInputY = Input.GetAxis("Mouse Y");
            // targetの位置のY軸を中心に、回転（公転）する
            transform.RotateAround(targetPos, Vector3.up, mouseInputX * Time.deltaTime * 200f);
            // カメラの垂直移動
            transform.RotateAround(targetPos, transform.right, mouseInputY * Time.deltaTime * 200f);
        }

        if (Input.GetMouseButtonDown(0) && RayHitFlg && Hand.GetComponent<PlayerHand>().state == PlayerHand.State.Normal)
        {
            Hand.GetComponent<PlayerHand>().state = PlayerHand.State.Firing;
            Hand.GetComponent<PlayerHand>().targetPos = hit.point;
            player.GetComponent<PlayerController>().TagPos = hit.point;
            player.GetComponent<PlayerController>().CheckFlg = true;
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
        int distance = 30;
        Debug.DrawLine(ray.origin, ray.direction * distance, Color.red);
        if (Physics.Raycast(ray, out hit, 30.0f))
        {
            if (hit.collider.tag != "GameObj")
            {
                // hit.point が正面方向へRayをとばした際の接触座標.
                RayHitFlg = true;
                LockOnMarker.SetActive(true);
            }
        }
        else
        {
            RayHitFlg = false;
            LockOnMarker.SetActive(false);
        }
    }
}
