using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
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
    // Start is called before the first frame update
    void Start()
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

        //target = waypoints[1].position;
        attack = _attackDamage;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
