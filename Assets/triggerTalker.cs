using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerTalker : MonoBehaviour {

    public TourGuide tourGuide;

    private void OnTriggerEnter(Collider other)
    {
        tourGuide.Talk();
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
