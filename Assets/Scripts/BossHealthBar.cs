using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    public Slider bossHealthBar;

    public GameObject boss;
    BossScript bossScript;

    void Start(){
        bossScript = boss.GetComponent<BossScript>();
    }
    void Update(){
        int health = bossScript.Gethealth();
        bossHealthBar.value = health;
        
    }

}
