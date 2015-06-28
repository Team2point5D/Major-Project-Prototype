using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour
{

    public float speed;

    Rigidbody myRigidBody;

    bool changedGravity = false;

    // Use this for initialization
    void Start()
    {
        myRigidBody = this.gameObject.GetComponent<Rigidbody>();


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

        // Flip Gravity

        if (Input.GetKeyDown(KeyCode.Space))
        {


            if (changedGravity == false)
            {
                changedGravity = true;
                Physics.gravity = new Vector3(0, 25, 0);
            }
            else if (changedGravity == true)
            {
                changedGravity = false;
                Physics.gravity = new Vector3(0, -25, 0);
            }
        }

    }
}
