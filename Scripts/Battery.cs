using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour
{
    private int electricity;

    private void Awake()
    {
        Initial();
    }

    private void Initial()
    {
        electricity = 10;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player.ChangeEquipmentElectricity(electricity)) 
            Destroy(gameObject);
    }
}
