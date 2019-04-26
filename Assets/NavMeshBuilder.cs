using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshBuilder : MonoBehaviour {

    

     public NavMeshSurface[] surfaces;

    public void BuildNavMeshes()
    {

        for (int i = 0; i < surfaces.Length; i++)
        {
            surfaces[i].BuildNavMesh();
        }

    }

// Use this for initialization
void Start () {
      //  BuildNavMeshes();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
