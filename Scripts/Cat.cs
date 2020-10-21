using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{

    private bool isTriggered;
    private void Update()
    {
        isTriggered = Input.GetKeyDown("x");


    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (isTriggered)
        {
            player.ChangeHealth(player.MaxHealth);
            Debug.Log("You have cured! Your Current Health: " + player.CurrentHealth);
        }
    }
}
