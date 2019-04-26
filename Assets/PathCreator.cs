using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class PathCreator : MonoBehaviour {

    ArrayList corners;
    internal bool drawCorners = false;
   public LineRenderer lineRenderer;
   // public Text outputText;
    NavMeshAgent agent;
    //internal Vector3 destination;

    // Use this for initialization
    void Start()
    {
        corners = new ArrayList();
//lineRenderer = GetComponent<LineRenderer>();
        agent = GetComponent<NavMeshAgent>();
    }

    internal void SetDest(Vector3 pos)
    {
        drawCorners = true;
        agent.SetDestination(pos);
    }
    internal void ClearDest(Vector3 pos)
    {
        drawCorners = false;
        agent.transform.position = pos;
        corners = new ArrayList();
    }
	
	// Update is called once per frame
	void Update () {
        if (drawCorners)
        {
           // outputText.text = "PathPoints: " +corners.Count;
            if (agent.remainingDistance > 2.0f)
            {
             
                corners.Add(gameObject.transform.position);
            }
            int i = 0;
            lineRenderer.positionCount = corners.Count;
            foreach (Vector3 p in corners)
            {
                lineRenderer.SetPosition(i, p);
                i++;
            }
        } else
        {
            lineRenderer.positionCount = 0;
           
        }
        }
		
	}

