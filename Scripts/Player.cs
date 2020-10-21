using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // 状态
    public bool isDie;
    public bool isFaint;
    public bool isRucksacked;
    public bool isArmed;
    public bool isTriggered;
    public bool isProtected;
    private Vector3 direction;

    // 血量
    private int currentHealth;
    private int maxHealth;

    // 精神力
    private int currentSanity;
    private int maxSanity;
    // 受天气影响
    private int sanityIncrease;
    private int sanityReduce;

    // 天气影响时间间隔
    private float weatherInflunceDeltaTime;
    private float weatherInflunceDeltaTimer;

    // 触发按键名称
    public string triggerKeyName;

    // 速度
    public float moveSpeed;
    
    // 无敌时间
    private float invincibleTime;
    private float invincibleTimer;
    private bool isInvincible;

    // 组件
    private Rigidbody2D rbody;
    private Animator animator;
    public GameObject boom;
    private EquipmentManeger equipment;
    private Weather weather;

    public int CurrentHealth { get => currentHealth; set => currentHealth = value; }
    public int MaxHealth { get => maxHealth; set => maxHealth = value; }
    public Vector3 Direction { get => direction; set => direction = value; }

    void Start()
    {
        Initial();
    }

    void Update()
    {
        // 移动
        Move();
        
        // 更新无敌状态
        InvincibleTimeUpdate();

        // 状态更新
        StateUpdate();

        // 死亡判定
        GameOver();

        // 触发事件
        Trigger();

        // 时间更新
        TimeUpdate();

        // 天气影响精神力和电量
        WeatherInflunce();
    }

    private void TimeUpdate()
    {
        weatherInflunceDeltaTimer -= Time.deltaTime;
    }

    // 初始化
    private void Initial()
    {
        isDie = false;
        isFaint = false;
        isRucksacked = false;
        isArmed = false;
        isTriggered = false;
        isProtected = false;

        weatherInflunceDeltaTime = 1;
        weatherInflunceDeltaTimer = weatherInflunceDeltaTime;
        MaxHealth = 100;
        CurrentHealth = MaxHealth / 2;

        maxSanity = 100;
        currentSanity = maxSanity;
        sanityIncrease = 6;
        sanityReduce = -4;

        moveSpeed = 5f;

        invincibleTime = 2f;
        invincibleTimer = 0;

        triggerKeyName = "x";

        rbody = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        weather = GameObject.FindGameObjectWithTag("Weather").GetComponent<Weather>();
    }

    private void InvincibleTimeUpdate()
    {
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer <= 0)
            {
                isInvincible = false;
            }
        }
    }

    private void StateUpdate()
    {
        /******** 血量状态 ********/
        if (CurrentHealth <= 0)
        {
            if (isProtected)
            {
                CurrentHealth = MaxHealth;
                isProtected = false;
            }
            else
                isDie = true;
        }

        /******** 精神力状态 ********/
        if(currentSanity <= 0)
        {
            // 昏厥
            isFaint = true;
        }
        else if((float)currentSanity / (float)maxSanity <= 0.1f)
        {
            // 操作大概率按不出来
        }
        else if((float)currentSanity / (float)maxSanity <= 0.3f)
        {
            // 战斗miss，有可能伤害自己
        }
        else if((float)currentSanity / (float)maxSanity <= 0.6f)
        {
            // 视野受限，屏幕亮度降低
        }

        /******** 装备状态 ********/
        if (isArmed)
        {
            if (equipment.CurrentElectricity <= 0)
            {
                equipment.IsExhausted = true;
            }
            else
            {
                equipment.IsExhausted = false;
            }
        }

    }

    void GameOver()
    {
        if (isDie || isFaint)
        {
            Destroy(gameObject);
            GameObject.Instantiate(boom, transform.position, Quaternion.identity);
        }
    }
    private void Move()
    {
        //平滑移动
        //float moveX = Input.GetAxis("Horizontal");
        //float moveY = Input.GetAxis("Vertical");

        //即时移动
        float moveX = 0;
        float moveY = 0;
        if(moveY == 0)
            moveX = Input.GetAxisRaw("Horizontal");
        if(moveX == 0)
            moveY = Input.GetAxisRaw("Vertical");
        

        MoveAnimation(moveX, moveY);

        //这种方式会抖动
        //transform.position = new Vector2(transform.position.x + moveX * speed * Time.deltaTime, transform.position.y + moveY * speed * Time.deltaTime);

        //这种方式可以取消抖动
        rbody.MovePosition(new Vector2(rbody.position.x + moveX * moveSpeed * Time.deltaTime, rbody.position.y + moveY * moveSpeed * Time.deltaTime));

        // 获取当前方向
        if(moveX > 0)
            Direction = new Vector3(0, 0, 0);
        else if(moveX < 0)
            Direction = new Vector3(0, 0, 180);
        if (moveY > 0)
            Direction = new Vector3(0, 0, 90);
        else if (moveY < 0)
            Direction = new Vector3(0, 0, 270);

        
    }

    private void MoveAnimation(float moveX, float moveY)
    {
        if (moveX > 0)
        {
            if (transform.localScale.x < 0)
            {
                transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
            }

            animator.SetBool("isWalkingRight", true);

        }
        else if (moveX < 0)
        {
            if (transform.localScale.x > 0)
            {
                transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
            }
            animator.SetBool("isWalkingRight", true);

        }
        else
        {
            animator.SetBool("isWalkingRight", false);
        }


        if (moveY > 0)
        {
            animator.SetBool("isWalkingBack", true);
            animator.SetBool("isWalkingForward", false);
        }
        else if (moveY < 0)
        {
            animator.SetBool("isWalkingBack", false);
            animator.SetBool("isWalkingForward", true);
        }
        else
        {
            animator.SetBool("isWalkingBack", false);
            animator.SetBool("isWalkingForward", false);
        }
    }

    public bool ChangeHealth( int change )
    {
        if (change < 0)
        {
            if (isInvincible)   // 无敌状态不受影响
            {
                return false;
            }
            else
            {
                isInvincible = true;
                invincibleTimer = invincibleTime;
            }

        }

        if (CurrentHealth >= MaxHealth) return false;
        else
        {
            CurrentHealth = Mathf.Clamp(CurrentHealth + change, 0, 100);
            return true;
        }
    }

    public bool ChangeSanity(int change)
    {
        if (currentSanity >= maxSanity && change > 0) return false;
        else
        {
            currentSanity = Mathf.Clamp(currentSanity + change, 0, 100);
            return true;
        }
    }

    public bool ChangeEquipmentElectricity(int change)
    {
        if(isArmed)
        {
            equipment.CurrentElectricity = Mathf.Clamp(equipment.CurrentElectricity + change, 0, equipment.MaxElectricity);
            Debug.Log("CurrentElectricity" + equipment.CurrentElectricity);
            return true;
        }
        else
        {
            Debug.Log("You have no equipment!");
            return false;
        }
    }

    public void GetRucksack()
    {
        isRucksacked = true;
    }

    public void GetArmed()
    {
        equipment = EquipmentManeger.getInstance();
        isArmed = true;
    }

    private void Trigger()
    {
        isTriggered = Input.GetKeyDown(triggerKeyName);
        if (isTriggered)
        {
            isTriggered = !Input.GetKeyUp(triggerKeyName);
            Debug.Log("Had put down x");
        }
    }

    public void GetChip()
    {
        if(isArmed)
        {
            if(equipment.CurrentChipNum >= equipment.MaxChipNum)
            {
                return;
            }
            else
            {
                equipment.CurrentChipNum += 1;
            }
        }
    }

    public void GetProtected()
    {
        isProtected = true;
    }

    private void WeatherInflunce()
    {
        if(weatherInflunceDeltaTimer <= 0)
        {
            weatherInflunceDeltaTimer = weatherInflunceDeltaTime;

            // 影响精神力
            if(weather.CurrentDay == Weather.dayMode.Day)
            {
                if(weather.CurrentWeather == Weather.weatherMode.Sunny)
                {
                    // 白天晴天，快速恢复精神力
                    ChangeSanity(sanityIncrease);
                }
                else
                {
                    // 白天雨天，慢速恢复精神力
                    ChangeSanity(sanityIncrease/2);
                }
            }
            else
            {
                if (weather.CurrentWeather == Weather.weatherMode.Sunny)
                {
                    // 夜晚晴天，慢速减少精神力
                    ChangeSanity(sanityReduce / 2);
                }
                else
                {
                    // 夜晚雨天，快速减少精神力
                    ChangeSanity(sanityReduce);
                }
            }
            Debug.Log("CurrentSanity:   " + currentSanity + "==== CurrentHealth:   " + currentHealth);

            if (isArmed)
            {
                // 影响电量
                if (weather.CurrentDay == Weather.dayMode.Day)
                {
                    if (weather.CurrentWeather == Weather.weatherMode.Sunny)
                    {
                        // 白天晴天，快速恢复电量
                        ChangeEquipmentElectricity(equipment.MaxElectricity / 5);
                    }
                    else
                    {
                        // 白天雨天，慢速恢复电量
                        ChangeEquipmentElectricity(equipment.MaxElectricity / 20);
                    }
                }
                else
                {       // 夜晚，缓慢消耗电量
                    ChangeEquipmentElectricity(equipment.MaxElectricity / 60);
                }
                Debug.Log("CurrentSanity:   " + currentSanity);
            }
        }
    }
}

