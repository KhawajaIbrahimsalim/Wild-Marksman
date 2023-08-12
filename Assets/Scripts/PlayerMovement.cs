using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [Header("Joystick Properties:")]
    public FixedJoystick joystick;
    [SerializeField] private RectTransform joystick_Move_Handle;

    [Header("Health Properties:")]
    public float PlayerMaxHealth;
    public float PlayerHealth;
    [SerializeField] private Slider PlayerHealthBar;

    [Header("Movement status:")]
    public float MoveSpeed;

    [Header("Void Trap Properties")]
    [SerializeField] private float VoidSpeed;
    [SerializeField] private float AttractionSpeed;

    [Header("Wave Manager:")]
    [SerializeField] private GameObject UpgradePanel;

    private GameObject Void;
    private float Temp_Movespeed;
    private bool IsNearVoid;
    private float moveHorizontal;
    private float moveVertical;
    private float Temp_Sped;
    private GameObject GameController;

    // Start is called before the first frame update
    void Start()
    {
        Temp_Movespeed = MoveSpeed;

        PlayerHealth = PlayerMaxHealth;

        Time.timeScale = 1;

        GameController = GameObject.Find("GameController");
    }

    [System.Obsolete]
    private void Update()
    {
        // This condition will only be true when Player is unable to do the required kill in a given time
        if (GameController.active == false)
        {
            // Don't Let the Player Destroyed
            PlayerHealth = PlayerMaxHealth;
        }

        PlayerHealthBar.value = PlayerHealth / PlayerMaxHealth;
    }

    // Update is called once per frame
    [System.Obsolete]
    void FixedUpdate()
    {
        if (UpgradePanel.active == true)
        {
            Temp_Sped = 0;
        }

        else if (joystick_Move_Handle.localPosition != new Vector3(0, 0, 0))
        {
            Temp_Sped = 1;
        }

        moveHorizontal = joystick.Horizontal;
        moveVertical = joystick.Vertical;

        GetComponent<Rigidbody2D>().velocity = new Vector3(moveHorizontal * MoveSpeed * Temp_Sped * Time.deltaTime, moveVertical * MoveSpeed * Temp_Sped * Time.deltaTime);

        if (IsNearVoid)
        {
            Vector3 Direction = Void.transform.position - transform.position;

            float Distance = Vector2.Distance(Void.transform.position, transform.position);

            transform.position += (Direction * AttractionSpeed);
            Debug.Log(Distance);
            //GetComponent<Rigidbody2D>().AddForce(new Vector2(-Direction.x * AttractionSpeed, -Direction.y * AttractionSpeed));
        }
    }

    // If inside the Void the Speed is Slow
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Void"))
        {
            MoveSpeed = VoidSpeed;

            Void = collision.gameObject;

            IsNearVoid = true;
        }
    }

    // On Exiting the Void the Speed is back is to normal
    [System.Obsolete]
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Void"))
        {
            MoveSpeed = Temp_Movespeed;

            IsNearVoid = false;
        }
    }
}
