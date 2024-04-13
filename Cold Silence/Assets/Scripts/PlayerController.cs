using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Runtime.InteropServices.WindowsRuntime;
using System.ComponentModel.Design;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;

    //Анимации
    public Animator animator;

    //ходьба
    private float Horizontal_Move = 0f;
    private bool FacingRight = true;

    public int max_health;
    public int health;
    
    public Text test_textbox;
    public int damage;
    public float invisibilty_frames;
    float last_damage;
    public static Player Instance { get; set; }
    //Стрельба
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed;
    public float shooting_cooldown;

    public float dashing_cooldown;
    public float dash_length;
    public float dash_speed_multiplier;

    float last_shot;
    float last_ground;
    float dash_start;

    //Movement
    public float speed;
    public float jump;
    float moveVelocity;

    public bool has_double_jump;
    public bool has_walljump;
    public bool has_dash;
    public bool grounded = true;
    bool double_jump = false;
    bool facingRight = true;
    bool has_taken_damage = false;
    bool is_on_enemy = false;
    bool is_on_wall = false;
    public bool is_in_menu = false;
    bool is_dashing = false;
    //public bool near_save_point = false;
    //For testing
    public Text level_textbox;

    BoxCollider2D groundTrigger;
    BoxCollider2D wallTrigger;
    void Start()
    {
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
        //xp_max = 15 + level * 5;
        //max_health = 20 + level * 5;
        //damage = 1 + level;
        health = max_health;
        //LoadPlayer();
        var colliders = GetComponents<BoxCollider2D>();//для загрузки триггеров
        groundTrigger = colliders[0];
        wallTrigger = colliders[1];
    }


    void Update()
    {
        //test_textbox.text = CheckIfAtSavePoint().ToString();

        //Анимации


        if (grounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(transform.up * jump, ForceMode2D.Impulse);
        }

        //по этому гайду подключались анимации https://www.youtube.com/watch?v=L5k9t7ug2r8
        if (facingRight)
            Horizontal_Move = Input.GetAxisRaw("Horizontal") * speed;
        else
            Horizontal_Move = Input.GetAxisRaw("Horizontal") * -speed;
        animator.SetFloat("Horizontal_Move", Mathf.Abs(Horizontal_Move));
        if (!grounded && !is_on_wall)
        {
            animator.SetBool("Jump", true);
        }
        else
        {
            animator.SetBool("Jump", false);
        }

        //animator.SetFloat("Horizontal_Move", Mathf.Abs(Horizontal_Move));
        if (Horizontal_Move < 0 && FacingRight)
        {
            Flip();
        }
        else if (Horizontal_Move > 0 && !FacingRight)
        {
            Flip();
        }

        /*if (Input.GetKeyDown(KeyCode.E) && CheckIfAtSavePoint())
        {
            SavePlayer();
        }*/

        //Если игрок всё ещё касается с врагом
        if (is_on_enemy && Time.time - last_damage >= invisibilty_frames)
        {
            has_taken_damage = true;
            last_damage = Time.time;
            GetDamage(EnemyMove.attack);
        }
        if (is_dashing)
        {
            //Debug.Log(Time.time - dash_start);
            if (Time.time - dash_start <= dash_length)
            {
                if (facingRight)
                {
                    rb.velocity = new Vector2(speed * dash_speed_multiplier, 0);
                }
                else
                {
                    rb.velocity = new Vector2(-speed * dash_speed_multiplier, 0);
                }
            }
            else
            {
                is_dashing = false;
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
        }
        if (!is_in_menu)
        {
            //shooting
            if (Input.GetMouseButtonDown(0))
            {
                if (Time.time - last_shot >= shooting_cooldown)
                {
                    last_shot = Time.time;
                    var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                    Bullet.attack = damage;
                    if (facingRight)
                    {
                        bullet.GetComponent<Rigidbody2D>().velocity = bulletSpawnPoint.right * bulletSpeed;

                    }
                    else
                    {
                        bullet.GetComponent<Rigidbody2D>().velocity = bulletSpawnPoint.right * -bulletSpeed;
                        bullet.GetComponent<Rigidbody2D>().transform.localScale = new Vector3(-(float)0.2, (float)0.2, 1); ;
                    }
                }
            }

            if (!has_taken_damage && !is_dashing)
            {
                //Dashing
                if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.X))
                {
                    //Debug.Log("Нажат шифт");
                    if (Time.time - dash_start - dash_length >= dashing_cooldown)
                    {
                        rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                        dash_start = Time.time;
                        is_dashing = true;
                    }
                }
                //Jumping
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.W))
                {
                    if (is_on_wall && has_walljump)
                    {
                        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                        rb.velocity = new Vector2(rb.velocity.x, jump);
                        is_on_wall = false;
                    }
                    if (grounded)
                    {
                        rb.velocity = new Vector2(rb.velocity.x, jump);
                        grounded = false;
                        last_ground = Time.time;
                    }
                    else if (Time.time - last_ground <= 0.1 && !grounded)
                    {
                        rb.velocity = new Vector2(rb.velocity.x, jump);
                    }
                    else if (double_jump)
                    {
                        rb.velocity = new Vector2(rb.velocity.x, jump);
                        double_jump = false;
                    }
                }

                moveVelocity = 0;

                //Left Right Movement

                if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
                {
                    moveVelocity = -speed;
                    if (facingRight)
                    {
                        Flip();
                    }
                }
                if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
                {
                    moveVelocity = speed;
                    if (!facingRight)
                    {
                        Flip();
                    }
                }

                if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)))
                {
                    moveVelocity = 0;
                }


                rb.velocity = new Vector2(moveVelocity, rb.velocity.y);

            }
        }
        else//Если Игрок в меню
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            animator.SetFloat("Horizontal_Move", 0);
            animator.SetBool("Jump", false);
        }
    }
    //касание разных объектов
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            is_on_enemy = true;
            if (Time.time - last_damage >= invisibilty_frames)
            {
                has_taken_damage = true;
                last_damage = Time.time;
                GetDamage(EnemyMove.attack);
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            is_on_enemy = false;
        }
    }

    //проверка, на земле ли
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Ground"))
        {
            if (collider.IsTouching(groundTrigger))
            {
                has_taken_damage = false;
                grounded = true;
                if (has_double_jump) double_jump = true;
            }

            if(collider.IsTouching(wallTrigger))
            {
                is_on_wall = true;
                if (has_walljump)
                    rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            }
            
        }
    }
    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.CompareTag("Ground"))
        {
            if (collider.IsTouching(groundTrigger))
            {
                grounded = true;
            }
            if (collider.IsTouching(wallTrigger))
            {
                if (rb.velocity.y < 0)
                {
                    is_on_wall = true;
                    if (has_walljump)
                        rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                }
            }
        }
    }
    void OnTriggerExit2D(Collider2D collider)
    {
        
        if (collider.CompareTag("Ground"))
        {
            if (!collider.IsTouching(groundTrigger))
            {
                last_ground = Time.time;
                grounded = false;
            }
            if (!collider.IsTouching(wallTrigger))
            {
                is_on_wall = false;
                if (has_walljump)
                    rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
        }
    }
    void Flip()
    {
        if (facingRight)
        {
            transform.localScale = new Vector3(-(float)0.5, (float)0.5, 1);
        }
        else
        {
            transform.localScale = new Vector3((float)0.5, (float)0.5, 1);
        }
        facingRight = !facingRight;
    }
    private void GetDamage(int damage)
    {
        health -= damage;
        if (health <= 0) Die();
        if (facingRight)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-speed / 2, jump / 2);
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(speed / 2, jump / 2);
        }

    }
    public void GetXP(int expirience)
    {
        PlayerLevelManager.xp += expirience;
        while(PlayerLevelManager.xp >= PlayerLevelManager.xp_max)
        //if (PlayerLevelManager.xp >= PlayerLevelManager.xp_max)//Новый уровень
        {
            PlayerLevelManager.NewLevel();
            Level.SetLevel();
            
        }
    }
    private void Die()
    {
        //Окончание игры
        //gameObject.SetActive(false);
    }

    /*public void SavePlayer()
    {
        SaveSysteam.SavePlayer(this);
    }*/
    /*public void LoadPlayer()//это планировалось сохранение прогресса
    {
        PlayerData data = SaveSysteam.LoadPlayer();

        //загружаемые значения
        level = data.level;
        level_textbox.text = level.ToString();
        xp = data.xp;

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        transform.position = position;
    }
    public void newPlayer()
    {
        SaveSysteam.newPlayer(this);
    }
    private bool CheckIfAtSavePoint()
    {
        GameObject[] savepoints = GameObject.FindGameObjectsWithTag("SavePoint");
        for (int i = 0; i < savepoints.Length; i++)
        {
            if (Mathf.Abs(transform.position.x - savepoints[i].transform.position.x) < 1) return true;
        }
        return false;
    }*/
}