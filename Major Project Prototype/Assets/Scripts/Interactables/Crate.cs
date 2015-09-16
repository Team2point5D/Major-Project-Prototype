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

	public float fScaleTimer = 0;
    public float scaleUSize = 1;
    public float scaleSSize = 1;

	private PlayerBehaviour PlayerBehaviour;

	void Start ()
	{
		PlayerBehaviour = GameObject.FindWithTag ("Player").GetComponent<PlayerBehaviour>();

        scaleUSize = PlayerBehaviour.scaleUpSize;

        scaleSSize = PlayerBehaviour.scaleDownSize;
	}

	void Update () 
	{
		fScaleTimer = Mathf.Clamp (fScaleTimer, 0, 1);

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
		transform.localScale = Vector3.Lerp (new Vector3(scaleUSize, transform.localScale.y, transform.localScale.z),
		                                     new Vector3(scaleSSize, transform.localScale.y, transform.localScale.z),
		                                     fScaleTimer);
        if (bIsBig == true)
        {
			fScaleTimer -= 5 * Time.deltaTime;
            gameObject.renderer.material.color = Color.red;
        }
        else
        {
			fScaleTimer += 5 * Time.deltaTime;
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
