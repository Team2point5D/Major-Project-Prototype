using UnityEngine;
using System.Collections;

//Marcus
public class PuzzleOneGravCube : MonoBehaviour {

    PlayerBehaviour PB;

    PlayerMoveDTwo PM;

	// Use this for initialization
	void Start () 
    {
      PB = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();

       PM = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMoveDTwo>(); 
	
	}
	
	// Update is called once per frame
	void FixedUpdate () 
    {

        if (PM.changedGravity == true)
        {
           // rigidbody.useGravity = true;

            rigidbody.AddForce(new Vector3(0, -10, 0));
        }
        else if(PM.changedGravity == false)
        {
           // rigidbody.useGravity = false;

            rigidbody.AddForce(new Vector3(0,10,0));


        }
		if (PB.bIsGravityReversed == true)
		{
			// rigidbody.useGravity = true;
			
			rigidbody.AddForce(new Vector3(0, -10, 0));
		}
		else if(PB.bIsGravityReversed == false)
		{
			// rigidbody.useGravity = false;
			
			rigidbody.AddForce(new Vector3(0,10,0));
			
			
		}


        
	
	}
}
