using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateStage_umitora : MonoBehaviour
{
    //球の座標+球の半径+オブジェクトの半径の座標にオブジェクト生成する


    public enum PREFAB_NAME
    {
        CUBE,
        SPHERE,


        MAX
    };


    GameObject[] Prefab = new GameObject[(int)PREFAB_NAME.MAX];

    Transform Planet;       //惑星の座標


    // Use this for initialization
    void Start()
    {

        Planet = GameObject.Find("Ground2_umitora").GetComponent<Transform>();      //惑星取得



        InitPrefab();           //プレハブを格納




        SetObject();       //惑星にオブジェクトを置いていく




    }

    // Update is called once per frame
    void Update()
    {








    }


    private void InitPrefab()
    {

        //Prefab = Resources.Load("Prefab/M_Item") as GameObject;

        Prefab[(int)PREFAB_NAME.CUBE] = Resources.Load("Prefab/CubePrefab") as GameObject;
        Prefab[(int)PREFAB_NAME.SPHERE] = Resources.Load("Prefab/SpherePrefab") as GameObject;



    }


    private void SetObject()
    {

        for (int i = 0; i < 30; i++)
        {
            int randomNumber = Random.Range(0, (int)PREFAB_NAME.MAX);
            GameObject instancePrefab = Instantiate(Prefab[randomNumber]);
            float randomScale = Random.Range(5f, 20);
            instancePrefab.transform.localScale = new Vector3(randomScale, randomScale, randomScale);
            //instancePrefab.transform.localScale *=randomScale;


            Vector3 randomVector = (new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1), Random.Range(-1f, 1))).normalized;


            instancePrefab.transform.position = Planet.position + randomVector * (Planet.localScale.x / 2 + instancePrefab.transform.localScale.x / 2);

            //p.transform.position = new Vector3(Planet.position.x+ randomVector.x + (p.transform.localScale.x / 2) + Planet.localScale.x / 2,
            //                                    Planet.position.y + randomVector.y + (p.transform.localScale.y / 2) + Planet.localScale.y / 2,
            //                                       Planet.position.z + randomVector.z + (p.transform.localScale.z / 2) + Planet.localScale.z / 2);

        }




    }

}