using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapRadius : MonoBehaviour
{
    public float snapRadius;
    public Transform snapCenter;
    public LayerMask playerLayer;
    bool playerInSnapRange;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Refereert naar de boss die weer naar de speler refereert. niet erg elegant. 
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(snapCenter.transform.position, snapRadius, playerLayer);
        for (int i = 0; i < hitPlayer.Length; i++)
        {
            hitPlayer[i].GetComponent<BossScript>().player.CanSnap();
        }

    }

    public bool getPlayerInSnapRange() {
        return playerInSnapRange;
    }
}
