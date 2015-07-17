using UnityEngine;
using System.Collections;

//Marcus
public class PlayerShoot : MonoBehaviour
{

    bool canShoot;

    public float shootSpeed;

    public Transform shotSpot;

    public GameObject shotBullet;

    GameObject shotParent;

    // Use this for initialization
    void Start()
    {
        shotParent = GameObject.Find("Magic Shots");
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
    }


    // Update is called once per frame
    void Update()
    {


    }

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Magic Area")
        {
            // print("Im in magic");

            canShoot = true;
        }



    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Magic Area")
        {
            // print("Im in magic");

            canShoot = false;
        }



    }
}
