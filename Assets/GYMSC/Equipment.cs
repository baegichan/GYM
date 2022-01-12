using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Equipment : MonoBehaviour
{
    public bool Using;
    public enum Equipments
    {
        None,
        Running,
        Dumbbel,
    }
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

   void Start()
    {
        if(EquipmentManager.s_instance!=null)
        {
            EquipmentManager.s_instance.EquipsP.Add(this);
            EquipmentManager.s_instance.UpdateLists();
        }
    }
}
