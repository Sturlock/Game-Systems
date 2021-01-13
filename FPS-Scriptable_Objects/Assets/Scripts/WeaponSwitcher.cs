using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSwitcher : MonoBehaviour
{
    public Controller Player;
    [Header("Weapons")]
    public Weapon Healmatic500;
    public Weapon GermOBlaster;
    public Weapon MedSpeader;
    public Weapon Healmatic501;
    public Weapon Pill;
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            return;
        }
    }

    public void ObtainHealmatic500()
    {
        return;
    }
    public void ObtainGermOBlaster()
    {
        return;
    }
    public void ObtainMedSpeader()
    {
        return;
    }
    public void ObtainHealmatic501()
    {
        return;
    }
    public void ObtainPill()
    {
        return;
    }

}
