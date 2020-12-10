using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Aswad Mirza 991445135
//This script should work for both the cars and the pedestrians.
//Will make them simply pace back and forth between two points, using a starting point that is provided 
public class MovementOnPoints : MonoBehaviour
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

    //AudioSource
    AudioSource _source;

    void Start()
    {
        // the first point it moves back and forth to is set here
        // _startPosition = transform.position;
        _startPosition = transform.position;
        //destination has to be set
        _destination = Destination;

        _source = gameObject.GetComponent<AudioSource>();
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

         CheckPoints();

        if (!_source.isPlaying) {
            _source.Play();
        }

    }
    // calculates the direction of how to go from our source to our destination
    // and returning the next point to get to our destination based on the direction and speed
    Vector3 CalculateDestination(Vector3 source, Vector3 destination, float _speed)
    {
        // vector math to get the direction
        Vector3 direction = (destination - source).normalized;

        // vector math to get the next "step" or point to get to our destination
        source += (direction * (_speed * Time.deltaTime));
        return source;
    }

    //problem with this check is that it only works if we are comparing points on the z axis
    void CheckPoints()
    {

        // If the distance between where we are, and where we ant to be is close enough, then we are at our destination
        // i spent alot of time trying to find specific points and checking if two points are exactly the same
        // and then found out this function exists.

        if (Vector3.Distance(transform.position, _destination) <= 0.1)
        {

            // we made it to our destination so this is true
            _isAtDestination = true;

            // have the object rotate 180 degrees so it looks like its going back in the right direction
            transform.Rotate(0, 180, 0);
        }

        if (Vector3.Distance(transform.position, _startPosition) <= 0.1 && _isAtDestination)
        {
            // we are back to where we started so now we have to go back to our destination
            _isAtDestination = false;
            transform.Rotate(0, 180, 0);
        } 
    }


}


