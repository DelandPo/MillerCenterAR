using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class RetrieveLocation : MonoBehaviour {


  //  public bool serviceRunning = false;
    public int timeOut;
    public int averageNumber;
    public float desiredAccuracyInMeters;
    public float updateDistanceInMeters;
    internal float magneticHeading;
    internal float trueHeading;
    private List<float> averageMagneticHeading = new List<float>();
    private List<float> averageTrueHeading = new List<float>();
    public Compass currentCompass;
    public Gyroscope currentGyroscope;
    public LocationInfo currentLocation;
    public Text details;

    public Camera cam;


    private static RetrieveLocation _instance;

    public static RetrieveLocation Instance { get { return _instance; } }

    private void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private void Start()
    {
        currentCompass = new Compass
        {
            enabled = true
        };

        Input.gyro.enabled = true;
        StartLocationservice();
    }

    private void Update()
    {
       // Checking if the list is full or not
       if(averageMagneticHeading.Count == averageNumber)
        {
            magneticHeading = GetAverage(averageMagneticHeading);
            trueHeading = GetAverage(averageTrueHeading);
            averageMagneticHeading.Clear();
            averageTrueHeading.Clear();
        }

       else
        {
            averageMagneticHeading.Add(currentCompass.magneticHeading);
            averageTrueHeading.Add(currentCompass.trueHeading);
        }

        RefreshData();
    }

    private IEnumerator StartCapturingLocation()
    {
        UpdateDetails("Service Starting");

        if (!Input.location.isEnabledByUser)
        {
            UpdateDetails("Error! Location service not turned on");
            yield break;
        }
        Input.location.Start(desiredAccuracyInMeters,updateDistanceInMeters);


        //Waiting until location services initializes
        UpdateDetails("Location Service Initializing");
        while(Input.location.status != LocationServiceStatus.Initializing && timeOut > 0)
        {
            yield return new WaitForSeconds(1);
            timeOut--;
        }

        if(timeOut <= 0)
        {
            UpdateDetails("Timeout! Location Couldn't be initialized");
        }


        if(Input.location.status != LocationServiceStatus.Failed)
        {
            currentLocation = Input.location.lastData;

            UpdateDetails("Location Service Initialized Successfully");         
        }
        else
        {
            UpdateDetails("Error! Couldn't determine the location");
        }
    }


    private void UpdateDetails(string message)
    {
        details.text = message;
    }

    //Method Invoked by the User to start the location service
    public void StartLocationservice()
    {
      //  if (!serviceRunning)
            StartCoroutine(StartCapturingLocation());
      //  else
          //  RefreshData();
    }


    private void RefreshData()
    {
        Vector3 pos = cam.transform.position;
        GetLocationData();

        string result = String.Format("Longitude = {0}\nLatitude = {1}\n", currentLocation.longitude, currentLocation.latitude);
       // result += String.Format("CameraPos:X = {0}: Y = {1}:Z = {2}\n", pos.x.ToString(), pos.y.ToString(), pos.z.ToString());
        result += String.Format(" Magnetic Heading = {0}\n", magneticHeading);
        UpdateDetails(result);
    }


    public LocationInfo GetLocationData()
    {
        currentLocation = Input.location.lastData;
        return currentLocation;
    }

    private float GetAverage(List<float> values)
    {
        return values.Average();
    }

    public float GetMagneticHeading()
    {
        return magneticHeading;
    }

    public float GetTrueHeading() {

        return trueHeading;
   }


   public Quaternion GetOrientation()
    {

        return GyroToUnity(Input.gyro.attitude);
    }

    private static Quaternion GyroToUnity(Quaternion q)
    {
        return new Quaternion(q.x, q.y, -q.z, -q.w);
    }


}


