using UnityEngine;

public class Drone : MonoBehaviour
{
    public float followDistance = 2f;
    public float moveSpeed; 
    public GameObject bulletPrefab;
    public float bulletSpeed;
    public float shootingCooldown;
    private float lastShotTime;
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (player != null)
        {
            Vector2 directionToPlayer = player.transform.position - transform.position;
            float distanceToPlayer = directionToPlayer.magnitude;

            if (distanceToPlayer > followDistance)
            {
                transform.Translate(directionToPlayer.normalized * moveSpeed * Time.deltaTime);
            }

            if (Time.time - lastShotTime >= shootingCooldown)
            {
                Attack();
            }
        }
    }

    void Attack()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            Vector2 directionToEnemy = enemy.transform.position - transform.position;
            float distanceToEnemy = directionToEnemy.magnitude;

            if (distanceToEnemy <= 7f)
            {
                Shoot(enemy);
                break; 
            }
        }
    }

    void Shoot(GameObject enemy)
    {
        lastShotTime = Time.time;
        var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        Vector2 directionToEnemy = (enemy.transform.position - transform.position).normalized;

        float angle = Random.Range(-15f, 15f);
        Vector2 directionWithOffset = Quaternion.Euler(0, 0, angle) * directionToEnemy;

        bullet.GetComponent<Rigidbody2D>().velocity = directionWithOffset * bulletSpeed;
    }


}
