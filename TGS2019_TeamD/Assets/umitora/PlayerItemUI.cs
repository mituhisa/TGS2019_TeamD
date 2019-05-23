using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class PlayerItemUI : MonoBehaviour
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


    int selectSize = (int)Size.S;         //ボタンを選択する時にどのサイズを選択しているか見るやつ

    GameObject[] Buttons = new GameObject[(int)Size.MAX];    //選択できるボタン

    PlayerItemManager myPlayerItemManager;  //プレイヤーのアイテムのスクリプト

    GameObject PlayerItemImage;

    [SerializeField] GameObject FirstSelectButton;  //最初に選択されているボタン
    GameObject SelectButton;      //今選択しているボタン
    GameObject ButtonBuf;   //前のフレームで選択したボタン


    [SerializeField] GameObject[] ThrowObject = new GameObject[(int)Size.MAX];

    Transform myPlayer;

    bool isDisplay=false;
    bool canDisplay = true;
    // Use this for initialization

    void Start()
    {

        PlayerItemImage = GameObject.Find("PlayerItem");

        myPlayerItemManager = GameObject.Find("ItemManager").GetComponent<PlayerItemManager>();

        GameObject myCanvas = GameObject.Find("Canvas ");
        myPlayer = GameObject.Find("Cube_005").GetComponent<Transform>();//*********************************************************

        foreach (Transform child in myCanvas.transform)
        {
            if (child.gameObject.name == "PlayerItem")
            {
                int i = 0;

                foreach (Transform grandChild in child)
                {
                    Buttons[i] = grandChild.gameObject;
                    i++;
                }
                break;

            }


        }




        if (FirstSelectButton == null)
        {
            FirstSelectButton =PlayerItemImage.transform.Find("Player_S_Item2").gameObject;
        }
        //Debug.Log(FirstSelectButton.gameObject.name);
        //テキストをオフにしたりアイテムの数をセットしたりする
        foreach (Transform child in PlayerItemImage.transform)
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

        ButtonBuf = SelectButton = FirstSelectButton;


        ItemTextSet();    //アイテムの数をセットするやつ



            ThrowObject[(int)Size.S] =(GameObject) Resources.Load("Prefab/S_Item");
            ThrowObject[(int)Size.M] = (GameObject)Resources.Load("Prefab/M_Item");
            ThrowObject[(int)Size.L] = (GameObject)Resources.Load("Prefab/L_Item");
            ThrowObject[(int)Size.XL] = (GameObject)Resources.Load("Prefab/XL_Item");



        PlayerItemImage.SetActive(false);       
        //PlayerItemImage.SetActive(true);


    }

    // Update is called once per frame
    void Update()
    {
        
        if (canDisplay&&  !isDisplay)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                GetComponent<Test_PlayerContllor>().CheckFlg = true;
                isDisplay = true;
                StartCoroutine(DisplayPlayerItemImage());
            }
        }

        //Debug.Log("update");

        //Debug.Log("can" + canDisplay);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Rocket")
        {
            canDisplay = false;

        }
        Debug.Log("tag"+other.gameObject.tag);
        Debug.Log("name" + other.gameObject.name);
        Debug.Log("Enter");

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Rocket")
        {
            canDisplay = false;

        }

    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Rocket")
        {
            canDisplay = true;

        }
        Debug.Log("tag" + other.gameObject.tag);
        Debug.Log("name" + other.gameObject.name);
        Debug.Log("Exit");

    }


    //アイテムの数をセットするやつ
    private void ItemTextSet()
    {

        int[] num;
        myPlayerItemManager.GetItemNum(out num);
        int i = 0;

        foreach (Transform child in PlayerItemImage.transform)
        {
            int j = 0;
            foreach (Transform grandChild in child.transform)
            {
                if (j == 2)
                {
                    grandChild.gameObject.GetComponent<Text>().text = "×" + num[i].ToString();      //ロケットとプレイヤーのアイテムの数を表示
                    i++; 
                }
                j++;
            }
        }




    }


    //ロケットの修理とかのUIをコルーチンで表示
    IEnumerator DisplayPlayerItemImage()
    {
        PlayerItemImage.SetActive(true);
        ItemTextSet();

        while (true)
        {
            //上下左右押した時の処理
            int vertical;

            if (Input.GetButtonDown("ButtonUp") && Input.GetButtonDown("ButtonDown"))
                vertical = 0;
            else if (Input.GetButtonDown("ButtonUp"))
                vertical = -1;
            else if (Input.GetButtonDown("ButtonDown"))
                vertical = 1;
            else
                vertical = 0;

            //どのボタンを選択するかの処理
            selectSize = Mathf.Clamp((int)selectSize + vertical, 0, (int)Size.MAX - 1);

            SelectButton = Buttons[selectSize];

            if (ButtonBuf != SelectButton)
            {
                int j = 0;
                foreach (Transform child in SelectButton.transform)       //今選択中のボタンのエフェクト表示
                {
                    child.gameObject.SetActive(true);
                    if (++j == 2)
                        break;
                }


                j = 0;

                foreach (Transform child in ButtonBuf.transform)        //前のフレームで選択していたエフェクトを非表示に
                {
                    child.gameObject.SetActive(false);

                    if (++j == 2)
                        break;
                }


            }




            ButtonBuf = SelectButton;




            //if(Input.GetButtonDown(""))

            yield return null;//***************************************
            if (Input.GetKeyDown(KeyCode.F))
            {
                GetComponent<Test_PlayerContllor>().CheckFlg = false;
                isDisplay = false;
                PlayerItemImage.SetActive(false);
                yield break;        //コルーチン終了
            }


            //ボタンを決定したときのやつ

            else if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("input.h");
                switch (SelectButton.name)
                {
                    
                    case "Player_S_Item2"://************************************************************名前違うかも**************************
                        if (myPlayerItemManager.PopItem("S_Item"))
                        {
                           GameObject instanceObject= Instantiate(ThrowObject[(int)Size.S], myPlayer.position + myPlayer.up*2, Quaternion.identity) ;
                            instanceObject.GetComponent<Rigidbody>().AddForce(myPlayer.up * 20,ForceMode.Impulse);
                        }

                        break;

                    case "Player_M_Item2":
                        if (myPlayerItemManager.PopItem("M_Item"))
                        {
                            GameObject instanceObject = Instantiate(ThrowObject[(int)Size.M], myPlayer.position + myPlayer.up * 2, Quaternion.identity);
                            instanceObject.GetComponent<Rigidbody>().AddForce(myPlayer.up * 20, ForceMode.Impulse);

                        }

                        break;

                    case "Player_L_Item2":
                        if (myPlayerItemManager.PopItem("L_Item"))
                        {
                            GameObject instanceObject = Instantiate(ThrowObject[(int)Size.L], myPlayer.position + myPlayer.up * 2, Quaternion.identity);
                            instanceObject.GetComponent<Rigidbody>().AddForce(myPlayer.up * 20, ForceMode.Impulse);

                        }

                        break;

                    case "Player_XL_Item2":
                        if (myPlayerItemManager.PopItem("XL_Item"))
                        {
                            GameObject instanceObject = Instantiate(ThrowObject[(int)Size.XL], myPlayer.position + myPlayer.up * 2, Quaternion.identity);
                            instanceObject.GetComponent<Rigidbody>().AddForce(myPlayer.up * 20, ForceMode.Impulse);

                        }

                        break;






                }
                ItemTextSet();      //アイテムの数をセットするやつ

            }






        }

    }


}
