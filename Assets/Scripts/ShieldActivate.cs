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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If Player Hits the Pick-up item GameObject then what will happen
        if (collision.CompareTag("Player"))
        {
            // If pick the shield again then only reset the delay time
            Player.GetComponent<PickUpItemSetUp>().ShieldActive_Delay = Player.GetComponent<PickUpItemSetUp>().Temp_ShieldActive_Delay;

            // Only one shield will be spawn at a time
            if (Player.GetComponent<PickUpItemSetUp>().Shield == null)
            {
                // Activate the shield
                _shield = Instantiate(Shield);

                // Set Parent of _Shield
                _shield.transform.parent = Player.transform;

                // Set Rotation
                _shield.transform.localRotation = new Quaternion(0, 0, 0, 0);

                // Set Position
                _shield.transform.localPosition = new Vector3(0, 0, 0);
            }
        }
    }
}
