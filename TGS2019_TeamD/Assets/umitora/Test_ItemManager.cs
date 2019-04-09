using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//using static;

public class Test_ItemManager : MonoBehaviour
{

    const int CAPACITY = 40;
    const int PLAYER_WEIGHT_S = 0;
    const int PLAYER_WEIGHT_M = 10;
    const int PLAYER_WEIGHT_L = 20;
    const int PLAYER_WEIGHT_XL = 30;

    public enum Size
    {
        S,
        M,
        L,
        XL,

        MAX,
    }


    int CurrentWeight = 0;

    Size PlayerWeightSize = Size.M;
    Size PlayerPower = Size.M;

    //Size WeightGroup=

    public struct ITEM
    {
        public Size size;
        public int weight;
        public int num;
    }
    ITEM[] Item = new ITEM[(int)Size.MAX];



    // Use this for initialization
    void Start()
    {
        ItemInit(); //アイテムの個数とか重さとかの初期化




    }

    // Update is called once per frame
    void Update()
    {





    }

    //アイテムそれぞれが何個あるか取得する
    public void GetItemNum(ref int[] tmp)
    {
        tmp[0] = Item[(int)Size.S].num;
        tmp[1] = Item[(int)Size.M].num;
        tmp[2] = Item[(int)Size.L].num;
        tmp[3] = Item[(int)Size.XL].num;



    }

    //アイテムを出す
    public void PopItem(string tag)
    {
        switch (tag)
        {
            case "S_Item":
                Item[(int)Size.S].num--;
                CurrentWeight -= Item[(int)Size.S].weight;

                break;

            case "M_Item":
                Item[(int)Size.M].num--;
                CurrentWeight -= Item[(int)Size.M].weight;

                break;

            case "L_Item":
                Item[(int)Size.L].num--;
                CurrentWeight -= Item[(int)Size.L].weight;

                break;

            case "XL_Item":
                Item[(int)Size.XL].num--;
                CurrentWeight -= Item[(int)Size.XL].weight;

                break;
        }





    }
    //アイテムを入れる
    public bool PushItem(string tag)
    {
        switch (tag)
        {
            case "S_Item":
                //int index = (int)Size.SHOU;
                if (CurrentWeight + Item[(int)Size.S].weight <= CAPACITY)
                {
                    CurrentWeight += Item[(int)Size.S].weight;
                    Item[(int)Size.S].num++;
                }
                else
                    return false;

                break;

            case "M_Item":
                if (CurrentWeight + Item[(int)Size.M].weight <= CAPACITY)
                {
                    CurrentWeight += Item[(int)Size.M].weight;
                    Item[(int)Size.M].num++;

                }
                else
                    return false;

                break;

            case "L_Item":
                if (CurrentWeight + Item[(int)Size.L].weight <= CAPACITY)
                {
                    CurrentWeight += Item[(int)Size.L].weight;
                    Item[(int)Size.L].num++;

                }
                else
                    return false;

                break;

            case "XL_Item":
                if (CurrentWeight + Item[(int)Size.XL].weight <= CAPACITY)
                {
                    CurrentWeight += Item[(int)Size.XL].weight;
                    Item[(int)Size.XL].num++;

                }
                else
                    return false;

                break;
        }


        return true;
    }

    //プレイヤーの重さを取得
    public int GetWeight()
    {
        return CurrentWeight;
    }

    //もうなんか書くのめんどくなったけどプレイヤーとアイテムの重さを比較するやつ
    public string ComparisonWeight(string tag)
    {
        switch (tag)
        {
            case "S_Item":
                if (CurrentWeight >= PLAYER_WEIGHT_M)
                    return "Player";
                else
                    return "Same";

            //break;
            case "M_Item":
                if (CurrentWeight >= PLAYER_WEIGHT_L)
                    return "Player";
                else if (CurrentWeight <= PLAYER_WEIGHT_S)
                    return "Item";
                break;

            case "L_Item":
                if (CurrentWeight >= PLAYER_WEIGHT_XL)
                    return "Player";
                else if (CurrentWeight <= PLAYER_WEIGHT_M)
                    return "Item";

                break;

            case "XL_Item":
                if (CurrentWeight <= PLAYER_WEIGHT_L)
                    return "Item";

                break;
        }


        return "Same";
    }

    //（仮）　重さが変わったらプレイヤーの重さを変えたりするやつ
    private void CheckWeight()
    {
        if (CurrentWeight >= PLAYER_WEIGHT_XL)
        {
            PlayerWeightSize = Size.XL;
        }
        else if (CurrentWeight >= PLAYER_WEIGHT_L)
        {
            PlayerWeightSize = Size.L;

        }
        else if (CurrentWeight >= PLAYER_WEIGHT_M)
        {
            PlayerWeightSize = Size.M;
        }
        else
        {
            PlayerWeightSize = Size.S;
        }
    }


    private void CheckPlayerPower()
    {

        int p = 1;

        if (Item[(int)Size.S].num * p >= PLAYER_WEIGHT_XL)
        {
            PlayerPower = Size.XL;
        }
        else if (Item[(int)Size.S].num * p >= PLAYER_WEIGHT_L)
        {
            PlayerPower = Size.L;
        }
        else if(Item[(int)Size.S].num * p >= PLAYER_WEIGHT_M)
        {
           PlayerPower= Size.M;
        }
        else
        {
            PlayerPower = Size.S;
        }

        //PlayerPower = Item[(int)Size.S].num * p;




    }



    public bool CarryItem()
    {
        if (PlayerPower >= PlayerWeightSize)
        {
            return true;
        }

        return false;
    }



    //（仮）アイテムの初期化
    private void ItemInit()
    {
        Item[(int)Size.S].size = Size.S;
        Item[(int)Size.S].weight = 1;
        Item[(int)Size.S].num = 0;

        Item[(int)Size.M].size = Size.M;
        Item[(int)Size.M].weight = 2;
        Item[(int)Size.M].num = 0;

        Item[(int)Size.L].size = Size.L;
        Item[(int)Size.L].weight = 3;
        Item[(int)Size.L].num = 0;

        Item[(int)Size.XL].size = Size.XL;
        Item[(int)Size.XL].weight = 4;
        Item[(int)Size.XL].num = 0;

    }

}