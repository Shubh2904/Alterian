using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeCameraFollow : MonoBehaviour
{

    [Header("Camera's constant offset and starting rotation")]
    [SerializeField] Vector3 offset;
    [SerializeField] Vector3 startRotation;

    [Header("Smaller snap speed = More time taken by camera to follow")]
    [SerializeField] float snapSpeed;
    
    Rigidbody rb;


    Transform camera;

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main.transform;

        camera.eulerAngles = startRotation;

        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        camera.position = Vector3.Lerp(camera.position, rb.position + offset + adjustToMouse(), snapSpeed*Time.deltaTime);
    }

    Vector3 adjustToMouse()
    {
        return Vector3.zero;
    }
}
