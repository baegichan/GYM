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
     List<Equipment> Man_Useble = new List<Equipment>();
    List<Equipment> Women_Useble = new List<Equipment>();
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


    
    public Equipment TryGetRandomEquip(Guest.Gender GuestGender)
    {
        UpdateLists();

        switch (GuestGender)
        {
            case Guest.Gender.M:
                if (Man_Useble.Count != 0)
                {
                    return Man_Useble[Random.Range(0, Man_Useble.Count)];
                }
                else
                {

                    return null;
                }
               
            case Guest.Gender.W:
                if (Women_Useble.Count != 0)
                {
                    return Women_Useble[Random.Range(0, Women_Useble.Count)];
                }
                else
                {        
                    return null;
                }
        
        }

        return null;

    }
    public void UpdateLists()
    {
        if (s_instance.EquipsP.Count != 0)
        {

            Man_Useble.Clear();
            Women_Useble.Clear();
            UnUseble.Clear();
            foreach (Equipment i in EquipsP)
            {
                if (i.UsingPermission() == true)
                {
                    if(i.target_gender==Equipment.TargetGender.M)
                    {
                        Man_Useble.Add(i);
                    }
                    else if(i.target_gender == Equipment.TargetGender.W)
                    {
                        Women_Useble.Add(i);
                    }
                    else
                    {
                        //both
                        Man_Useble.Add(i);
                        Women_Useble.Add(i);
                    }
                    
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


