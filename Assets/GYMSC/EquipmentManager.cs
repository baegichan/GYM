using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 누가 코드 수정할지 모르겠지만 UnUseble  Useble갱신 오류있음
/// </summary>
public class EquipmentManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static EquipmentManager s_instance;


    public List<Equipment> EquipsList = new List<Equipment>();


     List<Equipment> Useble = new List<Equipment>();
     List<Equipment> UnUseble = new List<Equipment>();


    public List<Equipment> EquipsP
    {
        get {
            return EquipsList;
        }
    }
    public void instance()
    {
        if (s_instance == null)
        {
            s_instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Awake()
    {
        instance();
    }


    public void SetEquipment(Equipment.Equipments equipment, Equipment.Equipments TargetEquipment)
    {
        equipment = TargetEquipment;
    }
    public Equipment TryGetRandomEquip()
    {
        UpdateLists();
        if (Useble.Count != 0)
        {
            return Useble[Random.Range(0, Useble.Count)];
        }
        else
        {

            Debug.Log("사용가능한 기구 없음");
            return null;
        }

    }
    public void UpdateLists()
    {
        if (s_instance.EquipsP.Count != 0)
        {

            Useble.Clear();
            UnUseble.Clear();
            foreach (Equipment i in EquipsP)
            {
                if (i.UsingPermission() == true)
                {

                    Useble.Add(i);
                }
                else
                {

                    UnUseble.Add(i);
                }
            }
        }       
    }

    public bool CheckEquipments()
    {
        if (s_instance.EquipsP.Count != 0)
        {
            int Usingcount = 0;
            foreach (Equipment i in EquipsP)
            {
                if (i.UsingPermission() != true)
                {
                    Usingcount += 1;
                }
                else
                {
                    return true;
                }
            }
            if(Usingcount == s_instance.EquipsP.Count)
            {
                return false;
            }
           
        }
        return false;
    }
}


