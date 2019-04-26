using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore.GuidedAR;
using UnityEngine.UI;
using UnityEngine.AI;

public class TourGuide : MonoBehaviour {

    public Transform startPoint;
    public GuidedARController ARController;
    GuidedARController.PlaceState placeState = GuidedARController.PlaceState.PLACE_ANCHOR;
    GuidedARController.PlaceState previousPlaceState = GuidedARController.PlaceState.PLACE_ANCHOR;
    public Camera playerCamera;
    float agility = 20.0f;
    public Text captionText;
    public Text reportText;
    AudioSource audioSource;
    public AudioClip[] dialogues;
    public AudioClip[] generalPhrases;

    public string[] infoCaptions;
    public string[] generalCaptions;
    int audioIndex = 0;
    public wayfinder pointer;

    public PathCreator pathCreator;
    public enum TourGuideStates { IDLE,LEADING,FOLLOWING,SPEAKING};
    public TourGuideStates guideState = TourGuideStates.IDLE;

    Animator animator;
  //  float dist = 0.0f;
    public int infoMarkerIndex = -1;
    public Transform nearPlayer;
  //  public PathCreator pathCreator;
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

            ClearDest();
        }
        previousPlaceState = ARController.placeState;
       
        if (ARController.placeState == GuidedARController.PlaceState.SHOW)
        {
            ReportState();
            CheckState();
          
        }

	}

    IEnumerator ResetToIdle()
    {
        while (audioSource.isPlaying)
        {
            yield return null;
        }

        TurnTowardCamera();
        animator.SetBool("talking", false);
        captionText.text = "";
        ClearDest();

    }

   public void Talk()
    {
        if (guideState == TourGuideStates.LEADING)
        {
            return;
        }
        TurnTowardCamera();

        int index = -1;
        Vector3 closest = infoMarkers[0].transform.position;

        foreach (InfoMarker i in infoMarkers)
        {
            float d = Vector3.Distance(playerCamera.transform.position, i.transform.position);
            if (d <= 5)
            {
                if (d < Vector3.Distance(playerCamera.transform.position, closest))
                {
                    closest = i.transform.position;
                    index = i.infoIndex;
                }
            }
        }


        if (!audioSource.isPlaying)
        {
            animator.SetBool("talking", true);
           
            if (index != -1)
            {
                audioSource.clip = dialogues[index];
                captionText.text = infoCaptions[index];
            } 
            else
            {
                audioSource.clip = generalPhrases[audioIndex];
                captionText.text = generalCaptions[audioIndex];
                audioIndex++;
                if (audioIndex == generalPhrases.Length)
                {
                    audioIndex = 0;
                }
            }
            audioSource.Play();
            StartCoroutine(ResetToIdle());
        }
    }

 

    void CheckforInput()
    {
        if (ARController.placeState == GuidedARController.PlaceState.SHOW)
        {

            if (Input.touchCount > 0)// && Input.GetTouch(0).phase == TouchPhase.Began)
            {

                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit hit;

                //Select Hool
                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    if (hit.collider.gameObject.name == "TourGuide")
                    {

                        reportText.text = "Hit";
                        if (!audioSource.isPlaying)
                        {

                            audioSource.clip = dialogues[0];
                            audioSource.Play();
                        }


                    }
                    else
                    {
                        reportText.text = "Miss";
                    }
                }
            }
        }
    }

    public void SetDest(int value)
    {
        guideState = TourGuideStates.LEADING;

        infoMarkerIndex = value;
        navMeshAgent.SetDestination(infoMarkers[infoMarkerIndex].gameObject.transform.position);
        pathCreator.gameObject.transform.position = nearPlayer.position;
        
        pathCreator.SetDest(infoMarkers[infoMarkerIndex].gameObject.transform.position);

    }

    public void ClearDest()
    {
        guideState = TourGuideStates.FOLLOWING;
        infoMarkerIndex = -1;
        navMeshAgent.SetDestination(nearPlayer.position);
        pathCreator.ClearDest(nearPlayer.position);
    }

    void ReportState()
    {
        switch (guideState)
        {
            case TourGuideStates.FOLLOWING:
                {
                    reportText.text = "Following";
                    break;
                }
            case TourGuideStates.IDLE:
                {
                    reportText.text = "Idle";
                    break;
                }
            case TourGuideStates.LEADING:
                {
                    reportText.text = "Leading";
                    break;
                }
            case TourGuideStates.SPEAKING:
                {
                    reportText.text = "Speaking";
                    break;
                }
        }
    }
    void CheckState()
    {
        // heading toward an infopoint
        if (guideState == TourGuideStates.LEADING)
        {
            if (infoMarkerIndex != -1)
            {
                float stoppingDistance = 3.5f;               
                float dist = Vector3.Distance(infoMarkers[infoMarkerIndex].gameObject.transform.position, transform.position);

                float distFromPlayer = Vector3.Distance(nearPlayer.position, transform.position);
                if (dist >= stoppingDistance)
                {
                    animator.SetBool("walking", true);
                    navMeshAgent.isStopped = false;
                    if (distFromPlayer > 10) // keep the bot from moving too far away
                    {
                        navMeshAgent.isStopped = true;
                        animator.SetBool("walking", false);
                    }

                }

                else

                {
                    guideState = TourGuideStates.IDLE;
                    pointer.index = -1; // turns the pointer off

                    navMeshAgent.isStopped = true;
                    animator.SetBool("walking", false);
                    TurnTowardCamera();
                }
            } // if destination index is set
        }// if leading
        // heading toward the camera
        else if (guideState == TourGuideStates.FOLLOWING)
        {

           float dist = Vector3.Distance(nearPlayer.position, transform.position);

            if (dist >= 2.5f)
            {
                // TurnTowardCamera();
                animator.SetBool("walking", true);
                navMeshAgent.isStopped = false;

            }
            else
            {
                guideState = TourGuideStates.IDLE;
                navMeshAgent.isStopped = true;
                animator.SetBool("walking", false);
            }
        }
        else if (guideState == TourGuideStates.IDLE)
        {
            float dist = Vector3.Distance(nearPlayer.position, transform.position);
            if (dist < 5)
            {
                navMeshAgent.isStopped = true;
                TurnTowardCamera();
            } else
            {
                guideState = TourGuideStates.FOLLOWING;
            }

        }
    }



    void TurnTowardTarget()
    {
        Transform destination = infoMarkers[infoMarkerIndex].gameObject.transform;
        Vector3 target = new Vector3(destination.position.x, transform.position.y, destination.position.z);
        transform.LookAt(target, transform.up);
    }

    void TurnTowardCamera()
    {

        
            Vector3 cameraPositionSameY = playerCamera.transform.position;
            cameraPositionSameY.y = transform.position.y;

     
            transform.LookAt(cameraPositionSameY, transform.up);

        

     
    }
}
