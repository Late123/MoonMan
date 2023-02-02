using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FP_Playermovement : MonoBehaviour
{
    public float mouse_sens_x = 1.0f;
    public float mouse_sens_y = 1.0f;


    public float max_move_speed = 0.5f;

    public float pitch_bounds = 80.0f;

    private float speed = 0.0f;
    private Camera main_camera;

    private float pitch = 0.0f;
    private float yaw = 0.0f;

    public Vector3 respawn_point;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        main_camera = Camera.main;
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 camera_up = main_camera.transform.TransformDirection(Vector3.up);
        float mouse_dx = Input.GetAxis("Mouse X") * mouse_sens_x;
        float mouse_dy = Input.GetAxis("Mouse Y") * mouse_sens_y;

        yaw   += mouse_dx;
        pitch += mouse_dy;

        yaw = Mathf.Repeat(yaw, 360.0f);
        pitch = Mathf.Clamp(pitch, -pitch_bounds, pitch_bounds);

        float p_rads = Mathf.Deg2Rad * pitch;
        float y_rads = Mathf.Deg2Rad * yaw;

        main_camera.transform.rotation = Quaternion.EulerAngles(-p_rads, y_rads, 0.0f);
    }


    void FixedUpdate()
    {
        float forward = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");


        Vector3 planar_direction = main_camera.transform.forward;
        planar_direction.y = 0.0f;
        planar_direction.Normalize();

        Vector3 planar_right = main_camera.transform.right;
        planar_right.y = 0.0f;
        planar_right.Normalize();

        Vector3 delta = new Vector3(0.0f, 0.0f, 0.0f);

        delta += planar_direction * forward;
        delta += planar_right * horizontal;

        delta.Normalize();
        delta *= max_move_speed * Time.fixedDeltaTime;

        rb.MovePosition(transform.position + delta);        
        //transform.position = new_position;
    }

    public void Respawn(float delay)
    {
        StartCoroutine(RespawnCoroutine(delay));
    }
    private IEnumerator RespawnCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        transform.position = respawn_point;
    }


}
