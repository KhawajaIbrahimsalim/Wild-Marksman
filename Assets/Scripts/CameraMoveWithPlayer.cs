using UnityEngine;

public class CameraMoveWithPlayer : MonoBehaviour
{
    [SerializeField] private float FollowSpeed;

    private GameObject Player;

    private void Awake()
    {
        transform.position = new Vector3(0, 0, -10);
    }

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Player != null)
        {
            // Move to the location of the player
            Vector3 NewPos = new Vector3(Player.transform.position.x, Player.transform.position.y, -10);
            Vector3 FollowValue = transform.position = Vector3.Slerp(transform.position, NewPos, FollowSpeed * Time.deltaTime);

            // The only purpose for this is to make z = -10 always
            Vector3 Z_Pos = new Vector3(FollowValue.x, FollowValue.y, -10);
            transform.position = Z_Pos;
        }
    }
}