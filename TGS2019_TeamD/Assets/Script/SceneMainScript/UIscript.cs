using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIscript : MonoBehaviour
{

    //　制限時間（分）
    [SerializeField]
    private int minute;
    //　制限時間（秒）
    [SerializeField]
    private float seconds;

    private float totalTime;
    private float oldSeconds;

    public Text XL_text;
    public Text L_text;
    public Text M_text;
    public Text S_text;
    public Text timerText;

    Slider Buff_slider;
    Slider Weight_slider;

    float buffvalue = 0f;
    float weightvalue = 0.25f;



    PlayerItemManager myPlayerItemManager;      //*************************************************************
    RocketRepair myRocketRepair;




    void Start()
    {
        totalTime = minute * 60 + seconds;
        oldSeconds = 0f;
        Buff_slider = GameObject.Find("Buff_Gauge").GetComponent<Slider>();
        Weight_slider = GameObject.Find("Weight_Gauge").GetComponent<Slider>();

        Buff_slider.enabled = false;
        Weight_slider.enabled = false;



        S_text = GameObject.Find("S_Count").GetComponent<Text>();
        M_text = GameObject.Find("M_Count").GetComponent<Text>();
        L_text = GameObject.Find("L_Count").GetComponent<Text>();
        XL_text = GameObject.Find("XL_Count").GetComponent<Text>();
        timerText = GameObject.Find("Timer").GetComponent<Text>();
        myPlayerItemManager =   GameObject.Find("ItemManager").GetComponent<PlayerItemManager>();
        myRocketRepair = GameObject.Find("ItemManager").GetComponent<RocketRepair>();
    }

    void Update()
    {

        //　制限時間が0秒以下ならなにもしない
        if (totalTime <= 0f)
        {
            return;
        }
        //　トータルの制限時間を計測
        totalTime = minute * 60 + seconds;
        totalTime -= Time.deltaTime;

        //　再設定
        minute = (int)totalTime / 60;
        seconds = totalTime - minute * 60;

        //　時間を表示
        if ((int)seconds != (int)oldSeconds)
        {
            timerText.text = minute.ToString("00") + ":" + ((int)seconds).ToString("00");
        }
        oldSeconds = seconds;

        //　制限時間以下になったら～
        if (totalTime <= 0f)
        {
            Debug.Log("制限時間終了");
        }


        //XL_text.text = "0/2";
        //L_text.text = "0/5";
        //M_text.text = "0/10";
        //S_text.text = "0/20";


        //buffvalue += 0.001f;
        //Buff_slider.value += buffvalue;
        //Weight_slider.value += weightvalue;


        int[] itemNum;//*************************************************
        myRocketRepair.GetItemNum(out itemNum);
        XL_text.text = itemNum[3].ToString() + "/2";
        L_text.text = itemNum[2].ToString() + "/5";
        M_text.text = itemNum[1].ToString() + "/10";
        S_text.text = itemNum[0].ToString() + "/20";

        Buff_slider.value = myPlayerItemManager.GetPowerRatio();//******************************************************************************************************
        Weight_slider.value = myPlayerItemManager.GetWeightRatio();




    }
}