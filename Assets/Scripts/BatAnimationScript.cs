using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatAnimationScript : MonoBehaviour
{
    public GameObject[] bats;

    public float nextFrameTimeMs;
    bool animationBusy;
    float animationTimer;

    public LayerMask hitLayer;
    public float hitRadius;
    public Transform hitPoint;
    private bool canGetHit;

    float blockTimer;
    public float blockTime;


    // Start is called before the first frame update
    void Start()
    {
        MoveToFrame(0);
    }

    // Update is called once per frame
    void Update()
    {




        if (animationBusy) {
            if (animationTimer > nextFrameTimeMs) {
                HitBoss();
            }
            animationTimer += Time.deltaTime * 1000;
            if (animationTimer > nextFrameTimeMs * 3) {
                StopAnimation();
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse1)) {
            Block();
        }
        if (!canGetHit) {
            blockTimer += Time.deltaTime * 1000;
            if (blockTimer > blockTime) {
                stopBlocking();
            }
        }
    }

    public void StartAnimation() {
        MoveToFrame(1);
        animationBusy = true;
    }

    void StopAnimation() {
        MoveToFrame(0);
        animationBusy = false;
        animationTimer = 0;
    }

    void MoveToFrame(int frame)
    {
        for (int i = 0; i < bats.Length; i++) {
            bats[i].GetComponent<Renderer>().enabled = false;
        }
        bats[frame].GetComponent<Renderer>().enabled = true;
    }

    void HitBoss() {
        MoveToFrame(2);
        Collider2D[] bossToDamage = Physics2D.OverlapCircleAll(hitPoint.position, hitRadius, hitLayer);
        for (int i = 0; i < bossToDamage.Length; i++)
        {
            bossToDamage[i].GetComponent<BossScript>().GetHit();
        }
    }

    public Boolean GetBlocking() {
        return canGetHit;
    }

    void Block() {
        MoveToFrame(3);
        canGetHit = false;
    }
    void stopBlocking() {
        MoveToFrame(0);
        canGetHit = true;
        blockTimer = 0;
    }

    void GetHit() {
        if (canGetHit) { }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}
