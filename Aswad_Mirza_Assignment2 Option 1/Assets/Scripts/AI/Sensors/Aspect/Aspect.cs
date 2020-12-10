using UnityEngine;

//Aswad Mirza 991445135
//Code is taken from our week8 lecture on ai sensing
public class Aspect : MonoBehaviour {
	public enum AspectTypes {
		PLAYER,
		ENEMY,
		CIVILLIAN,
		VEHICLE,
		BUILDING,
	}
	public AspectTypes aspectType;
}
