using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.EventSystems;


public class RocketRepairUI : MonoBehaviour
{

    RocketRepair myRocketRepair;
    PlayerItemManager myPlayerItemManager;


    GameObject RocketRepairImage;

    [SerializeField] GameObject FirstSelectButton;  //最初に選択されているボタン
    GameObject Button;      //今選択しているボタン
    GameObject ButtonBuf;   //前のフレームで選択したボタン

    GameObject[] ButtonEffect = new GameObject[2];  //ボタンを選択しているエフェクトみたいなやつ

    IEnumerator cDisplayRocketRepair;       //ロケットを修理するUIを表示するやつ
    bool isRepairDisplay = false;


    Text PressButtonText;
    Text RocketdayoText;
    Text PlayerdayoText;



    PlayerController2_umitora playerflag;//*****************************************************


    // Use this for initialization
    void Start()
    {
        playerflag = GameObject.Find("Player_umitora2").GetComponent<PlayerController2_umitora>();
        myRocketRepair = GameObject.Find("ItemManager").GetComponent<RocketRepair>();
        myPlayerItemManager = GameObject.Find("ItemManager").GetComponent<PlayerItemManager>();

        RocketRepairImage = GameObject.Find("RocketRepair");

        PressButtonText = GameObject.Find("PressButtonText").GetComponent<Text>();
        PressButtonText.enabled = false;
        RocketdayoText = GameObject.Find("Rocketdayo").GetComponent<Text>();
        RocketdayoText.enabled=false;
        PlayerdayoText = GameObject.Find("Playerdayo").GetComponent<Text>();
        PlayerdayoText.enabled=false;


        int[] num;
        myRocketRepair.GetItemNum(out num);
    
        int i = 0;
        foreach (Transform child in RocketRepairImage.transform)
        {
            int j = 0;
            foreach (Transform grandChild in child.transform)
            {
                if (j < 2)
                {
                    grandChild.gameObject.SetActive(false);
                }
                if (++j == 3)
                {
                    grandChild.gameObject.GetComponent<Text>().text = "×" + num[i % 4].ToString();  //ロケットとプレイヤーのアイテムの数を表示
                    if (++i == 4)
                    {
                        myPlayerItemManager.GetItemNum(out num);
                    }
                    break;
                }
            }
        }



        if (FirstSelectButton == null)
        {
            FirstSelectButton = GameObject.Find("Player_S_Item");
        }

        foreach (Transform child in RocketRepairImage.transform)
        {
            if (child.gameObject.name == FirstSelectButton.gameObject.name)
            {
                int j = 0;
                foreach (Transform grandChild in child.transform)
                {
                    grandChild.gameObject.SetActive(true);
                    if (++j == 2)
                        break;
                }
            }
        }

        EventSystem.current.SetSelectedGameObject(FirstSelectButton);
        ButtonBuf = Button = FirstSelectButton;


        cDisplayRocketRepair = DisplayRocketRepair();       //コルーチン
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
                playerflag.playerMoveFlag = false;//********************************************    //プレイヤー動かせなくする
                isRepairDisplay = true;                 //ロケット修理の画面表示するフラグ
                RocketdayoText.enabled= true;           //ロケットだよのテキスト表示
                PlayerdayoText.enabled = true;          //プレイヤーだよのテキスト表示
                RocketRepairImage.SetActive(true);      //ロケット修理の画面表示
                //StartCoroutine(cDisplayRocketRepair);   //ロケット修理の画面表示するコルーチン
                StartCoroutine(DisplayRocketRepair());
            }

            if (Input.GetKeyDown(KeyCode.L) && isRepairDisplay)     //
            {
                playerflag.playerMoveFlag = true;//*********************************************
                isRepairDisplay = false;
                RocketdayoText.enabled = false;
                PlayerdayoText.enabled = false;

                RocketRepairImage.SetActive(false);
                //StopCoroutine(cDisplayRocketRepair);
            }
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


    private void OnTriggerExit(Collider other)
    {
        PressButtonText.enabled = false;
    }


    IEnumerator DisplayRocketRepair()
    {
        while (true)
        {
           GameObject pushButton = EventSystem.current.currentSelectedGameObject;
            Debug.Log(pushButton);
            if(pushButton==null)
                pushButton=GameObject.Find("Player_S_Item");
            if (pushButton != null)
            {
                Button = pushButton;
                if (Button != ButtonBuf)
                {
                    int j = 0;
                    foreach (Transform child in Button.transform)       //今選択中のボタンのエフェクト表示
                    {
                        ButtonEffect[j] = child.gameObject;
                        ButtonEffect[j].SetActive(true);
                        if (++j == 2)
                            break;
                    }

                    j = 0;

                    foreach (Transform child in ButtonBuf.transform)        //前のフレームで選択していたエフェクトを非表示に
                    {
                        ButtonEffect[j] = child.gameObject;
                        ButtonEffect[j].SetActive(false);
                        if (++j == 2)
                            break;
                    }

                }
            }
            ButtonBuf = Button; 


            //if(Input.GetButtonDown(""))

            yield return null;//***************************************
            if (Input.GetKeyDown(KeyCode.L))
            {
                yield break;
            }


            //ボタンを決定したときのやつ

            else if (Input.GetKeyDown(KeyCode.K))
            {
                switch (Button.name)
                {
                    case "Player_S_Item":
                        if (myPlayerItemManager.PopItem("S_Item"))
                        {
                            myRocketRepair.PushItem("S_Item");

                        }

                        break;

                    case "Player_M_Item":
                        if (myPlayerItemManager.PopItem("M_Item"))
                        {
                            myRocketRepair.PushItem("M_Item");

                        }

                        break;

                    case "Player_L_Item":
                        if (myPlayerItemManager.PopItem("L_Item"))
                        {
                            myRocketRepair.PushItem("L_Item");

                        }

                        break;

                    case "Player_XL_Item":
                        if (myPlayerItemManager.PopItem("XL_Item"))
                        {
                            myRocketRepair.PushItem("XL_Item");

                        }

                        break;




                    case "Rocket_S_Item":
                        if (myRocketRepair.PopItem("S_Item"))
                        {
                            myPlayerItemManager.PushItem("S_Item");

                        }
                        break;

                    case "Rocket_M_Item":
                        if (myRocketRepair.PopItem("M_Item"))
                        {
                            myPlayerItemManager.PushItem("M_Item");

                        }

                        break;

                    case "Rocket_L_Item":
                        if (myRocketRepair.PopItem("L_Item"))
                        {
                            myPlayerItemManager.PushItem("L_Item");

                        }

                        break;

                    case "Rocket_XL_Item":
                        if (myRocketRepair.PopItem("XL_Item"))
                        {
                            myPlayerItemManager.PushItem("XL_Item");

                        }

                        break;



                }

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
    }

}