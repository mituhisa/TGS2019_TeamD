using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectButton : MonoBehaviour {

    /*
     * 現在選択中のボタン
     */
    private GameObject Button;
    /*
     * 現在の一つ前に選択していたボタン
     */
    private GameObject ButtonBuf;
    /*
     * ボタンに付加するエフェクト
     */
    private GameObject[] ButtonEffect = new GameObject[4];
    private int i = 0;

    private Color White = new Color(255, 255, 255, 255);
    private Color Red = new Color(255, 0, 0, 255);

    void Update() {
        /*
         * 現在の選択されているボタンを取得し、Buttonに代入
         */
        Button = EventSystem.current.currentSelectedGameObject;
        /*
         *-----------------------------------------------------------------------------------
         * updateの1回目だけ行う処理(Startの時に選択されているボタンが取得できなかったため)
         * ButtonBufに現在の選択されているボタンを代入(初期化)
         *-----------------------------------------------------------------------------------
         */
        if (i == 0) {
            ButtonBuf = Button;
            i++;
        }
        /*
         *-----------------------------------------------------------------------------------------
         * 現在の選択されているボタンが前に選択されているボタンと違う場合(選択肢を移動させた場合)
         * 現在のボタンのエフェクトを表示させる
         * 一つ前に選択していたボタンのエフェクトを非表示にさせる
         *-----------------------------------------------------------------------------------------
         */
        if (Button != ButtonBuf) {
            int j = 0;
            /*
             * 選択中のボタンの子要素を取得(1番目の要素は文字なので飛ばす)
             */
            foreach (Transform child in Button.transform) {
                ButtonEffect[j] = child.gameObject;
                if (j == 0) ButtonEffect[j].GetComponent<Text>().color = Red;
                if (j > 0) ButtonEffect[j].SetActive(true);
                j++;
            }
            j=0;
            /*
             * 一つ前にに選択していたボタンの子要素を取得(1番目の要素は文字なので飛ばす)
             */
            foreach (Transform child in ButtonBuf.transform) {
                ButtonEffect[j] = child.gameObject;
                if (j == 0) ButtonEffect[j].GetComponent<Text>().color = White;
                if (j > 0) ButtonEffect[j].SetActive(false);
                j++;
            }


        }
        /*
         * 現在選択されているボタンをButtonBufに代入(現在のボタンを一つ前に選択していたボタンに設定する)
         */
        ButtonBuf = Button;
    }
}

