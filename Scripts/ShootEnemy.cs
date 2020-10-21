using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootEnemy : MonoBehaviour
{

    public GameObject bullet;
    private Animator animator;
    public float bulletForce = 5f;

    private float deltaX;
    private float deltaY;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Rigidbody2D preyRbody = collision.GetComponent<Rigidbody2D>();
        if (collision.tag.Equals("Player"))
        {
            animator.SetTrigger("findPrey");

            if (preyRbody.position.x > transform.position.x && transform.localScale.x < 0)
            {
                transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
                deltaX = 1.96f;
                deltaY = 0.20f;
            }
            else if (preyRbody.position.x < transform.position.x && transform.localScale.x > 0)
            {
                transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
                deltaX = -1.83f;
                deltaY = 0.28f;
            }
            StartCoroutine(shoot(preyRbody));
        }
    }

    IEnumerator shoot(Rigidbody2D preyRbody)
    {
        yield return new WaitForSeconds(2.4f);
        GameObject go = GameObject.Instantiate(bullet, new Vector2(transform.position.x+deltaX, transform.position.y+deltaY ), Quaternion.identity);
        go.GetComponent<NormalEnemy>().IsChild = true;
        Rigidbody2D goRbody = go.GetComponent<Rigidbody2D>();
        Vector2 dir = (preyRbody.position - goRbody.position).normalized;
        go.GetComponent<NormalEnemy>().ChildMove(dir, 600);
    }
}
