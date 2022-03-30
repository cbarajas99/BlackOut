
using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 0.07f;
    private Vector3 velocity = Vector3.zero;
    private float targetFov = 60f;
    private float fovLerpFactor = 0.1f;
    private Camera cam;
    int degrees = 10;
    private bool paused = false;
    public GameObject pausedIndicator;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation((target.transform.position + new Vector3(0f, 0.65f, 0f)) - this.transform.position), 8f * Time.deltaTime);

        if (Input.GetMouseButton(1))
        {

            transform.RotateAround(target.position, Vector3.up, Input.GetAxis("Mouse X") * degrees);
            return;
            //            transform.RotateAround (target.position, Vector3.left, Input.GetAxis ("Mouse Y")* dragSpeed);
        }
        
        // Define a target position above and behind the target transform
        Vector3 targetPosition = target.TransformPoint(new Vector3(2.78999f, 4f, -1.81f));

        // Smoothly move the camera towards that target position
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime, 999f);


          //  (target.transform.position + new Vector3 (0f,0.65f,0f));

        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetFov, fovLerpFactor);

        //this.transform.Rotate(target.transform.position, Input.GetAxis("CamAxis"));
        //this.transform.Rotate(this.transform.position-target.transform.position, Input.GetAxis("CamAxis"));
        //this.transform.LookAt(target.transform.position);

        //this.transform.RotateAround(target.transform.position, Input.GetAxis("Axis 3");
        if (Input.GetKeyDown(KeyCode.P))
        {
            paused = !paused;
            pausedIndicator.SetActive(paused);
        }
        if (paused)
        {
            Time.timeScale = 0f;

        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    //lerps camera's fov to a specified value
    public void LerpToFov(float fov, float lerpfactor)
    {
        this.targetFov = fov;
        this.fovLerpFactor = lerpfactor;
    }


}
