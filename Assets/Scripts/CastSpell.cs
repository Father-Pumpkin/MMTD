using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CastSpell : MonoBehaviour
{
    public Spell mySpell;
    public TextMeshProUGUI notEnoughManaText;
    public void castSpeedTurret()
    {
        SpeedTurrets(mySpell.increase, mySpell.manaCost);
    }
    public void SpeedTurrets(float SpeedIncrease, int manaCost)
    {
        if(PlayerStats.Mana < manaCost)
        {
            Color manaColor = notEnoughManaText.color;
            manaColor.a = 1f;
            notEnoughManaText.color = manaColor;
            return;
        }
        PlayerStats.Mana -= manaCost;
        
        StartCoroutine(SpeedTurretCoroutine(2, 2f));
        
        return;
    }

    IEnumerator SpeedTurretCoroutine(int speedIncrease, float timeToWait)
    {
        GameObject[] turrets = GameObject.FindGameObjectsWithTag("Turret");
        foreach (GameObject turret in turrets)
        {
            Turret myTurret = turret.GetComponent<Turret>();
            myTurret.fireRate *= 2;
            Debug.Log("Fire Rate is " + myTurret.fireRate + " at " + Time.deltaTime);
        }
        yield return new WaitForSecondsRealtime(timeToWait);
        foreach (GameObject turret in turrets)
        {
            Turret myTurret = turret.GetComponent<Turret>();
            myTurret.fireRate /= 2;
            Debug.Log("Fire Rate is " + myTurret.fireRate + " at " + Time.deltaTime);
        }
    }
}
