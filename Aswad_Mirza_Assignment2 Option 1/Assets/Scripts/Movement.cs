using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Aswad Mirza 991445135
//This script should work for both the cars and the pedestrians.
//Will make them simply pace back and forth between two points, using ojects as a reference
public class Movement : MonoBehaviour
{

    private Vector3 _startPosition;
    private Vector3 _destination;

    // bool used to check whether we made it to our destination or not
    bool _isAtDestination;

    // public game object that gives a reference to where the object should be going
    public GameObject DestinationPointReference;

    //public GameObject that gives a reference to where it should be going back to
    public GameObject ReturningPointReference;

    //used to calculate how fast the gameobject moves
    public float Speed;
    // Start is called before the first frame update
    void Start()
    {
        // the first point it moves back and forth to is set here
        // _startPosition = transform.position;
        _startPosition = ReturningPointReference.transform.position;
        //destination has to be set
        _destination = DestinationPointReference.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector3 newPosition;

        // if we arent at our destination, lets move from our current point, to our destination point
        if (!_isAtDestination)
        {
            
            newPosition = CalculateDestination(transform.position, _destination, Speed);
        }

        // if we made it to our destination, lets move from our current position to the starting/returning point
        else {
            newPosition = CalculateDestination(transform.position, _startPosition, Speed);
        }
          
        //updates the position
        transform.position = newPosition;

       // CheckPointsZ();
       
    }
    // calculates the direction of how to go from our source to our destination
    // and returning the next point to get to our destination based on the direction and speed
    Vector3 CalculateDestination(Vector3 source, Vector3 destination, float _speed ) {
        
        Vector3 direction = (destination - source).normalized;
        
        source += (direction * (_speed*Time.deltaTime));
        return source;
    }

    //problem with this check is that it only works if we are comparing points on the z axis
    void CheckPointsZ() {
        // problem with this, is that its way too specific to this object/scene 
        if (_destination.z < 0)
        {

            if (transform.position.z <= _destination.z)
            {
                _isAtDestination = true;
            }

        }
        else {
            if (transform.position.z >= _destination.z) {
                _isAtDestination = true;
            }
        }
        if (_startPosition.z < 0)
        {
            if (transform.position.z <= _startPosition.z)
            {
                _isAtDestination = false;
            }
        }
        else {
            if (transform.position.z >= _startPosition.z)
            {
                _isAtDestination = false;
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
       
        if (other.gameObject.Equals(DestinationPointReference)) {
            // we are at the destination point, so we move back to the starting/returning point
            _isAtDestination = true;
            //debug to ensure we entered the trigger
            Debug.Log("trigger entered");

            // turns the object in the opposite direction
            transform.Rotate(0, 180, 0);
        }
        
        if (other.gameObject.Equals(ReturningPointReference)) {
            // we arent at the destination point, so we move back to the destination point
            _isAtDestination = false;

            //turns the object around
            transform.Rotate(0, 180, 0);
        }
        
    }
}


