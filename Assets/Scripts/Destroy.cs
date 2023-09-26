using UnityEngine;

public class Destroy : MonoBehaviour
{
    [SerializeField] private float DestroyTime;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, DestroyTime);
    }
}
