using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{//Базовый класс врага, от него наследуются остальные враги
    int moveSpeed;
    int attackDamage;
    int lifePoints;
    float attackRadius;
    float followRadius;
    
    //Вот эта часть это "патрулирование врага" 
    protected Vector3 target;
    protected Vector3 velocity;
    protected Vector3 previous_position;
    protected Animator animator;
    protected bool flipped;
    

    [SerializeField]
    protected Transform[] waypoints;

    public virtual void Start()
    {
        //animator.GetComponentInChildren<Animator>();
        target = waypoints[1].position;
    }

    public virtual IEnumerator SetTarget(Vector3 position)//враг остаётся на метке, на которую он шёл, 2 секунды, после этого идёт к следующей метке
    {
        yield return new WaitForSeconds(2f);
        target=position;
        FaceTowards(position - transform.position);
    }
    public virtual void FaceTowards(Vector3 direction)
    {
        if(direction.x < 0f)
        {
            transform.localEulerAngles = new Vector3(0, 180, 0);
            flipped = true;
        }
        else
        {
            transform.localEulerAngles = new Vector3(0, 0, 0);
            flipped = false;
        }
    }


    public void setMoveSpeed(int speed)
    {
        moveSpeed = speed;
    }

    public void setAttackDamage(int attdmg)
    {
        attackDamage = attdmg;
    }

    public void setLifePoints(int lp)
    {
        lifePoints = lp;
    }

    public int getMoveSpeed()
    {
        return moveSpeed;
    }

    public int getAttackDamage()
    {
        return attackDamage;
    }

    public int getLifePoints()
    {
        return lifePoints;
    }


    //movement toward a player
    public void setFollowRadius(float r)
    {
        followRadius = r;
    }
    //attack radius 
    public void setAttackRadius(float r)
    {
        attackRadius = r;
    }

    //if player in radius move toward him 
    public bool checkFollowRadius(float playerPosition, float enemyPosition)
    {
        if (Mathf.Abs(playerPosition - enemyPosition) < followRadius)
        {
            //player in range
            return true;
        }
        else
        {
            return false;
        }
    }

    //if player in radius attack him
    public bool checkAttackRadius(float playerPosition, float enemyPosition)
    {
        if (Mathf.Abs(playerPosition - enemyPosition) < attackRadius)
        {
            //in range for attack
            return true;
        }
        else
        {
            return false;
        }
    }
}
