using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Equipment : MonoBehaviour
{
    public bool Using;
    public Guest UsingGuest=null;
    public int LoopCount = 0;


    bool pre_gender = false;
    bool current_gender = false;


    public Animator ani;
    private void Awake()
    {
        if(GetComponent<Animator>()!=null)
        {
            ani = GetComponent<Animator>();
        }
    }
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
        if(UsingGuest.Guest_Gender == Guest.Gender.M)
        {
            if (GetComponent<MeshChanger>() != null)
            {
                GetComponent<MeshChanger>().Change(UsingGuest.Guest_Gender);
            }
            ani.SetBool("Gender", true);
            ani.SetBool("Use", true);
         
        }
        else
        {
            if (GetComponent<MeshChanger>() != null)
            {
                GetComponent<MeshChanger>().Change(UsingGuest.Guest_Gender);
            }
            ani.SetBool("Gender", false);
            ani.SetBool("Use", true);
        }
        
        Count += 1;
        Using = true;
    }
    public void UnUse()
    {
        UsingGuest = null;
        Using = false;
        ani.SetBool("Gender", false);
        ani.SetBool("Use", false);
    }
    #region animation Event
    public void SetRandomLoop()
    {
        LoopCount = Random.Range(5, 10);
    }
    public void UpCount()
    {
        ani.SetInteger("LoopCount", ani.GetInteger("LoopCount") + 1);
    }
    public void ClearLoop()
    {
        ani.SetInteger("LoopCount", 0);
        LoopCount = 0;
    }
    #endregion

    public void Gender_check()
    {
        if(pre_gender != current_gender)
        {
            

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
