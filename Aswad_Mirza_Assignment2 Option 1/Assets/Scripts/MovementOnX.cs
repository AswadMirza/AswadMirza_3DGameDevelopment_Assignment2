using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Aswad Mirza 991445135
//This script should work for both the cars and the pedestrians.
//Will make them simply pace back and forth between two points, using ojects as a reference
public class MovementOnX : MonoBehaviour
{

    private Vector3 _startPosition;
    private Vector3 _destination;

    // get a static point to go towards
    public Vector3 Destination;
    // bool used to check whether we made it to our destination or not
    bool _isAtDestination;

    

    //used to calculate how fast the gameobject moves
    public float Speed;
    // Start is called before the first frame update
    void Start()
    {
        // the first point it moves back and forth to is set here
        // _startPosition = transform.position;
        _startPosition = transform.position;

        _destination = Destination;
        //destination has to be set 
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
        else
        {
            newPosition = CalculateDestination(transform.position, _startPosition, Speed);
        }

        //updates the position
        transform.position = newPosition;

         CheckPointsX();

    }
    // calculates the direction of how to go from our source to our destination
    // and returning the next point to get to our destination based on the direction and speed
    Vector3 CalculateDestination(Vector3 source, Vector3 destination, float _speed)
    {

        Vector3 direction = (destination - source).normalized;

        source += (direction * (_speed * Time.deltaTime));
        return source;
    }

    //problem with this check is that it only works if we are comparing points on the z axis
    void CheckPointsX()
    {

        // checks if we are on the same x and z coordinate as the source and destination.
        // integer casting to avoid complications with Floating point numbers

        if (Mathf.Approximately(transform.position.x, _destination.x))
        {
            _isAtDestination = true;
            transform.Rotate(0, 180, 0);
        }

        else if (Mathf.Approximately(transform.position.x, _startPosition.x) && _isAtDestination == true)
        {

            _isAtDestination = false;
            transform.Rotate(0, 180, 0);
        }

        /*
        if ((int)transform.position.x == (int)_destination.x )
        {
            _isAtDestination = true;
            transform.Rotate(0, 180, 0);
        }

        else if ((int)transform.position.x == (int)_startPosition.x&& _isAtDestination == true)
        {

            _isAtDestination = false;
            transform.Rotate(0, 180, 0);
        }
        */
        /*
        if ((int)transform.position.x == (int)Destination.x && (int) transform.position.z==(int)_destination.z)
        {
            _isAtDestination = true;
        }

        else if ((int)transform.position.x == (int)_startPosition.x&& (int)transform.position.z == (int)_startPosition.z && _isAtDestination==true) {

            _isAtDestination = false;
        }
        */
        /*
        // problem with this, is that its way too specific to this object/scene 
        if (_destination.x <= 0)
        {

            if (transform.position.x <= _destination.x && _isAtDestination == false)
            {
                _isAtDestination = true;
            }

        }
        else if(_destination.x >0)
        {
            if (transform.position.x >= _destination.x &&_isAtDestination == false)
            {
                _isAtDestination = true;
            }
        }
        
        if (_startPosition.x <= 0)
        {
            if (transform.position.x >= _startPosition.x&& _isAtDestination == true)
            {
                _isAtDestination = false;
            }
        }
        else if(_startPosition.x >=0)
        {
            if (transform.position.x <= _startPosition.x && _isAtDestination == true)
            {
                _isAtDestination = false;
            }
        }
        */
    }


}


