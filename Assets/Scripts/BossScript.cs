using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossScript : MonoBehaviour
{
    Rigidbody2D rb;
    bool hasBeenHit;
    public Player player;
    public float hitForce;


    private float[] momentPoints = {223.6724f, 340.1221f, 451.6789f,
    680.0065f, 788.3334f, 905.0986f, 1149.975f, 1253.297f, 1360.02f,
    1581.616f, 1694.932f, 1804.93f, 1914.954f, 2019.941f, 2133.276f,
    2249.99f, 2704.898f, 2818.19f, 2921.54f, 3144.889f, 3253.196f,
    3371.509f, 3481.49f, 3589.845f, 3689.809f, 3814.801f, 4254.771f,
    4363.117f, 4476.429f, 4684.78f, 4796.419f, 4918.079f, 5081.41f,
    5216.416f, 5319.727f, 5433.038f, 5548.097f, 5669.702f, 5783.028f,
    5896.355f, 6133.276f, 7081.307f, 7192.958f, 7301.272f, 7521.28f,
    7634.861f, 7741.312f, 7842.908f, 7953.012f, 8067.902f, 8191.209f,
    8624.614f, 8734.511f, 8849.517f, 9061.188f, 9171.188f, 9312.838f,
    9417.897f, 9513.202f, 9626.201f, 9731.188f, 9839.459f, 9951.089f,
    10052.79f, 10169.43f, 10294.42f, 10394.41f, 10499.45f, 10601.13f,
    10717.74f, 10826.05f, 10942.75f, 11064.39f, 11222.75f, 11369.36f,
    11471.02f, 11569.36f, 11659.35f, 11995.98f, 12106.02f, 12212.75f,
    12434.29f, 12544.28f, 12652.62f, 12752.62f, 12859.31f, 12965.99f,
    13080.98f, 13527.57f, 13632.54f, 13744.2f, 13989.2f, 14124.24f,
    14354.18f};

    float currentMoment;
    int oncomingPoint;
    public float dashSpeed;

    public Transform hitPoint;
    public float hitRadius;
    public LayerMask hitLayer;
    bool hasHit;
    public float rotationMult;

    public GameObject[] pianos;

    UnityEngine.Random random;

    public float attackTime;
    public float attackTimer;
    bool isAttacking;

    int health;
    public int damageOnHit;
    bool hasStarted;


    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        oncomingPoint = 0;
        random = new UnityEngine.Random();
        health = 100;

    }

    // Update is called once per frame
    void Update()
    {
        if (hasStarted)
        {
            currentMoment += Time.unscaledDeltaTime * 100;
        }
        if (oncomingPoint >= momentPoints.Length)
        {
            hasStarted = false;
            oncomingPoint = momentPoints.Length;
        }
        if (hasStarted)
        {
            if (currentMoment >= momentPoints[oncomingPoint])
            {
                StopAttacking();
                DashToPlayer();
            }
        }
        if (!isAttacking)
        {
            LookAtPlayer();
        }

        if (isAttacking)
        {
            attackTimer += Time.deltaTime * 100;
            if (attackTimer > attackTime && !hasHit)
            {
                Attack();
            }
            if (attackTimer > attackTime * 5)
            {
                StopAttacking();
            }
        }
        if (health <= 0)
        {
            // insert win state
            SceneManager.LoadScene("Win");
        }
    }
    public void GetHit()
    {
        rb.velocity = (transform.position - player.transform.position) /
            Vector2.Distance(player.transform.position, transform.position) * hitForce;
    }

    void LookAtPlayer()
    {
        Vector3 dir = player.transform.position - transform.position;
        float targetAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(targetAngle, new Vector3(0, 0, 1));
    }

    /// <summary>
    /// dashes the boss towards the player and starts the attack animation
    /// </summary>
    void DashToPlayer()
    {
        LookAtPlayer();
        rb.AddForce(transform.right * dashSpeed);
        oncomingPoint++;
        isAttacking = true;
        StartAnimation();
    }

    void Attack()
    {
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(hitPoint.position, hitRadius, hitLayer);

        for (int i = 0; i < hitPlayer.Length; i++)
        {
            hitPlayer[i].GetComponent<Player>().GetHit();
        }
        hasHit = true;
        MoveToFrame(1);
    }
    void StopAttacking()
    {
        isAttacking = false;
        attackTimer = 0;
        MoveToFrame(0);
        hasHit = false;
    }

    void StartAnimation()
    {
        MoveToFrame(0);
        attackTimer = 0;
    }
    void MoveToFrame(int frame)
    {
        for (int i = 0; i < pianos.Length; i++)
        {
            pianos[i].GetComponent<Renderer>().enabled = false;
        }
        pianos[frame].GetComponent<Renderer>().enabled = true;
    }
    public int Gethealth()
    {
        return health;
    }
    public void GetDamaged(int damage)
    {
        health -= damage;
    }

    public void ResetMusic()
    {
        oncomingPoint = 0;
        currentMoment = 0;
        hasStarted = true;
    }
}
