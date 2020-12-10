using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Aswad Mirza 991445135
//Based on the example from week 8 and from examples from the textbook "Unity 2017 Game AI Programming"
public class VehiclePerspective : Sense
{
    public int fieldOfView = 45;
    public int viewDistance = 100;


    public Transform apectPerspectiveTransform;
    public List<Transform> interestingObjects;
    private Vector3 rayDirection;

    // the distance in which this car needs to slow down when it detects a civillian in front of it
    public float breakingDistance = 10f;
    public float breakSpeed = 0.5f;
    public float initialMovementSpeed = 9f;
    protected override void Initialize()
    {
        // this line of code is problematic because it is hardcoded for it to detect the player or enemy, 
        //it should be dynamic and able to detect whatever you want it to detect
        //apectPerspectiveTransform = GameObject.FindGameObjectWithTag("Player").transform;

        //playerTransform = GameObject.Find("Civilian_Test").transform;
        GameObject[] civillianObjects = GameObject.FindGameObjectsWithTag("Civillian");


        if (gameObject.GetComponent<MovementOnPoints>() != null && IsVehicle())
        {
            initialMovementSpeed = gameObject.GetComponent<MovementOnPoints>().Speed;
        }

        foreach (GameObject civillian in civillianObjects) {
            interestingObjects.Add(civillian.transform);
        }

        //interestingObjects.Add(GameObject.FindGameObjectWithTag("Player").transform);

    }

    protected override void UpdateSense()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= detectionRate)
        {
            

            
            for (int i = 0; i < interestingObjects.Count; i++) {
                DetectAspect(interestingObjects[i]);
            }
            
        }
    }

    //Detect perspective field of view for the AI Character
    

    void DetectAspect(Transform aspectTransform)
    {



        RaycastHit hit;
        rayDirection = aspectTransform.position - transform.position;

        if ((Vector3.Angle(rayDirection, transform.forward)) < fieldOfView)
        {
            // Detect if player is within the field of view
            if (Physics.Raycast(transform.position, rayDirection, out hit, viewDistance))
            {
                Aspect aspect = hit.collider.GetComponent<Aspect>();
                // logic for the object it is interested in
                if (aspect != null)
                {
                    //Check the aspect
                    if (aspect.aspectType == aspectName)
                    {


                        //print($"{aspectName} Detected");
                        if (IsVehicle())
                        {
                            CivillianDetectedByCar(aspectTransform);
                        }
                    }
                }
                // logic in case the object it is hitting does not have aspect
                else
                {
                    /*
                    MovementOnPoints movement = gameObject.GetComponent<MovementOnPoints>();
                    if (IsVehicle() && movement != null)
                    {
                        movement.Speed = initialMovementSpeed;
                    }
                    */
                }
            }
            // logic for if the object is no longer in thier field of view
            else {
                
                MovementOnPoints movement = gameObject.GetComponent<MovementOnPoints>();
                if (IsVehicle() && movement != null)
                {
                    movement.Speed = initialMovementSpeed;
                }
                
            }
        }
    }

    /// <summary>
    /// Show Debug Grids and obstacles inside the editor
    /// </summary>
    void OnDrawGizmos()
    {
        if (apectPerspectiveTransform == null)
        {
            return;
        }

        Debug.DrawLine(transform.position, apectPerspectiveTransform.position, Color.red);

        Vector3 frontRayPoint = transform.position + (transform.forward * viewDistance);

        //Approximate perspective visualization
        Vector3 leftRayPoint = frontRayPoint;
        leftRayPoint.x += fieldOfView * 0.5f;

        Vector3 rightRayPoint = frontRayPoint;
        rightRayPoint.x -= fieldOfView * 0.5f;

        Debug.DrawLine(transform.position, frontRayPoint, Color.green);
        Debug.DrawLine(transform.position, leftRayPoint, Color.green);
        Debug.DrawLine(transform.position, rightRayPoint, Color.green);
    }

    // helper functions to return bool values
    bool IsVehicle()
    {

        if (gameObject.tag.Equals("Car"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    bool IsCivillian()
    {
        if (gameObject.tag.Equals("Civillian"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // logic for the cars if they detect a civillian
    void CivillianDetectedByCar(Transform civillianTransform)
    {

        MovementOnPoints movement = gameObject.GetComponent<MovementOnPoints>();
        if (movement != null)
        {

            if (Vector3.Distance(civillianTransform.position, transform.position) < breakingDistance)
            {
                Debug.Log($"A {aspectName}  is detected by ,  {gameObject.name}, breaking");
                movement.Speed -= breakSpeed;
                if (movement.Speed <= 0)
                {
                    movement.Speed = 0;
                }
            }
            else {
                movement.Speed = initialMovementSpeed;
            }

        }
    }
}
