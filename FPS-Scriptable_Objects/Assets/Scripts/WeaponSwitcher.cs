using UnityEngine;
using UnityEngine.UI;

public class WeaponSwitcher : MonoBehaviour
{
    public GameObject startCanvas;

    public PlayerWeapon_sObj loadout1;
    public PlayerWeapon_sObj loadout2;
    public PlayerWeapon_sObj loadout3;


    public void ChooseLoadout1()
    {
        GameLoadouts.loadout = loadout1;
    }

    public void ChooseLoadout2()
    {
        GameLoadouts.loadout = loadout2;
    }

    public void ChooseLoadout3()
    {
        GameLoadouts.loadout = loadout3;
    }
    public void NextMenu(GameObject nxtCanvas)

    {
        nxtCanvas.SetActive(true);
        startCanvas.SetActive(false);
    }
}