using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataAccessLayer : MonoBehaviour {

// Sort like an API to access Core Location Functions, will be simplyfying queries
// Gyro Coming Soon

    void GetDetails()
    {
        RetrieveLocation RL = RetrieveLocation.Instance;
        if (Input.location.status == LocationServiceStatus.Running && RL.currentCompass.enabled == true) // Location service has started
        {
            LocationInfo locationData = RL.GetLocationData(); // Contains Longitude Latitude, updates in every 0.5 meters
            float trueHeading = RL.GetTrueHeading();          // True Heading returns float
            float magneticHeading = RL.GetMagneticHeading();  // Magnetic Heading return float 
            Quaternion q = RL.GetOrientation();               // Gyro Attitude Quaternion 

        }
      

    }

    // Should we Implement this --> https://wiki.unity3d.com/index.php/Averaging_Quaternions_and_Vectors

}
