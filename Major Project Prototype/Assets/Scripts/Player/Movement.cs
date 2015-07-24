using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	public float moveSpeed = 10f;
	public float jumpForce = 10f;
	public float jumpIncrease = 0.2f;
	public float jumpIncreaseTime = 0f;
	public bool isGrounded = true;

	void Start () 
	{

	}

	void Update ()
	{
		if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey (KeyCode.A))
		{
			transform.Translate(-Vector3.right * moveSpeed * Time.deltaTime);
		}

		if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey (KeyCode.D))
		{
			transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
		}

		if (Physics.Raycast(transform.position, Vector3.down, 1f))
		{ 
			isGrounded = true;
		}
		
		else
		{
			isGrounded = false;
		}

		if(Input.GetButtonDown ("Jump") && isGrounded == true)
		{
			rigidbody.velocity = new Vector3(0f, jumpForce, 0f);
			jumpIncreaseTime = 0.5f;
		}

		if(jumpIncreaseTime > 0f)
		{
			jumpIncreaseTime -= Time.deltaTime;
			if(Input.GetButton ("Jump"))
			{
				rigidbody.velocity += new Vector3(0f, jumpIncrease, 0f);
			}
		}

	}
}
