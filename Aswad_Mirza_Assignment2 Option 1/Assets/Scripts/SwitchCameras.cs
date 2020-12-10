using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Aswad Mirza 991445135
//Logic to switch cameras of the player, based on the example in week 2
/*
 APA rEFERENCE:
Smith, M., & Safari, an O'Reilly Media Company. (2018). Unity 2018 cookbook - third edition (3rd ed.) Packt Publishing.
 
 */
public class SwitchCameras : MonoBehaviour
{

    // I used 4 gameobject References, because I like naming them
    public Camera FPSCam;
    public Camera ThirdPersonCam;
    public Camera LeftCam;
    public Camera RightCam;
    public bool changeAudioListener = true;
   


    // Update is called once per frame
    void Update()
    {
        // logic for fps Camera
        if (Input.GetKeyDown("f"))
        {
            EnableCamera(FPSCam, true);
            EnableCamera(ThirdPersonCam, false);
            EnableCamera(LeftCam, false);
            EnableCamera(RightCam, false);
        }

        //switches to third person camera
        else if (Input.GetKeyDown("g"))
        {
            EnableCamera(FPSCam, false);
            EnableCamera(ThirdPersonCam, true);
            EnableCamera(LeftCam, false);
            EnableCamera(RightCam, false);
        }

        // switches to left camera
        else if (Input.GetKeyDown("h"))
        {
            EnableCamera(FPSCam, false);
            EnableCamera(ThirdPersonCam, false);
            EnableCamera(LeftCam, true);
            EnableCamera(RightCam, false);
        }

        // switches to right camera
        else if (Input.GetKeyDown("j"))
        {
            EnableCamera(FPSCam, false);
            EnableCamera(ThirdPersonCam, false);
            EnableCamera(LeftCam, false);
            EnableCamera(RightCam, true);
        }
    }


    private void EnableCamera(Camera camera, bool isEnabled) {
        camera.enabled = isEnabled;
        if (changeAudioListener) {
            camera.GetComponent<AudioListener>().enabled = isEnabled;
        }
    }
}
