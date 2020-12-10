using UnityEngine;

//Aswad Mirza 991445135
//Based on code from the textbook "Unity 2018 Cookbook - Third Edition" Chapter 7  exercise 6
/*
 APA rEFERENCE:
Smith, M., & Safari, an O'Reilly Media Company. (2018). Unity 2018 cookbook - third edition (3rd ed.) Packt Publishing.
 
 */
public class MiniMap : MonoBehaviour
{
    public GameObject mapUI;

    public GameObject playerObject;
    private Transform target;

    void Start()
    { 
        // We are targeting the passed Player gameobject's transform in
        target = playerObject.transform;
    }

    void Update()
    {
        // We rotate the passed gameobjects z value, to match the player's euler y value
        Vector3 compassAngle = new Vector3();
        compassAngle.z = target.transform.eulerAngles.y;
        mapUI.transform.eulerAngles = compassAngle;
    }
}