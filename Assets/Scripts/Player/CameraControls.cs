using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    public Transform player;
    public float sensitivity = 90.0f;

    float upDownRotation = 0; 

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        upDownRotation -= mouseY;
        upDownRotation = Mathf.Clamp(upDownRotation, -90, 90);

        //transform.localEulerAngles = new Vector3(upDownRotation, 0, 0);
        Tilt('x', upDownRotation);
        player.Rotate(Vector3.up * mouseX);
    }

    public void Tilt(char axis, float angle)
    {
        switch (axis)
        {
            case 'x':
                transform.localEulerAngles = new Vector3(angle, transform.localEulerAngles.y, transform.localEulerAngles.z);
                break;
            case 'y':
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, angle, transform.localEulerAngles.z);
                break;
            case 'z':
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, angle);
                break;
            default:
                break;
        }
    }

    public void ResetTilt(char axis)
    {
        switch (axis)
        {
            case 'x':
                transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, transform.localEulerAngles.z);
                break;
            case 'y':
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, 0, transform.localEulerAngles.z);
                break;
            case 'z':
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 0);
                break;
            default:
                break;
        }
    }
}
