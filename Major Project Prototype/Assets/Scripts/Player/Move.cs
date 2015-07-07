using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour
{

    public float speed;

    Rigidbody myRigidBody;


    bool changedGravity = false;


    public float jumpHeight;

    bool grounded = true;


    public bool onCompanion;

    GameObject CompanionnOBJ;


    public float pushPullForce;

    //public bool onCrate;

    Transform thingToPushPull;


    // Use this for initialization
    void Start()
    {
        myRigidBody = this.gameObject.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {

        // Make a raycast that checks player is on ground or ceilling
        RaycastHit hit;

        if (changedGravity == false)
        {
            if (Physics.Raycast(transform.position, -Vector3.up, out hit, 2f))
            {

                grounded = true;

                //print("Grounded");
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

                //print("Grounded");
            }
            else
            {
                grounded = false;
            }
        }

        // Player move input
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
        if (onCompanion == true)
        {
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                //print("PickUp cage");

                CompanionnOBJ.transform.position = new Vector3(this.gameObject.transform.position.x + 2, this.gameObject.transform.position.y, 0);

            }
        }

        // Push/pull



    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Cage")
        {
            print("Cage");

            CompanionnOBJ = col.gameObject;

            onCompanion = true;
        }
        else if (col.gameObject.tag != "Cage")
        {
            CompanionnOBJ = null;

            onCompanion = false;
        }

        //if (col.gameObject.tag == "Crate")
        //{
        //    print("Crate");

        //    crateOBJ = col.gameObject;

        //    onCrate = true;
        //}
        //else
        //{
        //    crateOBJ = null;

        //    onCrate = false;
        //}



    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // if (hit.transform.tag == "Crate") // NOTE: tags checking may be better
        //{
        //thingToPushPull = hit.transform;

        print("Hit Crate");

        Rigidbody body = hit.collider.attachedRigidbody;
        if (body == null || body.isKinematic)
            return;

        if (hit.moveDirection.y < -0.3F)
            return;

        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
        body.velocity = pushDir * pushPullForce;
        // }
    }
}
