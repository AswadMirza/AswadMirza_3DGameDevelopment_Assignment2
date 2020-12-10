using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Aswad Mirza 991445135
public class Pickup : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject Container;
    public float RotationSpeed = 80f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // roatates the object on its y axis
        Container.transform.Rotate(Vector3.up* RotationSpeed*Time.deltaTime);
    }
}
