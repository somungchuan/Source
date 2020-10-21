using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemy : MonoBehaviour
{
    public float normalSpeed = 5f;
    public float alarmSpeed = 10f;
    private bool isAlarm;
    private bool isChild = true;
    private float health = 30;

    private Rigidbody2D rbody;
    private Animator animator;
    private Rigidbody2D prey;
    public GameObject boom;

    [HideInInspector]
    
    public float childvel = 50f;
    private float childTime = 0.5f;
    private float childTimer;

    private Vector2 direction;
    private float changeDirectionTime = 2f;
    private float changeDirectionTimer;

    private float invincibleTime = 2f;
    private float invincibleTimer;
    private bool isInvincible;
    public bool IsChild { get => isChild; set => isChild = value; }

    // Start is called before the first frame update
    void Awake()
    {
        changeDirectionTimer = changeDirectionTime;
        rbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        childTimer = childTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isChild)
        {
            Move();
        }
        else
        {
            childTimer -= Time.deltaTime;
            if(childTimer <= 0)
            {
                isChild = false;
            }
        }


        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer <= 0)
            {
                isInvincible = false;
            }
        }


        Die();

    }

    void Die()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
            GameObject.Instantiate(boom, transform.position, Quaternion.identity);
        }
    }


    public void ChildMove( Vector2 childDir, float force)
    {
        //rbody.velocity = childDir * childvel * Time.deltaTime;
        //rbody.MovePosition(new Vector2(rbody.position.x + childDir.x * childvel * Time.deltaTime, rbody.position.y + childDir.y * childvel * Time.deltaTime));
        rbody.AddForce(childDir * force);
    }

    void Move()
    {
        if (isAlarm && prey != null)
        {
            //rbody.position = Vector2.Lerp(rbody.position, prey.position, alarmSpeed*Time.deltaTime);
            Vector2 dir = (prey.position - rbody.position).normalized;
            rbody.MovePosition(new Vector2(rbody.position.x + dir.x * alarmSpeed * Time.deltaTime, rbody.position.y + dir.y * alarmSpeed * Time.deltaTime));
        }
        else
        {
            changeDirectionTimer -= Time.deltaTime;
            if (changeDirectionTimer <= 0)
            {
                direction = new Vector2(Random.Range(-1, 2), Random.Range(-1, 2));
                changeDirectionTimer = changeDirectionTime;
            }
            if (direction.x > 0 && transform.localScale.x > 0)
            {
                transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
            }
            else if (direction.x < 0 && transform.localScale.x < 0)
            {
                transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
            }
            rbody.MovePosition(new Vector2(rbody.position.x + direction.x * normalSpeed * Time.deltaTime, rbody.position.y + direction.y * normalSpeed * Time.deltaTime));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        prey = collision.GetComponentInChildren<Rigidbody2D>();
        if(collision.tag.Equals("Player"))
        {
            isAlarm = true;
            animator.SetBool("isAlarm", isAlarm);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            isAlarm = false;
            animator.SetBool("isAlarm", isAlarm);
        }
    }


    public void ChangeHealth(int change)
    {
        if (change < 0)
        {
            if (isInvincible)
            {
                return;
            }
            else
            {
                isInvincible = true;
                invincibleTimer = invincibleTime;
            }

        }

        health = Mathf.Clamp(health + change, 0, 100);

        Debug.Log("EnemyHealth: " + health);
    }

}
