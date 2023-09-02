using UnityEngine;

public class RotateWithJoyStick : MonoBehaviour
{
    [Header("Joystick Properties:")]
    public FloatingJoystick joystick;

    [Header("Camera Properties:")]
    [SerializeField] private Camera MainCamera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Player will face towards the JoyStick
        FacingJoyStick();
    }

    void FacingJoyStick()
    {
        // Get the position of the mouse
        //Vector3 MousePosition = Input.mousePosition;
        //MousePosition = MainCamera.ScreenToWorldPoint(MousePosition);

        float shootHorizontal = joystick.Horizontal;
        float shootVertical = joystick.Vertical;

        Vector3 direction = new Vector3(shootHorizontal, shootVertical, 0.0f).normalized;

        //float angle = Mathf.Atan2(moveVertical, moveHorizontal) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.LookRotation(direction);

        // Set a direction
        //Vector2 Direction = new Vector2(moveHorizontal - transform.position.x, moveVertical - transform.position.y);

        // direction is not equal to the vector3.Zero which means transform.up will not reset
        if (direction != Vector3.zero)
        {
            // Set the direction in upward vector
            transform.up = direction * Time.deltaTime;
            Debug.Log(transform.up);
        }
    }
}
