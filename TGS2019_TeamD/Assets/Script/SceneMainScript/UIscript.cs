using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIscript : MonoBehaviour {

    //　制限時間（分）
    [SerializeField]
    private int minute;
    //　制限時間（秒）
    [SerializeField]
    private float seconds;

    [HideInInspector] public int s, m, l, xl = 0;

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

	void Start () {
        totalTime = minute * 60 + seconds;
        oldSeconds = 0f;
		Buff_slider = GameObject.Find("Buff_Gauge").GetComponent<Slider>();
        Weight_slider = GameObject.Find("Weight_Gauge").GetComponent<Slider>();

	}
	
	void Update () {

        //　制限時間が0秒以下ならなにもしない
        if(totalTime <= 0f) {
            return;
        }
        //　トータルの制限時間を計測
        totalTime = minute * 60 + seconds;
        totalTime -= Time.deltaTime;

        //　再設定
        minute = (int)totalTime / 60;
        seconds = totalTime - minute * 60;

        //　時間を表示
        if((int)seconds != (int)oldSeconds) {
            timerText.text = minute.ToString("00") + ":" + ((int)seconds).ToString("00");
        }
        oldSeconds = seconds;

        //　制限時間以下になったら～
        if(totalTime <= 0f) {
            Debug.Log("制限時間終了");
        }
		

        XL_text.text = xl + "/2";
        L_text.text = l + "/5";
        M_text.text = m + "/10";
        S_text.text = s + "/20";
        
        //buffvalue += 0.001f;
        //Buff_slider.value += buffvalue;
        //Weight_slider.value += weightvalue;
	}
}
