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
        //������������ ������� ��� �������
        //Destroy(collision.gameObject);

        //������������ ���� ��� �������
        Destroy(gameObject);
    }
}
