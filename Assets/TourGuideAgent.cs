using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore.GuidedAR;
using UnityEngine.UI;
using UnityEngine.AI;

public class TourGuideAgent : MonoBehaviour {

    public Transform startPoint;
    public GuidedARController ARController;
    GuidedARController.PlaceState placeState = GuidedARController.PlaceState.PLACE_ANCHOR;
    GuidedARController.PlaceState previousPlaceState = GuidedARController.PlaceState.PLACE_ANCHOR;
    public Camera playerCamera;

    public Text outputText;
    AudioSource audioSource;
    public AudioClip[] dialogues;
    public AudioClip[] generalPhrases;

    public string[] infoCaptions;
    public string[] generalCaptions;
    int audioIndex = 0;
    public wayfinder pointer;

    public enum TourGuideStates { IDLE, ROAMING, FOLLOWING, SPEAKING };
    public TourGuideStates guideState = TourGuideStates.IDLE;

    Animator animator;

    public int infoMarkerIndex = -1;
    public Transform agentDestination;

    NavMeshAgent navMeshAgent;

    public InfoMarker[] infoMarkers;


    // Use this for initialization
    void Start () {

        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        navMeshAgent = GetComponent<NavMeshAgent>();

    }
	
	// Update is called once per frame
	void Update () {

        if ((previousPlaceState != ARController.placeState) && (ARController.placeState == GuidedARController.PlaceState.SHOW))
        {

            gameObject.transform.position = startPoint.position;
            gameObject.transform.rotation = startPoint.rotation;

            guideState = TourGuideStates.FOLLOWING;
            navMeshAgent.SetDestination(new Vector3(agentDestination.position.x, transform.position.y, agentDestination.position.z));
           
        }

    }
}
