using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//David
public class Crate : MonoBehaviour {

    [Header("Mass")]
	public bool bIsObjectLight = false;
	public bool bIsObjectHeavy = false;
	public bool bIsObjectZeroMass = false;

    [Header("Scale")]
    public bool bIsBig;

    float scaleUSize;
    float scaleSSize;

	private PlayerBehaviour PlayerBehaviour;

	void Start ()
	{
		PlayerBehaviour = GameObject.FindWithTag ("Player").GetComponent<PlayerBehaviour>();

        scaleUSize = PlayerBehaviour.scaleUpSize;

        scaleSSize = PlayerBehaviour.scaleDownSize;
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

        //Marcus
        if (bIsBig == true)
        {
            transform.localScale = new Vector3(scaleUSize, 0, 0);
            gameObject.renderer.material.color = Color.red;
        }
        else
        {
            transform.localScale = new Vector3(scaleSSize, 0, 0);
            gameObject.renderer.material.color = Color.blue;
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

    void ChangeScale()
    {
    }

	void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.tag == "Bullet")
		{
			ChangeMass();
		}

        if (col.gameObject.tag == "Scale Bullet")
        {
            bIsBig = !bIsBig;
        }
	}

}
