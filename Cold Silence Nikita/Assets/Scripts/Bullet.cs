using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public static int attack = 3;
    

    void Awake()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //исчезновение объекта при касании
        //Destroy(collision.gameObject);

        //исчезновение пули при касании
        Destroy(gameObject);
    }
}
