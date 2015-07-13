using UnityEngine;
using System.Collections;

public class PlayerShoot : MonoBehaviour {

    bool canShoot;

    public float shootSpeed;

    public Transform shotSpot;

    public GameObject shotBullet;

	// Use this for initialization
	void Start () 
    {
	
	}

    void FixedUpdate()
    {
        if (canShoot == true)
        {
            if(Input.GetMouseButton(0))
            {
               Instantiate(shotBullet, shotSpot.position, Quaternion.identity);
            }
        }
    }
        
	
	// Update is called once per frame
	void Update () 
    {

	
	}

    void OnTriggerStay(Collider col) 
    {
        if (col.gameObject.tag == "Magic Area")
        {
           // print("Im in magic");

            canShoot = true;
        }
        else
        {
            //print("Im OUT magic");

            canShoot = false;
        }

    }
}
