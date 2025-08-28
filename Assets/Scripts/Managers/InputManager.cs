using Enums;
using Managers;
using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{   
    public static InputManager Instance;
   
    //Metoda urachaminia przy starcie 
    private void Awake()
    {
        if(Instance==null)
          Instance = this;
        else
            Destroy(Instance);

    }

    public void Initialize()
    {

    }

    public void CustomUpdate()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            GameManager.Instance.AddPoints(1);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Flipping the screen
        }   
        if (Input.GetKeyDown(KeyCode.A))
        {
            //Obraca bronie wogol statku  przy pomocy a/d lub prawo lewo
            float dir = Input.GetAxis("Horizontal");
        }

        if(GameManager.Instance.GameState == GameState.Paused)
        {
            for (int i = 0; i < UpgradeManager.Instance.CurrentUpgrades.Count; i++)
            {
                if (Input.GetKeyDown(UpgradeManager.Instance.CurrentUpgrades[i].inputKeyCode))
                {
                    UpgradeManager.Instance.CurrentUpgrades[i].TakeButton.onClick.Invoke();
                }
            }
        }
        else
        {
            //Prze³acza sie miedzy broniami 
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                ChangeWeapon(0);
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                ChangeWeapon(1);
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                ChangeWeapon(2);
            }

            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                ChangeWeapon(3);
            }

            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                ChangeWeapon(4);
            }
        }

    }

    private void ChangeWeapon(int index)
    {
        if (Ship.Instance.weapons.Count > index)
        {
            //Ship.Instance.weapons[Ship.Instance.currentWeapon].Renderer.color = new Color(10f / 255f, 60f / 255f, 190f / 255f);
            Ship.Instance.currentWeapon = index;
            //Ship.Instance.weapons[Ship.Instance.currentWeapon].Renderer.color = new Color(175f / 255f, 10f / 255f, 200f / 255f);
        }
    }
}
