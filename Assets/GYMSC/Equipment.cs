using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Equipment : MonoBehaviour
{
    public bool Using;
    public Guest UsingGuest=null;
    public int LoopCount = 0;
    private int AniLoopCount = 0;
    public GameObject GuestOBJ;


    bool pre_gender = false;
    bool current_gender = false;


    public Animator ani;
  
    public enum Equipments
    {
        None,
        Running,
        Dumbbel,
        smith,

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
        UsingGuest.ActiveOff();
        GuestOBJ.SetActive(true);
        SetRandomLoop();
        ani.SetTrigger("Start");
        if (UsingGuest.Guest_Gender == Guest.Gender.M)
        {
            /*
            if (GetComponent<MeshChanger>() != null)
            {
                GetComponent<MeshChanger>().Change(UsingGuest.Guest_Gender);
            }
            ani.SetBool("Gender", true);
            ani.SetBool("Use", true);
         */
        }
        else
        {
            /*
            if (GetComponent<MeshChanger>() != null)
            {
                GetComponent<MeshChanger>().Change(UsingGuest.Guest_Gender);
            }
            ani.SetBool("Gender", false);
            ani.SetBool("Use", true);
            */
        }
        
        Count += 1;
        Using = true;
    }
    public void UnUse()
    {
        UsingGuest.ActiveOn();
        UsingGuest.Target = null;
        UsingGuest = null;
        Using = false;
        GuestOBJ.SetActive(false);
        ClearLoop();
        //ani.SetBool("Gender", false);
        //ani.SetBool("Use", false);
    }
    #region animation Event
    public void SetRandomLoop()
    {
        LoopCount = Random.Range(10, 16);
    }
    public void UpCount()
    {
        AniLoopCount += 1;
    }
    public void ClearLoop()
    {
        ani.SetInteger("LoopCount", 0);
        LoopCount = 0;
        AniLoopCount = 0;
    }
    public void Check()
    {
        if(AniLoopCount == LoopCount)
        {

            ani.SetTrigger("Escape");

            UnUse();
            EquipmentManager.s_instance.UpdateLists();
        }
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
