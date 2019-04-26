using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obscure : MonoBehaviour {

    Renderer[] renderers;
	
	void Start () {
        renderers = GetComponentsInChildren<Renderer>();

        foreach (Renderer r in renderers)
        {
            r.material.renderQueue = 2002;
        }
		
	}
	
	
}
