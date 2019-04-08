using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//using static;

public class Test_ItemManager : MonoBehaviour {

    public enum Size
    {
        SHOU,
        CHU,
        DAI,
        TOKUDAI,

        MAX,
    }
    Size PlayerStatus=Size.CHU;
    
    int shouNum = 0;
    int chuNum = 0;
    int daiNum = 0;
    int tokudaiNum = 0;

    float Capacity;



    float CurrentWeight = 0;


    public struct Item
    {
        public Size size;
        public int weight;
        public int num;
    }
    Item[] item = new Item[(int)Size.MAX];




    // Use this for initialization
    void Start()
    {
        ItemInit();




    }

    // Update is called once per frame
    void Update () {
		




	}


    public void PopItem(string tag)
    {
    }

    public bool PushItem(string tag)
    {
        switch (tag)
        {
            case "SHOU":
                shouNum++;
                break;

            case "CHU":

                break;

            case "DAI":

                break;

            case "TOKUDAI":

                break;
        }


        return true;
    }


    public float GetWeight()
    {
        return 0;
    }


    //（仮）
    private void CheckWeight()
    {
        if (CurrentWeight >= 30)
        {
            PlayerStatus = Size.TOKUDAI;
        }
        else if (CurrentWeight >= 20)
        {
            PlayerStatus = Size.DAI;

        }
        else if (CurrentWeight >= 10)
        {
            PlayerStatus = Size.CHU;
        }
        else
        {
            PlayerStatus = Size.SHOU;
        }
    }


    //（仮）
    private void ItemInit()
    {
        item[(int)Size.SHOU].size = Size.SHOU;
        item[(int)Size.SHOU].weight = 1;
        item[(int)Size.SHOU].num = 0;

        item[(int)Size.CHU].size = Size.CHU;
        item[(int)Size.CHU].weight = 2;
        item[(int)Size.CHU].num = 0;

        item[(int)Size.DAI].size = Size.DAI;
        item[(int)Size.DAI].weight = 3;
        item[(int)Size.DAI].num = 0;

        item[(int)Size.TOKUDAI].size = Size.TOKUDAI;
        item[(int)Size.TOKUDAI].weight = 4;
        item[(int)Size.TOKUDAI].num = 0;

    }

}
