using System.Collections;
using System.Collections.Generic;
using System.Xml.XPath;
using UnityEngine;
using UnityEngine.UI;
public class EnemyMove : Enemy
{
    public int _moveSpeed;
    public int _attackDamage;
    public int _lifePoints;
    public float _attackRadius;
    public static int attack;
    public int xp;
    //public Text textbox;//для тестов
    //movement
    public float _followRadius;
    //end
    [SerializeField] Transform playerTransform;
    [SerializeField] Animator enemyAnim;
    SpriteRenderer enemySR;
    //public static EnemyMove Instance { get; set; }

    public override void Start()
    {
        //get the player transform   
        playerTransform = FindObjectOfType<Player>().GetComponent<Transform>();
        //enemy animation and sprite renderer 
        enemyAnim = gameObject.GetComponent<Animator>();
        enemySR = GetComponent<SpriteRenderer>();
        //set the variables
        setMoveSpeed(_moveSpeed);
        setAttackDamage(_attackDamage);
        setLifePoints(_lifePoints);
        setAttackRadius(_attackRadius);
        setFollowRadius(_followRadius);

        target = waypoints[1].position;
        attack = _attackDamage;
    }

    // Update is called once per frame
    void Update()
    {
        if (checkFollowRadius(playerTransform.position.x, transform.position.x))
        {
            //if player in front of the enemies
            if (playerTransform.position.x < transform.position.x)
            {
                transform.localEulerAngles = new Vector3(0, 180, 0);
                if (checkAttackRadius(playerTransform.position.x, transform.position.x))
                {
                    //for attack animation
                    enemyAnim.SetBool("Attack", true);
                    enemyAnim.SetBool("Walking", false);
                }
                else
                {
                    this.transform.position += new Vector3(-getMoveSpeed() * Time.deltaTime, 0f, 0f);
                    //for attack animation
                    enemyAnim.SetBool("Attack", false);
                    //walk
                    enemyAnim.SetBool("Walking", true);
                    enemySR.flipX = true;
                }

            }
            //if player is behind enemies
            else if (playerTransform.position.x > transform.position.x)
            {
                transform.localEulerAngles = new Vector3(0, 0, 0);
                if (checkAttackRadius(playerTransform.position.x, transform.position.x))
                {
                    //for attack animation
                    enemyAnim.SetBool("Attack", true);
                    enemyAnim.SetBool("Walking", false);
                }
                else
                {
                    this.transform.position += new Vector3(getMoveSpeed() * Time.deltaTime, 0f, 0f);
                    //for attack animation
                    enemyAnim.SetBool("Attack", false);
                    //walk
                    enemyAnim.SetBool("Walking", true);
                    enemySR.flipX = false;
                }


            }

        }
        else
        {
            velocity = ((transform.position - previous_position) / Time.deltaTime);
            previous_position = transform.position;
            if (playerTransform.position.x < transform.position.x)
            {
                enemySR.flipX = true;
            }
            else
            {
                enemySR.flipX = false;
            }


                enemyAnim.SetBool("Attack", false);
            //animator.SetFloat("speed", velocity.magnitude);
            //if (transform.position != target)
            bool at_waypoint = checkIfAtWaypoint(transform.position.x, target.x);
            //textbox.text = at_waypoint.ToString();
            if (!at_waypoint)
            {
                transform.position = Vector3.MoveTowards(transform.position, target, getMoveSpeed() * Time.deltaTime);
                enemyAnim.SetBool("Walking", true);
            }
            else
            {
                if (target == waypoints[0].position)
                {
                    if (flipped)
                    {
                        flipped = !flipped;
                    }
                    StartCoroutine("SetTarget", waypoints[1].position);
                }
                else
                {
                    if (!flipped)
                    {
                        flipped = !flipped;
                    }
                    StartCoroutine("SetTarget", waypoints[0].position);
                }
                enemyAnim.SetBool("Walking", false);
            }
        }


    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Bullet")
        {
            GetDamage(Bullet.attack);
        }


    }
    public bool checkIfAtWaypoint(float enemy_position, float waypoint_position)
    {
        if (Mathf.Abs(enemy_position - waypoint_position) < 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void GetDamage(int damage)
    {
        _lifePoints -= damage;
        if (_lifePoints <= 0) Die();
    }
    public virtual void Die()
    {
        //Player.Instance.xp += xp;
        Player.Instance.GetXP(this.xp);
        Destroy(this.gameObject);
    }
}