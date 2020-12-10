using UnityEngine;
//Aswad Mirza 991445135
//Based on the example from week 8 and from examples from the textbook "Unity 2017 Game AI Programming"
public class Touch : Sense
{
    Wander m_wander;
    protected override void Initialize()
    {

        m_wander = gameObject.GetComponent<Wander>();


    }

    void OnTriggerEnter(Collider other)
    {
        Aspect aspect = other.GetComponent<Aspect>();
        if (aspect != null)
        {
            //Check the aspect
            if (aspect.aspectType == aspectName)
            {
                Debug.Log($" {gameObject.name} has touched a {aspect.aspectType} named {aspect.gameObject.name}");
            }
            else if (aspect.aspectType == Aspect.AspectTypes.BUILDING)
            {
                Debug.Log($" {gameObject.name} has touched a building");
                
                if (m_wander != null)
                {
                    Debug.Log("Recalculating Direction");
                    m_wander.GetNextPosition();

                }
            }

            else if (aspect.aspectType == Aspect.AspectTypes.ENEMY) {
                Debug.Log($" {gameObject.name} has touched an enemy");
                Debug.Log("Recalculating Direction");
                if (m_wander != null)
                {
                    m_wander.GetNextPosition();
                }
            }
        }
    }
    
    //In order to keep the collision of the object, and to prevent it from moving through walls it has oncollision enter


    /*
    private void OnCollisionEnter(Collision collision)
    {
        Aspect aspect = collision.gameObject.GetComponent<Aspect>();
        if (aspect != null) {

            //Checking if it it touching an object that has a different aspect from its current  aspect, just give different logic for this
            if (aspect.aspectType == aspectName) {
                Debug.Log("Something interesting detected");
            }
        }
    }
    */
}
