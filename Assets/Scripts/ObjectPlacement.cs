using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacement : MonoBehaviour {

    public float GPSLattitude;
    public float GPSLongitude;
    public float dToM_Lat;
    public float dToM_Long;
    public RetrieveLocation location;

    // Use this for initialization
    void Start () {

        //-94.151609 - -94.150757 = -0.000852 = 65.94 = 77394.3661971831 meters/degree
        //  45.551457 -  45.551190 = 0.000267 = 29.29 = 109700.3745318352





        location = FindObjectOfType<RetrieveLocation>();
	}
	
    public string ReportLocation()
    {
        

        float newLat = location.currentLocation.latitude - ((location.cam.transform.position.x - transform.position.x) *(1/ dToM_Lat));
        float newLong = location.currentLocation.longitude- ((location.cam.transform.position.z - transform.position.z) *(1/ dToM_Long));

       return "Lat: " + newLat.ToString() + "\nlong: " + newLong.ToString();

      

    }

    public void RotateObject(float value)
    {
        gameObject.transform.rotation = Quaternion.Euler(0, (value), 0);

    }
    public void PlaceObject()
    {
        //

       // float latSep = location.cam.transform.position.x-((location.currentLocation.latitude - GPSLattitude) * dToM_Lat);
       // float longSep = location.cam.transform.position.z - ((location.currentLocation.longitude - GPSLongitude) * dToM_Long);

     //   gameObject.transform.position = new Vector3(latSep, 0, longSep);
        gameObject.transform.rotation = Quaternion.Euler(0, (location.trueHeading), 0);
        //gameObject.transform.Rotate(Vector3.up, 360 - location.trueHeading);
       

    }
	// Update is called once per frame
	void Update () {
      
	}
}
