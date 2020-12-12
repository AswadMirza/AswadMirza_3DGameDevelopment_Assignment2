using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum TrafficLightStates { 
    RED,
    YELLOW,
    GREEN

}
public class TrafficLight : MonoBehaviour
{

    public TrafficLightStates state = TrafficLightStates.GREEN;

    public float InitialSwitchTimer = 5f;
    private float switchTimer = 5f;
    NavMeshObstacle obstacle;
    bool isRed = false;
    // Start is called before the first frame update
    void Start()
    {
        switchTimer = InitialSwitchTimer;
        obstacle = gameObject.GetComponent<NavMeshObstacle>();
    }

    // Update is called once per frame
    void Update()
    {
        switchTimer -= Time.deltaTime;

        if (switchTimer <= 0) {
            //sets the bool to the opposite value
            isRed = !isRed;
            switchTimer = InitialSwitchTimer;
        }

        if (isRed)
        {
            state = TrafficLightStates.RED;
            obstacle.enabled = true;
            Debug.LogWarning("RED LIGHT");
        }
        else {
            state = TrafficLightStates.GREEN;
            obstacle.enabled = false;
            Debug.LogWarning("GREEN LIGHT");
        }
    }
}
