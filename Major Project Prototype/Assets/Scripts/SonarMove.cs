﻿using UnityEngine;
using System.Collections;

public class SonarMove : MonoBehaviour
{

    public float speed;

    public float lifeTime;

    float timer;

    public AudioClip sonarSFX;

    public AudioSource AS;

    Rigidbody rig;

    // Use this for initialization
    void Start()
    {
        AS = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<AudioSource>();

        rig = GetComponent<Rigidbody>();

        AS.clip = sonarSFX;

        AS.Play();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //transform.Translate(speed, 0, 0);

        rig.velocity = new Vector2(speed, 0);

        timer += Time.deltaTime;

        if (timer >= lifeTime)
        {
            Destroy(this.gameObject);

            timer = 0;
        }

    }

    void OnCollisionEnter(Collision col)
    {
        col.gameObject.GetComponent<Renderer>().material.color = Color.red;

      //  print("Hit");

        Destroy(this.gameObject);
    }
}
