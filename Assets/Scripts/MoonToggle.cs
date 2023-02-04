using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonToggle : MonoBehaviour
{
    //public Rigidbody moon;
    public float speed = 0.2f;
    public GameObject close, far;
    public float t;
    public float starting_t = 0.5f;

    public AudioSource water_audio_source;

    // Start is called before the first frame update
    private void Start()
    {
        t = starting_t;
    }

    // Update is called once per frame
    private void Update()
    {   
        if (Input.GetMouseButton(0))
        {
            t += speed * Time.deltaTime;

        }
        if (Input.GetMouseButton(1)) 
        {
            t -= speed * Time.deltaTime;
        }

        t = Mathf.Clamp(t, 0, 1);
        transform.position = Vector3.Lerp(far.transform.position, close.transform.position, t);
        transform.localScale = Vector3.Lerp(far.transform.localScale, close.transform.localScale, t);   
    }
}
