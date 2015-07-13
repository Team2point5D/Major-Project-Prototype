using UnityEngine;
using System.Collections;

public class PuzzleOneGravCube : MonoBehaviour {

    PlayerMove PM;

	// Use this for initialization
	void Start () 
    {
        PM = GameObject.Find("Player").GetComponent<PlayerMove>(); ;
	
	}
	
	// Update is called once per frame
	void FixedUpdate () 
    {
        //rigidbody.velocity -= 200 * Time.fixedTime * (this.gameObject.transform.position - transform.position);

        //rigidbody.useGravity = false;

        //rigidbody.constantForce = 

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
