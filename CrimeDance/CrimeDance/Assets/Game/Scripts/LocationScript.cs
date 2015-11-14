using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LocationScript : MonoBehaviour {

    public string LocationName;
    public float CameraAngle;

    [Tooltip("First one is right, second left, third up, fourth down, skip the missing ones")]
    public List<LocationScript> neighbours;
    public bool HasTarget = false;
    public bool HasPlayer = false;
    
    
    public LocationScript GetNeighbour(int position)
    {
        LocationScript location = neighbours[position];
        return location;
    }


}
