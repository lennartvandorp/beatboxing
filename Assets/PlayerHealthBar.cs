using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    public Slider playerHealthBar;

    public GameObject player;
    Player playerScript;

    void Start(){
        playerScript = player.GetComponent<Player>();
    }
    void Update(){
        int health = playerScript.GetHealth();
        playerHealthBar.value = health;
    }

}
