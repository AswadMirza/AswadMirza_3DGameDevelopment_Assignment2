using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Aswad Mirza 991445135 

//Code based  on the example from  the textbook "Unity 2018 Cookbook - Third Edition" - Chapter 10  and from week 5


/*
 APA rEFERENCE:
Smith, M., & Safari, an O'Reilly Media Company. (2018). Unity 2018 cookbook - third edition (3rd ed.) Packt Publishing.
 
 */

public class WeaponController : MonoBehaviour
{
    //Animator
    Animator anim;
    // Gun Objects
    public GameObject pistol;
    public GameObject smg;

    // transforms for the hip and hand
    public Transform handBone;
    public Transform hipBone;

    //vector 3  for hand positions
    public Vector3 handPositionOffset;
    public Vector3 handRotationOffset;

    // vector 3 for hip position
    public Vector3 hipPositionOffset;
    public Vector3 hipRotationOffset;

    public Vector3 bulletForce = new Vector3(0, 100, 5000);

    // for finding out which weapon is holstered and selected
    GameObject selectedWeapon;
    GameObject holsteredWeapon;

    // bullet prefab
    public GameObject bulletPrefab;

    // MouseAim variable for setting the weapon for this script
    MouseAim mouseAim;

    public float pistolBulletDelay = 1f;
    public float smgBulletDelay = 0.1f;

    // vector 3s to represent the roation offset the guns need to have while aiming
    public Vector3 pistolAimingRotationOffset;
    public Vector3 smgAimingRotationOffset;


    // Start is called before the first frame update
    void Start()
    {
        mouseAim = gameObject.GetComponent<MouseAim>();
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (getSelectedWeapon() != null) {
            mouseAim.weapon = selectedWeapon.transform;
        }

        //mouse wheel scrolling down
        if (Input.GetAxis("Mouse ScrollWheel") < 0f) {
            swapWeapons();
            Debug.Log("weapons are swapped");
        }
        // if we are holding the left mouse button, and we are in the rifle animation
        if (Input.GetMouseButton(0) && anim.GetBool("Rifle")) {
            StartCoroutine(fireBullet(smgBulletDelay));
        }

        //if we are in the pistol animation 
        if (Input.GetMouseButtonDown(0) && anim.GetBool("Pistol")) {
            StartCoroutine(fireBullet(pistolBulletDelay));
        }

        if (anim.GetBool("Pistol"))
        {
            selectedWeapon.transform.localEulerAngles = pistolAimingRotationOffset;
        }
        else if (anim.GetBool("Rifle"))
        {
            selectedWeapon.transform.localEulerAngles = smgAimingRotationOffset;
        }
        else if(getSelectedWeapon()!=null){
            selectedWeapon.transform.localEulerAngles = handRotationOffset;
        }

    }
    //problem need to set the gameobjects to the weapons already inside of the player, and not the ones in the prefab
    void swapWeapons() {
        GameObject newPistol;
        GameObject newSMG;
        findSelectedWeapon();
        findHolsteredWeapon();
        if (IsBothWeaponsOnPlayer()) {
            if (selectedWeapon.name.Equals(pistol.name))
            {
                Destroy(selectedWeapon);
                Destroy(holsteredWeapon);

                newPistol = Instantiate(pistol, hipBone.position, hipBone.rotation);
                newPistol.name = pistol.name;
                newPistol.transform.SetParent(hipBone);
                newPistol.transform.localPosition = hipPositionOffset;
                newPistol.transform.localEulerAngles = hipRotationOffset;


                newSMG = Instantiate(smg, handBone.position, handBone.rotation);
                newSMG.name = smg.name;
                newSMG.transform.SetParent(handBone);
                newSMG.transform.localPosition = handPositionOffset;
                newSMG.transform.localEulerAngles = handRotationOffset;

                selectedWeapon = newSMG;
                holsteredWeapon = newPistol;

                mouseAim.weapon = selectedWeapon.transform;
                Debug.Log("Switched from pistol to smg");
            }

            else if (selectedWeapon.name.Equals(smg.name)) {

                Destroy(selectedWeapon);
                Destroy(holsteredWeapon);

                newPistol = Instantiate(pistol, handBone.position, handBone.rotation);
                newPistol.name = pistol.name;
                newPistol.transform.SetParent(handBone);
                newPistol.transform.localPosition = handPositionOffset;
                newPistol.transform.localEulerAngles = handRotationOffset;


                newSMG = Instantiate(smg, hipBone.position, hipBone.rotation);
                newSMG.name = smg.name;
                newSMG.transform.SetParent(hipBone);
                newSMG.transform.localPosition = hipPositionOffset;
                newSMG.transform.localEulerAngles = hipRotationOffset;


                selectedWeapon = newPistol;
                holsteredWeapon = newSMG;

                mouseAim.weapon = selectedWeapon.transform;
                Debug.Log("Switched from smg to pistol");
            }
        }
    }

    bool IsBothWeaponsOnPlayer() {
        bool bothWeapons = false;
        bool isPistol = false;
        bool isSMG = false;

        foreach (Transform child in handBone) {
            if (child.name.Equals(pistol.name))
            {
                isPistol = true;
            }
            else if(child.name.Equals(smg.name)){
                isSMG = true;
            }
        
        }

        foreach (Transform child in hipBone) {
            if (child.name.Equals(pistol.name))
            {
                isPistol = true;
            }
            else if (child.name.Equals(smg.name))
            {
                isSMG = true;
            }
        }

        if (isPistol && isSMG)
        {
            bothWeapons = true;
        }
        else {
            bothWeapons = false;
        }

        return bothWeapons;
    }

    void findSelectedWeapon() {
        string gunName = pistol.name;
        string smgName = smg.name;

        // the weapon in the hand is the selected weapon
        foreach (Transform child in handBone) {
            if (child.name.Equals(gunName) || child.name.Equals(smgName))
            {

                selectedWeapon = child.gameObject;
            }
            /*
            else if(child.name.Equals(smgName)) {
                selectedWeapon = smg;
            }
            */
        }
    }

    void findHolsteredWeapon() {
        string gunName = pistol.name;
        string smgName = smg.name;

        // the weapon in the hip is the holstered weapon
        foreach (Transform child in hipBone)
        {
            if (child.name.Equals(gunName) || child.name.Equals(smgName))
            {

                holsteredWeapon = child.gameObject;
            }
            /*
            else if(child.name.Equals(smgName)) {
                selectedWeapon = smg;
            }
            */
        }
    }

    public GameObject getSelectedWeapon() {
        findSelectedWeapon();
        return selectedWeapon;
    }

    public GameObject getHolsteredWeapon() {
        findHolsteredWeapon();
        return holsteredWeapon;
    }

    IEnumerator fireBullet(float delay) {
        yield return new WaitForSeconds(delay);
        GameObject bulletInstance = Instantiate(bulletPrefab, selectedWeapon.transform.position, selectedWeapon.transform.rotation);
        bulletInstance.name = bulletPrefab.name;
        bulletInstance.transform.parent = selectedWeapon.transform;


        Vector3 direction = selectedWeapon.transform.rotation.eulerAngles;
        bulletInstance.transform.rotation = Quaternion.Euler(direction);

        bulletInstance.transform.parent = null;

        bulletInstance.GetComponent<Rigidbody>().AddRelativeForce(bulletForce);
    }


    //changes rotation A to Rotation B
    void SwapRotations(Transform weapon, Quaternion rotationB) {
    }
    



}
