using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerBehaviour : MonoBehaviour {

	public bool bIsHeavySelected = false;
	public bool changedGravity = false;
	public bool onCompanion;
	public bool inMagic;
	//public bool onCrate;
	private bool grounded = true;
	private bool canShoot;

	public float shootSpeed;
	public float speed;
	public float jumpHeight;
	public float pushPullForce;

	public Transform shotSpot;

	private Rigidbody myRigidBody;

	public GameObject shotBullet;
	private GameObject CompanionnOBJ;
	private GameObject thingToPushPull;
	private GameObject shotParent;

	public Text teSelectedMass;
	public Text teSelectedGravity;

	void Start()
	{
		myRigidBody = this.gameObject.GetComponent<Rigidbody>();
		shotParent = GameObject.Find("Magic Shots");
	}

	void Update ()
	{
		if(Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown("2"))
		{
			bIsHeavySelected = !bIsHeavySelected;
		}

        if (bIsHeavySelected)
        {
            teSelectedMass.text = "Heavy";
        }
        else
        {
            teSelectedMass.text = "Light";
        }
	}

	void FixedUpdate()
	{

		if (canShoot == true)
		{
			if (Input.GetMouseButtonDown(0))
			{
				// Instantiate(shotBullet, shotSpot.position, Quaternion.identity);
				
				Vector2 target = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
				Vector2 myPos = new Vector2(shotSpot.transform.position.x,shotSpot.transform.position.y);
				Vector2 direction = target - myPos;
				direction.Normalize();
				Quaternion rotation = Quaternion.Euler( 0, 0, Mathf.Atan2 ( direction.y, direction.x ) * Mathf.Rad2Deg + 90 );
				GameObject projectile = (GameObject)Instantiate(shotBullet, myPos, rotation);
				
				//projectile.transform.parent = shotParent.transform;
				
				projectile.rigidbody.velocity = direction * shootSpeed;
				
			}
		}

		// Make a raycast that checks player is on ground or ceilling
		RaycastHit hit;
		
		if (changedGravity == false)
		{
			if (Physics.Raycast(transform.position, -Vector3.up, out hit, 2f))
			{
				
				grounded = true;
				
				print("Grounded");
			}
			else
			{
				grounded = false;
			}
		}
		else
		{
			if (Physics.Raycast(transform.position, Vector3.up, out hit, 2f))
			{
				grounded = true;
				
				print("Grounded");
			}
			else
			{
				grounded = false;
			}
		}
		
		// Player move input

        // TO DO: add xbox controller support
		if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
		{
			myRigidBody.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, myRigidBody.velocity.y);
			
			//if (onCrate == true)
			//{
			//    if (Input.GetKeyDown(KeyCode.C))
			//    {
			
			//    }
			//}
			
		}
		else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
		{
			myRigidBody.velocity = new Vector2(-Input.GetAxis("Horizontal") * -speed, myRigidBody.velocity.y);
		}
		
		
		//If the player is on the ground or the ceilling
		if (grounded == true)
		{
			if (changedGravity == false)
			{
				
				if (Input.GetKeyDown(KeyCode.Space))
				{
					myRigidBody.velocity = new Vector2(Input.GetAxis("Vertical") * 75, myRigidBody.velocity.x);
				}
			}
			else
			{
				if (Input.GetKeyDown(KeyCode.Space))
				{
					myRigidBody.velocity = new Vector2(Input.GetAxis("Vertical") * 75, -myRigidBody.velocity.x);
				}
				
			}
		}
		
		
		// Flip Gravity
		if (inMagic == true)
		{
			if(Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown("1"))
			{
				if (changedGravity == false)
				{
					changedGravity = true;
					teSelectedGravity.text = "Up";
					Physics.gravity = new Vector3(0, 9.81f, 0);
				}
				else if (changedGravity == true)
				{
					changedGravity = false;
					teSelectedGravity.text = "Down";
					Physics.gravity = new Vector3(0, -9.81f, 0);
				}
			}
		}
		
		// Pickup Companion

        //To change so that you toggle press. When pickup the companion i dont have to be colliding with it to hold
		if (onCompanion == true)
		{
			if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
			{
				//print("PickUp cage");
				
				CompanionnOBJ.transform.position = new Vector3(this.gameObject.transform.position.x + 2, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
				
			}
		}
		
		// Push/pull

	}

    // Collision
	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.tag == "Companion")
		{
			print("Companion");
			
			CompanionnOBJ = col.gameObject;
			
			onCompanion = true;
		}
		else if (col.gameObject.tag != "Companion")
		{
			CompanionnOBJ = null;
			
			onCompanion = false;
		}
		
		if (col.gameObject.tag == "Pushable")
		{
			print("Hit Crate");
			
			thingToPushPull = col.gameObject;
			
			Vector3 pushDir = new Vector3(thingToPushPull.rigidbody.velocity.x,0,0);
			
			thingToPushPull.rigidbody.velocity = pushDir * 1;
			
			speed = 5;
		}
		
	}
	
  
	void OnCollisionExit(Collision col)
	{
		if (col.gameObject.tag == "Pushable")
		{
			
			speed = 15;
		}
	}
	
	void OnTriggerStay(Collider col)
	{
		if (col.gameObject.tag == "Magic Area")
		{
			print("Im in magic");
			
			inMagic = true;
		}

		if (col.gameObject.tag == "Magic Area")
		{
			// print("Im in magic");
			
			canShoot = true;
		}

        if (col.gameObject.tag == "Climeable")
        {
            print("Ladder");

            if (Input.GetKey(KeyCode.Q))
            {
               // gameObject.transform.position = new Vector3(0,

                transform.Translate(0f, 0.5f, 0f);
            }
        }

        if (col.gameObject.tag == "Lever")
        {
            print("Lever");

            if (Input.GetKeyDown(KeyCode.E))
            {
               // print("Lever Switch");
            }
        }
	}
	
	void OnTriggerExit(Collider col)
	{
		if (col.gameObject.tag == "Magic Area")
		{
			print("Im OUT magic");
			
			inMagic = false;
			
			//Physics.gravity = new Vector3(0, -9.81f, 0);
		}

		if (col.gameObject.tag == "Magic Area")
		{
			// print("Im in magic");
			
			canShoot = false;
		}

        
	}
	

}
