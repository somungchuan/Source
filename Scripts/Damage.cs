using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    private int damage = -10;

    private void OnTriggerStay2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        NormalEnemy normalEnemy = collision.GetComponent<NormalEnemy>();
        if(player != null)
        {
            player.ChangeHealth(damage);
        }
        if(normalEnemy != null)
        {
            normalEnemy.ChangeHealth(damage);
        }
    }
}
