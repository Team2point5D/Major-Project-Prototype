using UnityEngine;
using System.Collections;

//Marcus
public class PuzzleOneGravCube : MonoBehaviour {

    PlayerBehaviour Player;

	// Use this for initialization
	void Start () 
    {
      Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();
	}
	
	// Update is called once per frame
	void FixedUpdate () 
    {

        if (Player.bIsGravityReversed == true)
        {
           // rigidbody.useGravity = true;

            rigidbody.AddForce(new Vector3(0, -10, 0));
        }
        else if(Player.bIsGravityReversed == false)
        {
           // rigidbody.useGravity = false;

            rigidbody.AddForce(new Vector3(0,10,0));


        }
//		if (Player.bIsGravityReversed == true)
//		{
//			// rigidbody.useGravity = true;
//			
//			rigidbody.AddForce(new Vector3(0, -10, 0));
//		}
//		else if(PB.bIsGravityReversed == false)
//		{
//			// rigidbody.useGravity = false;
//			
//			rigidbody.AddForce(new Vector3(0,10,0));
//			
//			
//		}


        
	
	}
}
