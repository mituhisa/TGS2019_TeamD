using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubbleCreate : MonoBehaviour {
    [SerializeField] private int RubbleNumber;
    [SerializeField] private float RubblePosition_X, RubblePosition_Y, RubblePosition_Z;
    [SerializeField] Vector3 SpawnPoint;
    [SerializeField] private float SpawnRange_Max,SpawnRange_Min;
    public GameObject Rubble_prefab; 

	// Use this for initialization
	void Start () {
        RubbleNumber = Random.Range(10,20);
        Debug.Log("瓦礫生成数" + RubbleNumber);
        SpawnRange_Max = 3;
        SpawnRange_Min = -3;
        Debug.Log("瓦礫の数" + RubbleNumber);
	}
	
	// Update is called once per frame
	void Update () {
        if (RubbleNumber > 0)
        {
            GameObject CloneRabble = Instantiate(Rubble_prefab) as GameObject;
            CloneRabble.GetComponent<RubbleGravity>().Planet = this.transform;
           // CloneRabble.GetComponent<RubbleMove>().Planet = this.gameObject;
            CloneRabble.transform.position = this.gameObject.transform.position;
            RubblePosition_X = Random.Range(SpawnRange_Min, SpawnRange_Max);
            RubblePosition_Y = Random.Range(SpawnRange_Min, SpawnRange_Max);
            RubblePosition_Z = Random.Range(SpawnRange_Min, SpawnRange_Max);
            Debug.Log("瓦礫生成場所＿X座標" + RubblePosition_X);
            Debug.Log("瓦礫生成場所＿Y座標" + RubblePosition_Y);
            Debug.Log("瓦礫生成場所＿Z座標" + RubblePosition_Z);
            SpawnPoint = new Vector3((this.transform.position.x /*+ (this.transform.localScale.x/2)*/) + RubblePosition_X *5, (this.transform.position.y /*+ (this.transform.localScale.y/2)*/) + RubblePosition_Y*5,(this.transform.position.z /*+ (this.transform.localScale.z/2)*/) + RubblePosition_Z *5);
            CloneRabble.transform.position = SpawnPoint;
            RubbleNumber--;
        }
	}
}
