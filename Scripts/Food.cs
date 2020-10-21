using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    private int addHealth = 10;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            Player player = collision.GetComponent<Player>();

            // 加血操作宜在Player脚本中添加
            //if( player != null)
            //{
            //    player.Health += addHealth;
            //    if (player.Health > 100)
            //    {
            //        player.Health = 100;
            //    }

            //}

            if (player.ChangeHealth(addHealth))
            {
                Destroy(gameObject);
            }
        }
        

        
    }
}
