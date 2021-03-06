﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//using static;

public class PlayerItemManager : MonoBehaviour
{

    int CAPACITY;    //アイテムを持てる最大の重量     //********************************************************

    ////アイテムの重さの区分
    int PLAYER_WEIGHT_S;
    int PLAYER_WEIGHT_M;
    int PLAYER_WEIGHT_L;
    int PLAYER_WEIGHT_XL;




    //区分
    public enum Size
    {
        S,
        M,
        L,
        XL,

        MAX,
    }

    //アイテムの構造体
    public struct ITEM
    {
        public Size size;       //重さ(size型)
        public int weight;      //重さ(int)
        public int num;         //個数
    }
    ITEM[] Item = new ITEM[(int)Size.MAX];




    int PlayerWeight = 0;     //プレイヤーの重さ(int)

    Size PlayerWeightDivision;     //プレイやの重さ(Size型)
    Size PlayerPowerDivision;      //プレイヤーのパワー(Size型)


    float S_ItemPower;                    //S_Item一個のバフ





    private void Awake()
    {
        ItemInit(); //アイテムの個数とか重さとかの初期化

    }

    // Use this for initialization
    void Start()
    {
        //ItemInit(); //アイテムの個数とか重さとかの初期化


        //playerTag = "Player";
        //itemTag[(int)Size.S] = "S_Item";
        //itemTag[(int)Size.M] = "M_Item";
        //itemTag[(int)Size.L] = "L_Item";
        //itemTag[(int)Size.XL] = "XL_Item";

    }

    // Update is called once per frame
    void Update()
    {
    }

    /***********************************************************************
    **  private関数
    ************************************************************************/


    //（仮）アイテムの初期化       **************************************************いじってほしいとこ**************************************************
    private void ItemInit()
    {
        //********************************************************************************************************************************************

        CAPACITY = 100;    //アイテムを持てる最大の重量     

        //アイテムの重さの区分
        PLAYER_WEIGHT_S = 0;
        PLAYER_WEIGHT_M = CAPACITY / 4;
        PLAYER_WEIGHT_L = CAPACITY / 4 * 2;
        PLAYER_WEIGHT_XL = CAPACITY / 4 * 3;


        //アイテム一個一個の情報
        Item[(int)Size.S].size = Size.S;    //サイズの区分
        Item[(int)Size.S].weight = 5;       //1個あたりの重さ
        Item[(int)Size.S].num = 5;          //最初からプレイヤーが持っている数

        Item[(int)Size.M].size = Size.M;
        Item[(int)Size.M].weight = 15;
        Item[(int)Size.M].num = 2;

        Item[(int)Size.L].size = Size.L;
        Item[(int)Size.L].weight = 30;
        Item[(int)Size.L].num = 1;

        Item[(int)Size.XL].size = Size.XL;
        Item[(int)Size.XL].weight = 60;
        Item[(int)Size.XL].num = 0;


        S_ItemPower = 10;       //S_Item1個につきどのぐらいパワーがあるか


        //***************************************************************************************************************************



        foreach (ITEM i in Item)
        {
            PlayerWeight += i.weight * i.num;
        }

        CheckPlayerWeightDivision();
        CheckPlayerPowerDivision();
    }



    //プレイヤーのsizeの重さを、intの重さによって変える
    private void CheckPlayerWeightDivision()
    {
        if (PlayerWeight >= PLAYER_WEIGHT_XL)
        {
            PlayerWeightDivision = Size.XL;
        }
        else if (PlayerWeight >= PLAYER_WEIGHT_L)
        {
            PlayerWeightDivision = Size.L;

        }
        else if (PlayerWeight >= PLAYER_WEIGHT_M)
        {
            PlayerWeightDivision = Size.M;
        }
        else
        {
            PlayerWeightDivision = Size.S;
        }
    }



    //プレイヤーのパワーをバフに応じて変える
    private void CheckPlayerPowerDivision()
    {

        if (Item[(int)Size.S].num * S_ItemPower >= PLAYER_WEIGHT_XL)
        {
            PlayerPowerDivision = Size.XL;
        }
        else if (Item[(int)Size.S].num * S_ItemPower >= PLAYER_WEIGHT_L)
        {
            PlayerPowerDivision = Size.L;
        }
        else if (Item[(int)Size.S].num * S_ItemPower >= PLAYER_WEIGHT_M)
        {
            PlayerPowerDivision = Size.M;
        }
        else
        {
            PlayerPowerDivision = Size.S;
        }


    }





    /***********************************************************************
    **  public関数
    ************************************************************************/


    //もうなんか書くのめんどくなったけどプレイヤーとアイテムの重さを比較するやつ
    public string ComparisonWeight(string tag)
    {
        switch (tag)
        {
            case "S_Item":
                if (PlayerWeight >= PLAYER_WEIGHT_M)
                    return "Player";
                else
                    return "Same";

            //break;
            case "M_Item":
                if (PlayerWeight >= PLAYER_WEIGHT_L)
                    return "Player";
                else if (PlayerWeight <= PLAYER_WEIGHT_S)
                    return "Item";
                break;

            case "L_Item":
                if (PlayerWeight >= PLAYER_WEIGHT_XL)
                    return "Player";
                else if (PlayerWeight <= PLAYER_WEIGHT_M)
                    return "Item";

                break;

            case "XL_Item":
                if (PlayerWeight <= PLAYER_WEIGHT_L)
                    return "Item";

                break;
        }


        return "Same";
    }



    //アイテムを入れる
    public bool PushItem(string tag)
    {
        bool bReturn = false;

        switch (tag)
        {
            case "S_Item":
                if (PlayerWeight + Item[(int)Size.S].weight <= CAPACITY)
                {
                    PlayerWeight += Item[(int)Size.S].weight;
                    Item[(int)Size.S].num++;
                    bReturn = true;
                }

                break;

            case "M_Item":
                if (PlayerWeight + Item[(int)Size.M].weight <= CAPACITY)
                {
                    PlayerWeight += Item[(int)Size.M].weight;
                    Item[(int)Size.M].num++;
                    bReturn = true;
                }

                break;

            case "L_Item":
                if (PlayerWeight + Item[(int)Size.L].weight <= CAPACITY)
                {
                    PlayerWeight += Item[(int)Size.L].weight;
                    Item[(int)Size.L].num++;
                    bReturn = true;
                }

                break;

            case "XL_Item":
                if (PlayerWeight + Item[(int)Size.XL].weight <= CAPACITY)
                {
                    PlayerWeight += Item[(int)Size.XL].weight;
                    Item[(int)Size.XL].num++;
                    bReturn = true;
                }

                break;
        }

        if (bReturn)
        {
            CheckPlayerPowerDivision();
            CheckPlayerWeightDivision();
            return true;
        }
        else
        {
            return false;
        }

    }



    //アイテムを出す
    public bool PopItem(string tag)
    {
        bool bReturn = false;
        switch (tag)
        {
            case "S_Item":
                if (Item[(int)Size.S].num >= 1)
                {
                    Item[(int)Size.S].num--;
                    PlayerWeight -= Item[(int)Size.S].weight;
                    bReturn = true;
                }

                break;

            case "M_Item":
                if (Item[(int)Size.M].num >= 1)
                {
                    Item[(int)Size.M].num--;
                    PlayerWeight -= Item[(int)Size.M].weight;
                    bReturn = true;
                }

                break;

            case "L_Item":
                if (Item[(int)Size.L].num >= 1)
                {
                    Item[(int)Size.L].num--;
                    PlayerWeight -= Item[(int)Size.L].weight;
                    bReturn = true;
                }

                break;

            case "XL_Item":
                if (Item[(int)Size.XL].num >= 1)
                {
                    Item[(int)Size.XL].num--;
                    PlayerWeight -= Item[(int)Size.XL].weight;
                    bReturn = true;
                }

                break;
        }
        if (bReturn)
        {
            CheckPlayerPowerDivision();
            CheckPlayerWeightDivision();
            return true;
        }
        else
        {
            return false;
        }

    }

    //Sizeでプレイヤーのパワーと重さを比較して持てるか取得する
    public bool CanCarryItem()
    {
        if (PlayerPowerDivision >= PlayerWeightDivision)
        {
            return true;
        }

        return false;
    }

    //プレイヤーの重さを割合で取得
    public float GetWeightRatio()
    {
        return (float)PlayerWeight / CAPACITY;
    }
    //プレイヤーの重さをSizeで取得
    public Size GetWeightDivision()
    {
        return PlayerWeightDivision;
    }
    //プレイヤーのパワーを割合で取得
    public float GetPowerRatio()
    {
        return Item[(int)Size.S].num * S_ItemPower / CAPACITY;//*********************************************************
    }
    public float GetWeight()
    {
        return PlayerWeight;
    }

    //アイテムそれぞれが何個あるか取得する
    public void GetItemNum(out int[] tmp)
    {
        tmp = new int[4];
        tmp[(int)Size.S] = Item[(int)Size.S].num;
        tmp[(int)Size.M] = Item[(int)Size.M].num;
        tmp[(int)Size.L] = Item[(int)Size.L].num;
        tmp[(int)Size.XL] = Item[(int)Size.XL].num;
    }




    //アイテムを入れる
    public bool CanPushItem(string tag)
    {
        bool bReturn = false;

        switch (tag)
        {
            case "S_Item":
                if (PlayerWeight + Item[(int)Size.S].weight <= CAPACITY)
                {
                    bReturn = true;
                }

                break;

            case "M_Item":
                if (PlayerWeight + Item[(int)Size.M].weight <= CAPACITY)
                {
                    bReturn = true;
                }

                break;

            case "L_Item":
                if (PlayerWeight + Item[(int)Size.L].weight <= CAPACITY)
                {
                    bReturn = true;
                }

                break;

            case "XL_Item":
                if (PlayerWeight + Item[(int)Size.XL].weight <= CAPACITY)
                {
                    bReturn = true;
                }

                break;
        }

        if (bReturn)
        {

            return true;
        }
        else
        {
            return false;
        }

    }




}