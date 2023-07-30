using UnityEngine;

public class BackGroundMoveWithPlayer : MonoBehaviour
{
    [SerializeField] private float FollowSpeed;

    private GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Move the BackGround as the Player moves
        if (Player != null)
        {
            // Move to the location of the player
            Vector3 NewPos = new Vector3(Player.transform.position.x, Player.transform.position.y);
            Vector3 FollowValue = transform.position = Vector3.Slerp(transform.position, NewPos, FollowSpeed * Time.deltaTime);
        }
    }
}
