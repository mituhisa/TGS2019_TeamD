using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRandomSpawn : MonoBehaviour {
    public GameObject[] Rubble;
    [SerializeField] private int ObjectAmount = 0;
    [SerializeField] private float RandomPoint_X;
    [SerializeField] private float RandomPoint_Z;

    // Use this for initialization
    void Start()
    {
        ObjectAmount = Random.Range(80, 90);
    }

    // Update is called once per frame
    void Update()
    {

        if (ObjectAmount > 0)
        {
            RandomPoint_X = Random.Range(0.0f, 500.0f);
            Debug.Log("ランダム座標生成＿X地点" + RandomPoint_X);

            RandomPoint_Z = Random.Range(0.0f, 500.0f);
            Debug.Log("ランダム座標生成＿Y地点" + RandomPoint_Z);
            Vector3 PutPoint = new Vector3(RandomPoint_X, 30, RandomPoint_Z);
            GameObject CloneRubble = Instantiate(Rubble[0]) as GameObject;
            CloneRubble.transform.position = PutPoint;
            ObjectAmount--;

        }
    }
}
