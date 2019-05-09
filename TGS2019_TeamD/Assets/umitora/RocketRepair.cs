using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class RocketRepair : MonoBehaviour {

    PlayerItemManager myPlayerItemManager;


    public enum Size
    {
        S,
        M,
        L,
        XL,

        MAX,
    }


    int[] MaxItemNum=new int[(int)Size.MAX];
    int[] CurrentItemNum= new int[(int)Size.MAX];


	// Use this for initialization
	void Start () {




        ItemInit();

       

	}
	
	// Update is called once per frame
	void Update () {
		







	}


    private void ItemInit()
    {
        //************************************************いじってほしいとこ***************************************************************                

        MaxItemNum[(int)Size.S] = 20;
        CurrentItemNum[(int)Size.S] = 0;

        MaxItemNum[(int)Size.M] = 10;
        CurrentItemNum[(int)Size.M] = 0;

        MaxItemNum[(int)Size.L] = 5;
        CurrentItemNum[(int)Size.L] = 0;

        MaxItemNum[(int)Size.XL] = 2;
        CurrentItemNum[(int)Size.XL] = 0;



      //  *********************************************************************************************************************************

    }





    /***********************************************************************
    **  public関数
    ************************************************************************/



    public bool IsFinishRepair()
    {

        for(int i = 0; i < (int)Size.MAX; i++)
        {
            if (MaxItemNum[i] != CurrentItemNum[i])
            {
                return false;
            }
        }

        return true;
    }


    public bool PushItem(string tag)
    {
        switch (tag)
        {
            case "S_Item":
                if (CurrentItemNum[(int)Size.S] + 1<=MaxItemNum[(int)Size.S])
                {
                    CurrentItemNum[(int)Size.S]++;
                    return true;
                }
                break;

            case "M_Item":
                if (CurrentItemNum[(int)Size.M] + 1 <= MaxItemNum[(int)Size.M])
                {
                    CurrentItemNum[(int)Size.M]++;
                    return true;

                }

                break;

            case "L_Item":
                if (CurrentItemNum[(int)Size.L] + 1 <= MaxItemNum[(int)Size.L])
                {
                    CurrentItemNum[(int)Size.L]++;
                    return true;

                }

                break;

            case "XL_Item":
                if (CurrentItemNum[(int)Size.XL] + 1 <= MaxItemNum[(int)Size.XL])
                {
                    CurrentItemNum[(int)Size.XL]++;
                    return true;

                }

                break;


        }


        return false;
    }


    public  bool PopItem(string tag)
    {

        switch (tag)
        {
            case "S_Item":
                if (CurrentItemNum[(int)Size.S] - 1 >=0)
                {
                    CurrentItemNum[(int)Size.S]--;
                    return true;

                }

                break;

            case "M_Item":
                if (CurrentItemNum[(int)Size.M] - 1 >= 0)
                {
                    CurrentItemNum[(int)Size.M]--;
                    return true;

                }

                break;

            case "L_Item":
                if (CurrentItemNum[(int)Size.L] - 1 >= 0)
                {
                    CurrentItemNum[(int)Size.L]--;
                    return true;

                }

                break;

            case "XL_Item":
                if (CurrentItemNum[(int)Size.XL] - 1 >= 0)
                {
                    CurrentItemNum[(int)Size.XL]--;
                    return true;

                }

                break;


        }

        return false;
    }


    //アイテムそれぞれが何個あるか取得する
    public void GetItemNum(out int[] tmp)
    {
        tmp = new int[4];
        tmp[(int)Size.S] = CurrentItemNum[(int)Size.S];
        tmp[(int)Size.M] = CurrentItemNum[(int)Size.M];
        tmp[(int)Size.L] = CurrentItemNum[(int)Size.L];
        tmp[(int)Size.XL] = CurrentItemNum[(int)Size.XL];
    }

}
