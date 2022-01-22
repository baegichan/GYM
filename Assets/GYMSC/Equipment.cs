using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Equipment : MonoBehaviour
{
    public bool Using;
    public Guest UsingGuest=null;
    public int LoopCount = 0;
    private int AniLoopCount = 0;


    [Header("Guest MeshOBJ")]
    public GameObject Man_OBJ;
    public GameObject Women_OBJ;

    [Header("Guest Ani")]
    public Animator Man_ani;
    public Animator Women_ani;



    public enum TargetGender
    {
    M,
    W,
    Both
    }

    [Header("Equipment TargetGender")]
    public TargetGender target_gender;

    
    //언젠가 사용할거같은 미래를 위해 남겨둠
    int Count = 0;

  
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
        SetRandomLoop();
        switch(guest.Guest_Gender)
        {
            case Guest.Gender.M:
                Man_OBJ.SetActive(true);
                Man_ani.SetTrigger("Start");
                break;
            case Guest.Gender.W:
                Women_OBJ.SetActive(true);
                Women_ani.SetTrigger("Start");
                break;
        }
        Count += 1;
        Using = true;
    }
    public void UnUse( )
    {
        UsingGuest.ActiveOn();
        UsingGuest.Target = null;
        UsingGuest = null;
        Using = false;
    
        if (Man_OBJ!=null)
        {
            Man_OBJ.SetActive(false);
        }
 
        if (Man_ani!=null) 
        {
            Man_ani.SetInteger("LoopCount", 0);
        }

        if (Women_OBJ!= null)
        {
            Women_OBJ.SetActive(false);
        }

        if (Women_ani!= null)
        {
            Women_ani.SetInteger("LoopCount", 0);
        }


    
        ClearLoop();
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
        LoopCount = 0;
        AniLoopCount = 0;
    }
    public void Check()
    {
        if(AniLoopCount == LoopCount)
        {
            switch (UsingGuest.Guest_Gender)
            {
                case Guest.Gender.M:                  
                    Man_ani.SetTrigger("Escape");
                    break;
                case Guest.Gender.W:
                    Women_ani.SetTrigger("Escape");
                    break;
            }
            UnUse();
            EquipmentManager.s_instance.UpdateLists();
        }
    }
    #endregion
    void Start()
    {
        if(EquipmentManager.s_instance!=null)
        {
            EquipmentManager.s_instance.EquipsP.Add(this);
            EquipmentManager.s_instance.UpdateLists();
        }
    }
}
