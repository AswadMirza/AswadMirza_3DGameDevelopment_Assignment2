using UnityEngine;
//Aswad Mirza 991445135
//Based on the example from week 8 and from examples from the textbook "Unity 2017 Game AI Programming"
public class PlayerTank : MonoBehaviour 
{
    public Transform targetTransform;
    public float targetDistanceTolerance = 3.0f;

    private float movementSpeed;
    private float rotationSpeed;

	// Use this for initialization
	void Start () 
    {
        movementSpeed = 10.0f;
        rotationSpeed = 2.0f;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Vector3.Distance(transform.position, targetTransform.position) < targetDistanceTolerance) 
        {
            return;
        }

        Vector3 targetPosition = targetTransform.position;
        targetPosition.y = transform.position.y;
        Vector3 direction = targetPosition - transform.position;

        Quaternion tarRot = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, tarRot, rotationSpeed * Time.deltaTime);

        transform.Translate(new Vector3(0, 0, movementSpeed * Time.deltaTime));
	}
}
