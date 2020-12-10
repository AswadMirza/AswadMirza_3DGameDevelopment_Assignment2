using UnityEngine;
using UnityEngine.UI;

//Aswad Mirza 991445135

//Code based  on the example from  the textbook "Unity 2018 Cookbook - Third Edition" - Chapter 2 Excersice 11, and from week 3


/*
 APA rEFERENCE:
Smith, M., & Safari, an O'Reilly Media Company. (2018). Unity 2018 cookbook - third edition (3rd ed.) Packt Publishing.
 
 */
public class Radar : MonoBehaviour
{
    // how far an object should be from the player to still be on the radar
    public float insideRadarDistance = 20;

    //the percentage of the blips relative to the radar
    public float blipSizePercentage = 5;

    //The passed gameObjects will 
    public GameObject rawImageBlipCar;
    public GameObject rawImageBlipCivillian;

    //A bit unnecessary but i am making it so that I can drag a reference of the player object in
    public GameObject playerObject;


    private RawImage rawImageRadarBackground;
    private Transform playerTransform;
    private float radarWidth;
    private float radarHeight;
    private float blipHeight;
    private float blipWidth;

    void Start()
    {
        
        rawImageRadarBackground = GetComponent<RawImage>(); 

        //setting the transform variable to be  based on the game object passed in
        playerTransform = playerObject.transform;

        radarWidth = rawImageRadarBackground.rectTransform.rect.width;
        radarHeight = rawImageRadarBackground.rectTransform.rect.height;
        blipHeight = radarHeight * blipSizePercentage / 100;
        blipWidth = radarWidth * blipSizePercentage / 100;
    }   

    void Update()
    {

        //Very very very intense on cpu, basically removing every blip, drawing them again, calculating postion etc. every frame
        RemoveAllBlips();
        FindAndDisplayBlipsForTag("Civillian", rawImageBlipCivillian);
        FindAndDisplayBlipsForTag("Car", rawImageBlipCar);
    }


    // find game objects with the passed tag, and the prefab to represent them with.
    // for every game object with that tag, we are constanly checking how far they are from the player, and if they are close enough
    // we calculate thier position on the radar, and display them
    private void FindAndDisplayBlipsForTag(string tag, GameObject prefabBlip)
    {
        Vector3 playerPos = playerTransform.position;
        GameObject[] targets = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject target in targets)
        {
            Vector3 targetPos = target.transform.position;
            float distanceToTarget = Vector3.Distance(targetPos, playerPos);
            if ((distanceToTarget <= insideRadarDistance))
                CalculateBlipPositionAndDrawBlip(playerPos, targetPos, prefabBlip);
        }
    }
    // gets the normalized location of the target, calculates thier position on the radar, and then draws them, using the passed prefab
    private void CalculateBlipPositionAndDrawBlip(Vector3 playerPos, Vector3 targetPos, GameObject prefabBlip)
    {
        Vector3 normalisedTargetPosition = NormaizedPosition(playerPos, targetPos);
        Vector2 blipPosition = CalculateBlipPosition(normalisedTargetPosition);
        DrawBlip(blipPosition, prefabBlip);
    }
    //Destroys every single game object that is a blip
    private void RemoveAllBlips()
    {
        GameObject[] blips = GameObject.FindGameObjectsWithTag("Blip");
        foreach (GameObject blip in blips)
            Destroy(blip);
    }
    //This gets the normalized position of the target by taking thier current and and z values, subtracting them by the players x and z coordinates
    // and then dividing them by the internal radar distance, and returning the vector 3
    private Vector3 NormaizedPosition(Vector3 playerPos, Vector3 targetPos)
    {
        float normalisedyTargetX = (targetPos.x - playerPos.x) / insideRadarDistance;
        float normalisedyTargetZ = (targetPos.z - playerPos.z) / insideRadarDistance;
        return new Vector3(normalisedyTargetX, 0, normalisedyTargetZ);
    }

    //calculates the blips position on the radar based on the target position passed, and returns a vector 2 with the x and y coordinates
    private Vector2 CalculateBlipPosition(Vector3 targetPos)
    {
        float angleToTarget = Mathf.Atan2(targetPos.x, targetPos.z) * Mathf.Rad2Deg;
        float anglePlayer = playerTransform.eulerAngles.y;
        float angleRadarDegrees = angleToTarget - anglePlayer - 90;
        float normalizedDistanceToTarget = targetPos.magnitude;
        float angleRadians = angleRadarDegrees * Mathf.Deg2Rad;
        float blipX = normalizedDistanceToTarget * Mathf.Cos(angleRadians);
        float blipY = normalizedDistanceToTarget * Mathf.Sin(angleRadians);
        blipX *= radarWidth / 2;
        blipY *= radarHeight / 2;
        blipX += radarWidth / 2;
        blipY += radarHeight / 2;
        return new Vector2(blipX, blipY);
    }

    //draws the blips based on the postiion given, and the blip prefab
    private void DrawBlip(Vector2 pos, GameObject blipPrefab)
    {
        GameObject blipGO = (GameObject)Instantiate(blipPrefab);
        blipGO.transform.SetParent(transform.parent);
        RectTransform rt = blipGO.GetComponent<RectTransform>();
        rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, pos.x, blipWidth);
        rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, pos.y, blipHeight);
    }
}