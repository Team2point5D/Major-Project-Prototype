﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class PlayerBehaviour : MonoBehaviour {

	[Header("Powers")]
	public bool bIsHeavySelected = false;
	public bool bIsGravityReversed = false;
	private bool onCompanion;
	private bool inMagic;
	//public bool onCrate;

	[Header("Shooting")]
	public float shootSpeed;
	public Transform shotSpot;
	public GameObject shotBullet;
	private bool canShoot;

	[Header("Movement")]
	public float moveSpeed;
	public float jumpForce;
	public float jumpIncrease;
	public float pushPullForce;
	private float jumpIncreaseTime;
	private bool bIsGrounded = true;

    [Range(1f, 100f)]
    [SerializeField]
    float gravityForce;

	[Header("User Interface")]
	public RectTransform rectCanvas;
	public Image rectAimerFollow;
	public Image imAimer;
	public Text teSelectedMass;
	public Text teSelectedGravity;

	public float fClampedY = 0;
	public float fClampedX = 0;

    [Header("Sonar")]
    public GameObject sonarBull;

	private Rigidbody myRigidBody;

	private GameObject CompanionnOBJ;
	private GameObject thingToPushPull;
	private GameObject shotParent;

   

	void Start()
	{
		myRigidBody = this.gameObject.GetComponent<Rigidbody>();
		shotParent = GameObject.Find("Magic Shots");
	}

	void Update ()
	{


		Vector3 cursorPosition = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 0);
		Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, transform.position + new Vector3 (0f, 0f, 0f));
		rectAimerFollow.rectTransform.anchoredPosition = screenPoint - rectCanvas.sizeDelta / 2f;

		fClampedX = Mathf.Clamp (fClampedX, -100, 100);
		fClampedY = Mathf.Clamp (fClampedY, -100, 100);

		if (fClampedY >= 0)
		{
			if (fClampedX >= 0)
			{
				fClampedX = 100 - fClampedY;
			}
			else
			{
				fClampedX = -100 + fClampedY;
			}
		}
		else
		{
			if (fClampedX >= 0)
			{
				fClampedY = fClampedX - 100;
			}
			else
			{
				fClampedY = -100 - fClampedX;
			}
		}

		cursorPosition.x = Mathf.Clamp (cursorPosition.x, (rectAimerFollow.rectTransform.position.x - 100), (rectAimerFollow.rectTransform.position.x + 100));
		cursorPosition.y = Mathf.Clamp (cursorPosition.y, (rectAimerFollow.rectTransform.position.y - 100), (rectAimerFollow.rectTransform.position.y + 100));
		imAimer.rectTransform.position = cursorPosition;

		Screen.showCursor = false;


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
        //Vector3 extraGravityForce = (Physics.gravity * gravityForce) - Physics.gravity;
        //rigidbody.AddForce(extraGravityForce);

		float clampedY = Mathf.Clamp (0, 0, 0);
		float clampedZ = Mathf.Clamp (0, 0, 0);
		transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, clampedY, clampedZ);

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
		if (bIsGravityReversed == false)
		{
			if (Physics.Raycast(transform.position, Vector3.down, 1f))
			{ 
				bIsGrounded = true;
			}
			else
			{
				bIsGrounded = false;
			}
		}
		else
		{
			if (Physics.Raycast(transform.position, Vector3.up, 1f))
			{ 
				bIsGrounded = true;
			}
			else
			{
				bIsGrounded = false;
			}
		}
		
		// Player move input
		if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey (KeyCode.A))
		{
//			transform.Translate(-Vector3.right * moveSpeed * Time.deltaTime);

            rigidbody.velocity = -Vector3.right * moveSpeed * Time.deltaTime;
		}
		if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey (KeyCode.D))
		{
//			transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);

            rigidbody.velocity = Vector3.right * moveSpeed * Time.deltaTime;
		}
        // TO DO: add xbox controller support
		transform.Translate(Vector3.right * Input.GetAxis("LeftThumbstickX") * moveSpeed * Time.deltaTime);
		
		//If the player is on the ground or the ceilling
		if(bIsGravityReversed == false)
		{
			if((Input.GetButtonDown ("Jump") || Input.GetButtonDown ("A")) && bIsGrounded == true)
			{
				rigidbody.velocity = new Vector3(0f, jumpForce, 0f);
				jumpIncreaseTime = 0.5f;
			}
			if(jumpIncreaseTime > 0f)
			{
				jumpIncreaseTime -= Time.deltaTime;
				if((Input.GetButtonDown ("Jump") || Input.GetButtonDown ("A")))
				{
					rigidbody.velocity += new Vector3(0f, jumpIncrease, 0f);
				}
			}
		}
		else
		{
			if((Input.GetButtonDown ("Jump") || Input.GetButtonDown ("A")) && bIsGrounded == true)
			{
				rigidbody.velocity = new Vector3(0f, -jumpForce, 0f);
				jumpIncreaseTime = 0.5f;
			}
			if(jumpIncreaseTime > 0f)
			{
				jumpIncreaseTime -= Time.deltaTime;
				if(Input.GetButton ("Jump"))
				{
					rigidbody.velocity += new Vector3(0f, -jumpIncrease, 0f);
				}
			}
		}
		
        //Sonar
        if (Input.GetKeyDown("3"))
        {
            	Vector2 target = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
				Vector2 myPos = new Vector2(shotSpot.transform.position.x + 2,shotSpot.transform.position.y);
				Vector2 direction = target - myPos;
				direction.Normalize();
				Quaternion rotation = Quaternion.Euler( 0, 0, Mathf.Atan2 ( direction.y, direction.x ) * Mathf.Rad2Deg + 90 );


                GameObject sonarShoot = (GameObject)Instantiate(sonarBull, myPos, Quaternion.identity);
        }
		
		// Flip Gravity
		if (inMagic == true)
		{
			if(Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown("1") || Input.GetButtonDown ("Y"))
			{
				if (bIsGravityReversed == false)
				{
					bIsGravityReversed = true;
					teSelectedGravity.text = "Up";
					Physics.gravity = new Vector3(0, 9.81f, 0);
//					LeanTween.rotateX (gameObject, 180, 1f);
//					LeanTween.rotateLocal (gameObject, new Vector3 (180, 0, 0), 1f);
				}
				else if (bIsGravityReversed == true)
				{
					bIsGravityReversed = false;
					teSelectedGravity.text = "Down";
					Physics.gravity = new Vector3(0, -9.81f, 0);
//					LeanTween.rotateX (gameObject, 0, 1f);
//					LeanTween.rotateLocal (gameObject, new Vector3 (0, 0, 0), 1f);
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
				
				CompanionnOBJ.transform.position = new Vector3(this.gameObject.transform.position.x + 1, this.gameObject.transform.position.y + 1, this.gameObject.transform.position.z);

               // onCompanion = true;
				
			}
		}
		
		// Push/pull

	}

    // Collision
	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.tag == "Companion")
		{
			//print("Companion");
			
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
			//print("Hit Crate");
			
			thingToPushPull = col.gameObject;
			
			Vector3 pushDir = new Vector3(thingToPushPull.rigidbody.velocity.x,0,0);
			
			thingToPushPull.rigidbody.velocity = pushDir * 1;
			
			moveSpeed = 5;
		}
		
	}
	
  
	void OnCollisionExit(Collision col)
	{
		if (col.gameObject.tag == "Pushable")
		{
			moveSpeed = 15;
		}
	}
	
	void OnTriggerStay(Collider col)
	{
		if (col.gameObject.tag == "Magic Area")
		{
			//print("Im in magic");
			
			inMagic = true;
		}

		if (col.gameObject.tag == "Magic Area")
		{
			// print("Im in magic");
			
			canShoot = true;
		}

        if (col.gameObject.tag == "Climeable")
        {
            //print("Ladder");

            if (Input.GetKey(KeyCode.Q))
            {
              

                transform.Translate(0f, 0.5f, 0f);
            }
        }

        if (col.gameObject.tag == "Lever")
        {
            //print("Lever");

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
			//print("Im OUT magic");
			
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
