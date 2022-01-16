using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Equipment : MonoBehaviour
{
    public bool Using;
    public Guest UsingGuest=null;
    public enum Equipments
    {
        None,
        Running,
        Dumbbel,
    }

    //언젠가 사용할거같은 미래를 위해 남겨둠
    int Count = 0;

    public Equipments equipment_type;

    public bool UsingPermission()
    {
        if(Using)
        {
            return false;
        }
        else
        { 
            return true;
        }
    }

    public void Use(Guest guest)
    {
        UsingGuest = guest;
        Count += 1;
        Using = true;
    }
    public void UnUse()
    {
        UsingGuest = null;
        Using = false;
    }
   void Start()
    {
        if(EquipmentManager.s_instance!=null)
        {
            EquipmentManager.s_instance.EquipsP.Add(this);
            EquipmentManager.s_instance.UpdateLists();
        }
    }
}
