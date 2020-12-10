using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Aswad Mirza 991445135 


// Code taken from week 4 from slate. Based on code from "Unity 2018 Cookbook Third Edition" Chapter 10, exercise 6


/*
 APA rEFERENCE:
Smith, M., & Safari, an O'Reilly Media Company. (2018). Unity 2018 cookbook - third edition (3rd ed.) Packt Publishing.
 
 */

public class PropManager : MonoBehaviour
{
    public GameObject prop;
    public Transform targetBone;
    public Vector3 positionOffset;
    public Vector3 rotationOffset;
    public bool destroyTrigger = true;

  
    void OnTriggerEnter(Collider collision)
    {
        bool addPropCondition = targetBone.IsChildOf(collision.transform) & !AlreadyHalreadyHasChildObject();
        if (addPropCondition)
            AddProp();
    }

    private void AddProp()
    {
        GameObject newprop;
        newprop = Instantiate(prop, targetBone.position,
                  targetBone.rotation) as GameObject;
        newprop.name = prop.name;
        newprop.transform.parent = targetBone;
        newprop.transform.localPosition = positionOffset;
        newprop.transform.localEulerAngles = rotationOffset;
        if (destroyTrigger)
            Destroy(gameObject);
    }

    private bool AlreadyHalreadyHasChildObject()
    {
        string propName = prop.name;
        foreach (Transform child in targetBone)
        {
            if (child.name == propName)
                return true;
        }
        return false;
    }
}
