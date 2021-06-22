using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    float upDownRotation = 0;
    public float sensitivity = 90;
    public Transform player;

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

        transform.localEulerAngles = new Vector3(upDownRotation, 0, 0);
        player.Rotate(Vector3.up * mouseX);

    }
}
