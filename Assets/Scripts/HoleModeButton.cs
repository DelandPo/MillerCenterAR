using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoleModeButton : MonoBehaviour {

    public HoleCutterBehavior holeCutter;
    public Text buttonText;

    public void HoleModeToggle()
    {
        holeCutter.follow = !holeCutter.follow;
        if (holeCutter.follow)
        {
            buttonText.text = "Hole Mode\n Follow";
        }
        else
        {
            buttonText.text = "Hole Mode\n Stay";
        }
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
