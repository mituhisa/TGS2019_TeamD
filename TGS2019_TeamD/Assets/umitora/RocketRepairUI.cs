using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.EventSystems;


public class RocketRepairUI : MonoBehaviour
{
    //区分
    public enum Size
    {
        S,
        M,
        L,
        XL,

        MAX,
    }

    //選択しているオブジェクトの名前
public enum OBJECT
    {
        ROCKET,
        PLAYER,

        MAX,
    }

    int selectObject = (int)OBJECT.PLAYER;  //ボタンを選択する時にどのオブジェクトを選択しているか見るやつ
    int selectSize = (int)Size.S;         //ボタンを選択する時にどのサイズを選択しているか見るやつ


    GameObject[,] Buttons=new GameObject[(int)OBJECT.MAX,(int)Size.MAX];    //選択できるボタン


    RocketRepair myRocketRepair;            //ロケットのアイテムのスクリプト
    PlayerItemManager myPlayerItemManager;  //プレイヤーのアイテムのスクリプト


    GameObject RocketRepairImage;       //修理のUIの親のオブジェクト

    public GameObject Player;

    [SerializeField] GameObject FirstSelectButton;  //最初に選択されているボタン
    GameObject SelectButton;      //今選択しているボタン
    GameObject ButtonBuf;   //前のフレームで選択したボタン

    //GameObject[] ButtonEffect = new GameObject[2];  //ボタンを選択しているエフェクトみたいなやつ

    //IEnumerator cDisplayRocketRepair;       //ロケットを修理するUIを表示するやつ
    bool isRepairDisplay = false;


    Text PressButtonText;
    Text RocketdayoText;
    Text PlayerdayoText;



    //PlayerController2_umitora playerflag;//*****************************************************


    // Use this for initialization
    void Start()
    {
        //playerflag = GameObject.Find("Player_umitora2").GetComponent<PlayerController2_umitora>();
        myRocketRepair = GameObject.Find("ItemManager").GetComponent<RocketRepair>();
        myPlayerItemManager = GameObject.Find("ItemManager").GetComponent<PlayerItemManager>();

        RocketRepairImage = GameObject.Find("RocketRepair");

        PressButtonText = GameObject.Find("PressButtonText").GetComponent<Text>();
        PressButtonText.enabled = false;
        RocketdayoText = GameObject.Find("Rocketdayo").GetComponent<Text>();
        RocketdayoText.enabled=false;
        PlayerdayoText = GameObject.Find("Playerdayo").GetComponent<Text>();
        PlayerdayoText.enabled=false;



        GameObject[] getButtons = new GameObject[(int)OBJECT.MAX * (int)Size.MAX];
        int i = 0;
        foreach(Transform child in RocketRepairImage.transform)
        {
            //Buttons
            getButtons[i] = child.gameObject;
            i++;

        }
        for(int n=0;n< (int)OBJECT.MAX; n++)
        {
            for(int m=0;m< (int)Size.MAX; m++)
            {
                Buttons[n, m] = getButtons[n * (int)Size.MAX + m];
            }
        }


        if (FirstSelectButton == null)
        {
            FirstSelectButton = GameObject.Find("Player_S_Item");
        }
        //テキストをオフにしたりアイテムの数をセットしたりする
        foreach (Transform child in RocketRepairImage.transform)
        {
            if (child.gameObject.name != FirstSelectButton.gameObject.name)
            {
                int j = 0;
                foreach (Transform grandChild in child.transform)
                {
                    grandChild.gameObject.SetActive(false);
                    if (++j == 2)
                        break;
                }
            }
        }

        ItemTextSet();    //アイテムの数をセットするやつ







        ButtonBuf = SelectButton = FirstSelectButton;


        RocketRepairImage.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {

    }

    
    


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")   //プレイヤーがロケットの範囲にいるときの処理
        {
            if (Input.GetKeyDown(KeyCode.K) && !isRepairDisplay)    //範囲内でボタン押した時
            {
                //playerflag.playerMoveFlag = false;//********************************************    //プレイヤー動かせなくする
                isRepairDisplay = true;                 //ロケット修理の画面表示するフラグ
                RocketdayoText.enabled = true;           //ロケットだよのテキスト表示
                PlayerdayoText.enabled = true;          //プレイヤーだよのテキスト表示
                RocketRepairImage.SetActive(true);      //ロケット修理の画面表示
                //StartCoroutine(cDisplayRocketRepair);   //ロケット修理の画面表示するコルーチン
                StartCoroutine(DisplayRocketRepair());
            }

            if (Input.GetKeyDown(KeyCode.L) && isRepairDisplay)     //
            {
                //playerflag.playerMoveFlag = true;//*********************************************
                isRepairDisplay = false;
                RocketdayoText.enabled = false;
                PlayerdayoText.enabled = false;

                RocketRepairImage.SetActive(false);
                //StopCoroutine(cDisplayRocketRepair);
            }



            //ロケットの範囲内でボタン押してねのテキスト表示するやつ
            if (isRepairDisplay)
            {
                PressButtonText.enabled = false;
            }
            else
            {
                PressButtonText.enabled = true;
            }
        }

    }

    //ロケットの範囲からプレイヤーが出た時
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")   //プレイヤーがロケットの範囲にいるときの処理
            PressButtonText.enabled = false;
    }


    //ロケットの修理とかのUIをコルーチンで表示
    IEnumerator DisplayRocketRepair()
    {
        ItemTextSet();

        while (true)
        {
            //上下左右押した時の処理
            int horizontal;
            int vertical;
            if (Input.GetButtonDown("ButtonRight") && Input.GetButtonDown("ButtonLeft"))
                horizontal = 0;
            else if (Input.GetButtonDown("ButtonRight"))
                horizontal = 1;
            else if (Input.GetButtonDown("ButtonLeft"))
            {
                Debug.Log("left");

                horizontal = -1;
            }
            else
                horizontal = 0;

            if (Input.GetButtonDown("ButtonUp") && Input.GetButtonDown("ButtonDown"))
               vertical = 0;
            else if (Input.GetButtonDown("ButtonUp"))
                vertical = -1;
            else if (Input.GetButtonDown("ButtonDown"))
                vertical = 1;
            else
                vertical = 0;
       
            //どのボタンを選択するかの処理
            selectObject = Mathf.Clamp((int)selectObject + horizontal, 0, (int)OBJECT.MAX - 1);
            selectSize = Mathf.Clamp((int)selectSize + vertical, 0, (int)Size.MAX - 1);

            SelectButton = Buttons[selectObject, selectSize];

            if (ButtonBuf != SelectButton)
            {
                int j = 0;
                foreach (Transform child in SelectButton.transform)       //今選択中のボタンのエフェクト表示
                {
                    //ButtonEffect[j] = child.gameObject;
                    //ButtonEffect[j].SetActive(true);
                    child.gameObject.SetActive(true);
                    if (++j == 2)
                        break;
                }


                j = 0;

                foreach (Transform child in ButtonBuf.transform)        //前のフレームで選択していたエフェクトを非表示に
                {
                    //ButtonEffect[j] = child.gameObject;
                    //ButtonEffect[j].SetActive(false);
                    child.gameObject.SetActive(false);

                    if (++j == 2)
                        break;
                }


            }




            ButtonBuf = SelectButton;

            Player.GetComponent<Test_PlayerContllor>().CheckFlg = true;


            //if(Input.GetButtonDown(""))

            yield return null;//***************************************
            if (Input.GetKeyDown(KeyCode.L))
            {
                Player.GetComponent<Test_PlayerContllor>().CheckFlg = false;
                yield break;        //コルーチン終了
            }


            //ボタンを決定したときのやつ

            else if (Input.GetKeyDown(KeyCode.K))
            {
                switch (SelectButton.name)
                {
                    case "Player_S_Item":
                        if (myPlayerItemManager.PopItem("S_Item"))
                        {
                            if (!myRocketRepair.PushItem("S_Item"))
                            {
                                myPlayerItemManager.PushItem("S_Item");
                            }

                        }

                        break;

                    case "Player_M_Item":
                        if (myPlayerItemManager.PopItem("M_Item"))
                        {
                            if (!myRocketRepair.PushItem("M_Item"))
                            {
                                myPlayerItemManager.PushItem("M_Item");
                            }

                        }

                        break;

                    case "Player_L_Item":
                        if (myPlayerItemManager.PopItem("L_Item"))
                        {
                            if (!myRocketRepair.PushItem("L_Item"))
                            {
                                myPlayerItemManager.PushItem("L_Item");
                            }

                        }

                        break;

                    case "Player_XL_Item":
                        if (myPlayerItemManager.PopItem("XL_Item"))
                        {
                            if (!myRocketRepair.PushItem("XL_Item"))
                            {
                                myPlayerItemManager.PushItem("XL_Item");
                            }

                        }

                        break;




                    case "Rocket_S_Item":
                        if (myRocketRepair.PopItem("S_Item"))
                        {
                            if (!myPlayerItemManager.PushItem("S_Item"))
                            {
                                myRocketRepair.PushItem("S_Item");
                            }

                        }
                        break;

                    case "Rocket_M_Item":
                        if (myRocketRepair.PopItem("M_Item"))
                        {
                            if (!myPlayerItemManager.PushItem("M_Item"))
                            {
                                myRocketRepair.PushItem("M_Item");
                            }

                        }

                        break;

                    case "Rocket_L_Item":
                        if (myRocketRepair.PopItem("L_Item"))
                        {
                            if (!myPlayerItemManager.PushItem("L_Item"))
                            {
                                myRocketRepair.PushItem("L_Item");
                            }

                        }

                        break;

                    case "Rocket_XL_Item":
                        if (myRocketRepair.PopItem("XL_Item"))
                        {
                            if (!myPlayerItemManager.PushItem("XL_Item"))
                            {
                                myRocketRepair.PushItem("XL_Item");
                            }

                        }

                        break;



                }
                ItemTextSet();      //アイテムの数をセットするやつ

            }






        }




    }

    //アイテムの数をセットするやつ
    private void ItemTextSet()
    {

        int[] num;
        myRocketRepair.GetItemNum(out num);
        int i = 0;

        foreach (Transform child in RocketRepairImage.transform)
        {
            int j = 0;
            foreach (Transform grandChild in child.transform)
            {
                if (++j == 3)
                {
                    grandChild.gameObject.GetComponent<Text>().text = "×" + num[i % 4].ToString();      //ロケットとプレイヤーのアイテムの数を表示
                    if (++i == 4)
                    {
                        myPlayerItemManager.GetItemNum(out num);
                    }
                    break;
                }
            }
        }




    }



}


//int[] itemNum;
//myRocketRepair.GetItemNum(out itemNum);
//        //int[] playerItemNum;
//        //myPlayerItemManager.GetItemNum(out playerItemNum);
//        i = 0;
//        foreach(GameObject button in Buttons)
//        {
//            int childNum = 0;
//            foreach(Transform child in button.transform)
//            {
//                switch (childNum)
//                {
//                    case 0:
//                    case 1:
//                        //ButtonEffect[childNum] = child.gameObject;
//                        child.gameObject.SetActive(false);
//                        break;

//                    case 2:
//                        child.GetComponent<Text>().text= "×" + itemNum[i % 4].ToString();  //ロケットとプレイヤーのアイテムの数を表示
//                        break;
//                    //break;

//                    case 3:
//                        childNum = 0;
//                        continue;


//                }
//                childNum++;
//            }
//            if(++i==4)
//                myPlayerItemManager.GetItemNum(out itemNum);



//        }

//        if (FirstSelectButton == null)
//        {
//            FirstSelectButton = GameObject.Find("Player_S_Item");
//        }

//        foreach (Transform child in RocketRepairImage.transform)
//        {
//            if (child.gameObject.name == FirstSelectButton.gameObject.name)
//            {
//                int j = 0;
//                foreach (Transform grandChild in child.transform)
//                {
//                    grandChild.gameObject.SetActive(true);
//                    if (++j == 2)
//                        break;
//                }
//            }
//        }







//selectObject =Mathf.Clamp( (int)selectObject + (int)Input.GetAxisRaw("Vertical"),0,(int)OBJECT.MAX-1);
//selectSize = Mathf.Clamp((int)selectSize + (int)Input.GetAxisRaw("Horizontal"), 0, (int)Size.MAX - 1);

//SelectButton = Buttons[selectObject, selectSize];

//if (ButtonBuf != SelectButton)
//{
//    int j = 0;
//    foreach (Transform child in SelectButton.transform)       //今選択中のボタンのエフェクト表示
//    {
//        ButtonEffect[j] = child.gameObject;
//        ButtonEffect[j].SetActive(true);
//        if (++j == 2)
//            break;
//    }


//    j = 0;

//    foreach (Transform child in ButtonBuf.transform)        //前のフレームで選択していたエフェクトを非表示に
//    {
//        ButtonEffect[j] = child.gameObject;
//        ButtonEffect[j].SetActive(false);
//        if (++j == 2)
//            break;
//    }


//}




//ButtonBuf = SelectButton;



//GameObject pushButton = EventSystem.current.currentSelectedGameObject;
// Debug.Log(pushButton);
// if(pushButton==null)
//     pushButton=GameObject.Find("Player_S_Item");
// if (pushButton != null)
// {
//     SelectButton = pushButton;
//     if (SelectButton != ButtonBuf)
//     {
//         int j = 0;
//         foreach (Transform child in SelectButton.transform)       //今選択中のボタンのエフェクト表示
//         {
//             ButtonEffect[j] = child.gameObject;
//             ButtonEffect[j].SetActive(true);
//             if (++j == 2)
//                 break;
//         }

//         j = 0;

//         foreach (Transform child in ButtonBuf.transform)        //前のフレームで選択していたエフェクトを非表示に
//         {
//             ButtonEffect[j] = child.gameObject;
//             ButtonEffect[j].SetActive(false);
//             if (++j == 2)
//                 break;
//         }

//     }
// }
// ButtonBuf = SelectButton; 



//int[] num;
//myRocketRepair.GetItemNum(out num);

// i = 0;
//foreach (Transform child in RocketRepairImage.transform)
//{
//    int j = 0;
//    foreach (Transform grandChild in child.transform)
//    {
//        if (j < 2)
//        {
//            grandChild.gameObject.SetActive(false);
//        }
//        if (++j == 3)
//        {
//            grandChild.gameObject.GetComponent<Text>().text = "×" + num[i % 4].ToString();  //ロケットとプレイヤーのアイテムの数を表示
//            if (++i == 4)
//            {
//                myPlayerItemManager.GetItemNum(out num);
//            }
//            break;
//        }
//    }
//}



//if (FirstSelectButton == null)
//{
//    FirstSelectButton = GameObject.Find("Player_S_Item");
//}

//foreach (Transform child in RocketRepairImage.transform)
//{
//    if (child.gameObject.name == FirstSelectButton.gameObject.name)
//    {
//        int j = 0;
//        foreach (Transform grandChild in child.transform)
//        {
//            grandChild.gameObject.SetActive(true);
//            if (++j == 2)
//                break;
//        }
//    }
//}

//EventSystem.current.SetSelectedGameObject(FirstSelectButton);
