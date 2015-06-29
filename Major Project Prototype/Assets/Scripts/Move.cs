using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour
{

    public float speed;

    Rigidbody myRigidBody;


    bool changedGravity = false;


    public float jumpHeight;

    bool grounded = true;


    public bool onCage;

    GameObject cageOBJ;


    // Use this for initialization
    void Start()
    {
        myRigidBody = this.gameObject.GetComponent<Rigidbody>();

        // groundDistance = collider.bounds.extents.y;

    }

    void FixedUpdate()
    {

        RaycastHit hit;

        if (Physics.Raycast(transform.position, -Vector3.up, out hit, 2f))
        {
            // offsetDistance = hit.distance;
            grounded = true;

            //print("Grounded");

            //Debug.DrawLine(transform.position, hit.point, Color.cyan);
        }
        else
        {
            grounded = false;
        }

    }

    // Update is called once per frame
    void Update()
    {
        // Vector2 myVec2 = new Vector2(speed, 0);

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            myRigidBody.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, myRigidBody.velocity.y);
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            myRigidBody.velocity = new Vector2(-Input.GetAxis("Horizontal") * -speed, myRigidBody.velocity.y);
        }


        if (grounded == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                myRigidBody.velocity = new Vector2(Input.GetAxis("Vertical") * 75,myRigidBody.velocity.x);
            }
        }


        // Flip Gravity

        if (Input.GetKeyDown(KeyCode.RightControl) || Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (changedGravity == false)
            {
                changedGravity = true;
                Physics.gravity = new Vector3(0, 9.81f, 0);
            }
            else if (changedGravity == true)
            {
                changedGravity = false;
                Physics.gravity = new Vector3(0, -9.81f, 0);
            }
        }

        // Pickup Cage

        if (onCage == true)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                print("PickUp cage");

                cageOBJ.transform.position = new Vector3(this.gameObject.transform.position.x + 2, this.gameObject.transform.position.y, 0);


            }
        }

    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Cage")
        {
            print("Cage");

            cageOBJ = col.gameObject;

            onCage = true;



        }
        else
        {
           // onCage = false;
        }
    }
}
