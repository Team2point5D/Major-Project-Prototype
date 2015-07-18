using UnityEngine;
using System.Collections;

//Marcus
public class PuzzleOneGravCube : MonoBehaviour {

    PlayerMoveDTwo PM;

	// Use this for initialization
	void Start () 
    {
       // PM = GameObject.Find("Player").GetComponent<PlayerMove>();

        PM = GameObject.Find("Player").GetComponent<PlayerMoveDTwo>(); 
	
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

        
	
	}
}
