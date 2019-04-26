using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleCutterBehavior : MonoBehaviour {

    public Camera cam;
    public bool follow = false;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {

        if (follow)
        {
            transform.position = new Vector3(cam.transform.position.x, 0, cam.transform.position.z);
        }
    }
}
