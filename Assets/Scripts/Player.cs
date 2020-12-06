using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float acceleration;
    private Vector2 force;
    private Vector2 direction;
    public float bpm;//The beats per minute of this song

    [SerializeField]
    float beatDuration;

    Rigidbody2D rb;
    public float drag;
    public float dashAcc;

    public float hitPauzeTime;
    float moveMult = 1000f;

    public Transform attackPos;
    public float attackRange;
    public LayerMask bossLayer;
    public LayerMask pianoLayer;

    public ParticleSystem particles;
    public ParticleSystem hitParticles;
    public ParticleSystem teleParticles;
    public ParticleSystem dashParticles;
    public ParticleSystem blood;

    public GameObject boss;

    public float dashCooldownLength;
    float dashCooldownTimer;

    bool playBlood;

    int health;

    public GameObject audioManager;

    public GameObject gameManager;
    HitPauze hitPauze;
    bool doHitPauze;

    public float beatsBetweenHits;
    bool canHit;
    float canHitTimer;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        health = 100;
        hitPauze = gameManager.GetComponent<HitPauze>();
        beatDuration = 60 / bpm;
    }

    // Update is called once per frame
    void Update()
    {
        if (doHitPauze)
        {
            hitPauze.Freeze(hitPauzeTime);
            doHitPauze = false;
        }


        //rotates the player to the mouse
        Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);


        //sets the player up to be moved
        if (Input.GetKey(KeyCode.W))
        {
            direction.y = 1;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            direction.y = -1;
        }
        else { direction.y = 0; }

        if (Input.GetKey(KeyCode.A))
        {
            direction.x = -1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            direction.x = 1;
        }
        else { direction.x = 0; }

        if (Input.GetKeyDown(KeyCode.Space) && dashCooldownTimer > dashCooldownLength)
        {
            Dash();
        }
        dashCooldownTimer += Time.deltaTime * 100;

        if (playBlood)
        {
            blood.Play();
            playBlood = false;
        }


        //moves the player
        force = Vector2.ClampMagnitude(direction, 1);
        rb.AddForce(force * acceleration * Time.deltaTime * moveMult);
        rb.drag = drag;

        //handles the highest and lowest values of the health
        if (health > 100) { health = 100; }
        if (health <= 0) { SceneManager.LoadScene("Fail"); }

        //Timer in between hits from the player
        if (!canHit)
        {
            canHitTimer -= Time.unscaledDeltaTime;
            if (canHitTimer < 0)
            {
                canHit = true;
            }
        }

    }


    void Dash()
    {
        rb.AddForce(force * dashAcc * moveMult);
        dashCooldownTimer = 0;
        dashParticles.Play();
        audioManager.GetComponent<AudioManager>().Play("Dash");
    }

    public void GetHit()
    {

        if (!GetComponent<BatAnimationScript>().GetBlocking())
        {
            particles.GetComponent<ParticleSystem>().Play();
            ChangeHealth(3);
        }
        else
        {
            hitParticles.GetComponent<ParticleSystem>().Play();
            ChangeHealth(-9);
            audioManager.GetComponent<AudioManager>().Play("HitPlayer");
            doHitPauze = true;
        }

    }

    public void CanSnap()
    {
        RaycastHit2D hitPiano = Physics2D.Raycast(attackPos.position, boss.transform.position - attackPos.position);
        Color color;
        bool cantHitPiano = true;
        if (hitPiano.collider.name == "Piano")
        {
            color = new Color(1, 0, 0);
            cantHitPiano = false;
        }
        else { color = new Color(0, 1, 0); }

        Debug.DrawRay(attackPos.position, boss.transform.position - attackPos.position, color);


        if (Input.GetKeyDown(KeyCode.Mouse0) && canHit)
        {
            GetComponent<BatAnimationScript>().StartAnimation();
            teleParticles.Play();
            rb.MovePosition(FindSnapPoint());
            canHit = false;
            canHitTimer = beatDuration * beatsBetweenHits;
            if (cantHitPiano)
            {
                boss.GetComponent<BossScript>().GetDamaged(4);
                audioManager.GetComponent<AudioManager>().Play("HitSkin");
                playBlood = true;
                doHitPauze = true;
            }
            else { audioManager.GetComponent<AudioManager>().Play("KnockOnWood"); }
        }
        if (health <= 0)
        {
            //insert loss state
            Debug.Log("Loss");
        }
    }
    Vector2 FindSnapPoint()
    {
        Vector2 snapPoint;
        Vector2 dir, dirNorm;
        dir = boss.transform.position - transform.position;
        dirNorm = dir / dir.magnitude;
        snapPoint = new Vector2(boss.transform.position.x, boss.transform.position.y) - 2 * dirNorm;
        return snapPoint;
    }

    public void ChangeHealth(int value)
    {
        health += value;
    }
    public int GetHealth()
    {
        return health;
    }
}
