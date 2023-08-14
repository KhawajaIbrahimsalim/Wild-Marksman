using UnityEngine;

public class MineMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float MineSpeed;
    [SerializeField] private float DestroyTime;

    // Start is called before the first frame update
    void Start()
    {
        MinesMove();
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, DestroyTime);
    }

    void MinesMove()
    {
        int positionRandom = Random.Range(-100, 100);

        if (positionRandom <= 0)
        {
            rb.AddForce(MineSpeed * Time.deltaTime * transform.up, ForceMode2D.Force);
        }

        else if (positionRandom > 0)
        {
            rb.AddForce(MineSpeed * Time.deltaTime * transform.right, ForceMode2D.Force);
        }
    }
}