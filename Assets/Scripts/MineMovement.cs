using UnityEngine;

public class MineMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float MineSpeed;
    [SerializeField] private float DestroyTime;

    private GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");

        MinesMove();
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, DestroyTime);
    }

    void MinesMove()
    {
        float positionRandom = Random.Range(2, 2.3f);

        if (positionRandom >= 2f && positionRandom <= 2.09f)
        {
            rb.AddForce(MineSpeed * Time.deltaTime * (positionRandom * transform.up), ForceMode2D.Force);
        }

        else if (positionRandom >= 2.1f && positionRandom <= 2.19f)
        {
            rb.AddForce(MineSpeed * Time.deltaTime * (positionRandom * transform.right), ForceMode2D.Force);
        }

        else
        {
            rb.AddForce(MineSpeed * Time.deltaTime * (positionRandom * (-transform.right)), ForceMode2D.Force);
        }
    }
}