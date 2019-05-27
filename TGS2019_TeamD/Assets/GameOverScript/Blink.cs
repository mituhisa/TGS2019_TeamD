using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blink : MonoBehaviour {
   [SerializeField] float alpha = 1f;
    float Speed = 0.015f;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.GetComponent<CanvasRenderer>().SetAlpha(alpha);
        alpha = alpha - Speed;

        if(alpha <= 0 || alpha >= 1)
        {
            Speed = Speed * -1;
        }
    
    }
}
