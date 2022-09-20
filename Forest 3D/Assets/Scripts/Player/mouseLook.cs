using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class mouseLook : NetworkBehaviour  
{
    public float mouseSensitivity = 100f;

    public Transform playerBody;

    float xRotation = 0f;

    public override void OnNetworkSpawn()
    {
        if (!IsOwner)
        {
            Destroy(this);
        } 
    }
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 1; i < Display.displays.Length; i++)
        {
            Display.displays[i].Activate();
        }
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime * 5;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime* 5;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation,-90f,90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
