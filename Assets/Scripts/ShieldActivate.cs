using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldActivate : MonoBehaviour
{
    [SerializeField] private GameObject Shield;

    private GameObject Player;    
    private GameObject _shield;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If Player Hits the Pick-up item GameObject then what will happen
        if (collision.CompareTag("Player"))
        {
            Player.GetComponent<PickUpItemSetUp>().ShieldActive_Delay = Player.GetComponent<PickUpItemSetUp>().Temp_ShieldActive_Delay;

            if (Player.GetComponent<PickUpItemSetUp>().Shield == null)
            {
                // Activate the shield
                _shield = Instantiate(Shield);

                // Set Parent of _Shield
                _shield.transform.parent = Player.transform;

                // Set Position
                _shield.transform.localPosition = new Vector3(0, 0, 0);
            }
        }
    }
}
