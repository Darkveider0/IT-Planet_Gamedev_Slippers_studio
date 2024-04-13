using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Runtime.InteropServices.WindowsRuntime;
using System.ComponentModel.Design;

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
    public int xp;
    public int xp_max;
    public int level;
    public Text test_textbox;
    public int damage;
    public float invisibilty_frames;
    float last_damage;
    public static Player Instance { get; set; }
    //Стрельба
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10;
    public float shooting_cooldown;
    float last_shot;
    float last_ground;
    //GameObject Gun;
    //Movement
    public float speed;
    public float jump;
    float moveVelocity;
    //Grounded Vars
    public bool has_double_jump;
    bool grounded = true;
    bool double_jump = false;
    bool facingRight = true;
    bool has_taken_damage = false;
    bool is_on_enemy = false;
    public bool near_save_point = false;
    //For testing
    public Text level_textbox;

    public TrapController trapController;
    private ITrap trapStalagmites;
    public bool move = false;
    public GameObject losePanel;

    void Start()
    {
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
        xp_max = 15 + level * 5;
        max_health = 20 + level * 5;
        damage = 1 + level;
        health = max_health;
        trapStalagmites = new TrapStalagmites();
        //LoadPlayer();
    }


    void Update()
    {
        //test_textbox.text = CheckIfAtSavePoint().ToString();

        //Анимации

        if (move)
        {

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
            if (grounded == false)
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

            if (Input.GetKeyDown(KeyCode.E) && CheckIfAtSavePoint())
            {
                SavePlayer();
            }

            //Если игрок всё ещё касается с врагом
            if (is_on_enemy && Time.time - last_damage >= invisibilty_frames)
            {
                has_taken_damage = true;
                last_damage = Time.time;
                GetDamage(EnemyMove.attack);
            }

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
            if (!has_taken_damage)
            {
                //Jumping
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.W))
                {
                    if (grounded)
                    {
                        GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jump);
                        grounded = false;
                        last_ground = Time.time;
                    }
                    else if (Time.time - last_ground <= 0.1 && !grounded)
                    {
                        GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jump);
                    }
                    else if (double_jump)
                    {
                        GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jump);
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
                GetComponent<Rigidbody2D>().velocity = new Vector2(moveVelocity, GetComponent<Rigidbody2D>().velocity.y);
            }
            else
            {
                //GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity;// + new Vector2(0, -(float)0.1);
            }

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
        /*if (collision.gameObject.tag == "SavePoint")
        {
            near_save_point = true;
        }*/
        /*if (collision.gameObject.tag == "Ground")
        {
            grounded = true;
        }*/
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            is_on_enemy = false;
        }
        /*if(collision.gameObject.tag == "SavePoint")
        {
            near_save_point = false;
        }*/
    }
    //проверка, на земле ли
    void OnTriggerEnter2D(Collider2D other)
    {
        has_taken_damage = false;
        grounded = true;
        if (has_double_jump) double_jump = true;

        if (other.tag == "Stalagmites")
        {
            trapStalagmites.FallIntoTrap(trapController);
            if (health <= 0) Die();
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        last_ground = Time.time;
        grounded = false;

        if (other.tag == "Stalagmites")
        {
            grounded = true;
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
        //Health healthBar = new Health();
        //healthBar.SetHealth(health, max_health);
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
        this.xp += expirience;
        if (this.xp >= xp_max)
        {
            this.xp -= xp_max;
            level++;
            //Level lvl = new Level();
            //lvl.SetLevel(level);
            level_textbox.text = level.ToString();
            damage++;
            max_health += 5;
            xp_max += 5;
            health = max_health;
        }
    }
    public void Die()
    {
        //Окончание игры
        //gameObject.SetActive(false);
        losePanel.SetActive(true);
        Lose.Instance.Defeat();
    }

    public void SavePlayer()
    {
        SaveSysteam.SavePlayer(this);
    }
    public void LoadPlayer()
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
    }
}