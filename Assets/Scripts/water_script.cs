using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;
//using Cinemachine.Examples;
public class water_script : MonoBehaviour
{
	public GameObject moon;
    public float far_height = 0.0f;
    public float close_height = 10.0f;
    public float move_speed = 100.0f;
	public float moon_height_when_close = 50.0f;
	public float moon_height_when_far = 100.0f;
    private AudioSource audioSource;
    public AudioClip deathSound;
    private FP_Playermovement player_movement_script;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        player_movement_script = GameObject.FindGameObjectWithTag("Player").GetComponent<FP_Playermovement>();
		float desired_height = get_desired_height();
		Vector3 new_position = new Vector3(transform.position.x, desired_height, transform.position.z);
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
    }

    
    void FixedUpdate()
    {
        float desired_height = get_desired_height();
        float new_height = Mathf.MoveTowards(transform.position.y, desired_height, move_speed * Time.fixedDeltaTime);

        Vector3 new_position = new Vector3(transform.position.x, new_height, transform.position.z);

        rb.MovePosition(new_position);
        //transform.position = new_position;
    }

	float get_desired_height() {
		float moon_height = moon.transform.position.y;
		float moon_height_range = moon_height_when_far - moon_height_when_close;

		float t = (moon_height - moon_height_when_close) / moon_height_range;

		return Mathf.Lerp(close_height, far_height, t);
	}
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            audioSource.Stop();
            audioSource.clip = deathSound;
            audioSource.Play();
            player_movement_script.Respawn(1.0f);
        }
    }

}
