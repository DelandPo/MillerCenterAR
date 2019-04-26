using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wayfinder : MonoBehaviour {

    public GameObject[] informarkers;
    public int index = -1;
    public MeshRenderer arrow;

	// Use this for initialization
	void Start () {
		
	}
	
    public void setIndex(int value)
    {

        index = value;
    }


	// Update is called once per frame
	void Update () {

        if (index == -1)
        {
            arrow.enabled = false;
        }
        else
        {
            arrow.enabled = true;
            Vector3 infoPositionSameY = informarkers[index].transform.position;
            infoPositionSameY.y = transform.position.y;
            transform.LookAt(infoPositionSameY, Vector3.up);
        }
		
	}
}
