using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{

    public float mouseSensitivity = 100f;


    float xRotation = 0f;
    float yRotation = 0f;


    void Start()
    {
        //locking the cursor to the middle of the screen and making it invisible
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (!InventorySystem.Instance.isOpen) 
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            // control rotation around x axis (look up and down)
            xRotation -= mouseY;

            //we clamp the rotation so we cant over-rotate (like in real life)
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            // control rotation around y axis (loop up and down)
            yRotation += mouseX;

            //applying both rotations
            transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
        }
    }


}
