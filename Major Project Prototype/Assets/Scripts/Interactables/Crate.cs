using UnityEngine;
using UnityEngine.UI;
using System.Collections;

    //David
public class Crate : MonoBehaviour {

	public bool bIsObjectLight = false;
	public bool bIsObjectHeavy = false;
	public bool bIsObjectZeroMass = false;

	private PlayerBehaviour PlayerBehaviour;

	void Start ()
	{
		PlayerBehaviour = GameObject.FindWithTag ("Player").GetComponent<PlayerBehaviour>();
	}

	void Update () 
	{
		if(!bIsObjectHeavy && !bIsObjectLight)
		{
			gameObject.rigidbody.mass = 5;
			gameObject.renderer.material.color = Color.white;
		}

		if(bIsObjectZeroMass)
		{
			gameObject.rigidbody.useGravity = false;
			gameObject.renderer.material.color = Color.black;
		}
	}

	void ChangeMass ()
	{
		if(!bIsObjectZeroMass)
		{
			if(PlayerBehaviour.bIsHeavySelected)
			{
				bIsObjectHeavy = !bIsObjectHeavy;
				bIsObjectLight = false;
				gameObject.rigidbody.mass = 10;
				gameObject.renderer.material.color = Color.red;
			}
			else
			{
				bIsObjectLight = !bIsObjectLight;
				bIsObjectHeavy = false;
				gameObject.rigidbody.mass = 1;
				gameObject.renderer.material.color = Color.blue;
			}
		}
	}

	void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.tag == "Bullet")
		{
			ChangeMass();
		}
	}

}
