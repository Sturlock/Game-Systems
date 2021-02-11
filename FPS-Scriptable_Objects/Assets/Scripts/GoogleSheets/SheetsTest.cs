using LightJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheetsTest : MonoBehaviour
{
    public Weapon_sObj GermOBlaster;
    public Weapon_sObj HealMatic500;
    public Weapon_sObj HealOMatic501;
    public Weapon_sObj MedSpreader;
    public Weapon_sObj Pill;
    public GoogleSheetsManager sheetManager = null;
    void Start()
    {
        sheetManager.GetSheets(OnGetSheets);
    }

   private void OnGetSheets(bool success, JsonObject sheets)
    {
        if (success)
        {
            Debug.Log("Something happens");
            Debug.Log(sheets.ToString(true));
            JsonObject weapons = sheets["Weapon"];
            if (weapons != null)
            {
                GermOBlaster.FromJson(weapons["Weapon"]);
                HealMatic500.FromJson(weapons["Weapon"]);
                HealOMatic501.FromJson(weapons["Weapon"]);
                MedSpreader.FromJson(weapons["Weapon"]);
                Pill.FromJson(weapons["Weapon"]);
                PillProjectile.FromJson(weapons["Weapon"]);
            }
        }
        else
        {
            Debug.LogWarning("It Borked");
        }
}
}
    
