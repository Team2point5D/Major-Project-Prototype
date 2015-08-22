using UnityEngine;
using System.Collections;

public class SonarMove : MonoBehaviour
{

    public float speed;

    public float lifeTime;

    float timer;

    public AudioClip sonarSFX;

    public AudioSource AS;

    // Use this for initialization
    void Start()
    {
        AS = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<AudioSource>();

        AS.clip = sonarSFX;

        AS.Play();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(speed, 0, 0);

        timer += Time.deltaTime;

        if (timer >= lifeTime)
        {
            Destroy(this.gameObject);

            timer = 0;
        }

    }
}
