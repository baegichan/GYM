using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(GuestMovement))]
public class Guest : MonoBehaviour
{
  

    public NavMeshAgent Agent;
    public Equipment Target;
    public Equipment UsingEquipment;

    public enum Gender
    {
        M,
        W
    }
    public Gender Guest_Gender;


    bool Stop = false;
    private Animator Ani;
    private int LoopCount = 0;
    private float View = 1;
    public Material material;
 
    private void Update()
    {
        if(Stop ==false)
        {
            if (Target != null)
            {
                 Agent.SetDestination(Target.transform.position);
            }
        }
    }
    private void OnEnable()
    {
        StartCoroutine(CheckRoutine(1));
    }
    private void Start()
    {
  
        if (GuestManager.s_instance!=null)
        {
            GuestManager.GuestCount += 1;
        }
        StartCoroutine(CheckRoutine(1));
    }

    public IEnumerator CheckRoutine(int time)
    {
        TargetCheck();
        yield return new WaitForSeconds(time);
        StartCoroutine(CheckRoutine(time));
    }
    public void TargetCheck()
    {
        if (Target == null)
        {
          if(EquipmentManager.s_instance!=null)
             {
                Target = EquipmentManager.s_instance.TryGetRandomEquip(Guest_Gender);
            }
          
        }
        else
        {
            if(Target.Using && Target.UsingGuest!=this)
            {
              Target = null;
            }
        }
    }
    private void Awake()
    {
        Ani = GetComponent<Animator>();
      //  material = GetComponent<Renderer>().material;
    }
    private void SetStep()
    {
        material.SetFloat("Step", View);
    }
    public void Off()
    {
        StartCoroutine(ViewOff());
    }
    public void On()
    {
        StartCoroutine(ViewOn());
    }
    IEnumerator ViewOn()
    {
        View = Mathf.Clamp(View += Time.deltaTime, 0, 1);
        SetStep();
        yield return new WaitForSeconds(Time.deltaTime);
        if (View != 1)
        {
            StartCoroutine(ViewOn());
        }
        else
        {
            StopCoroutine(ViewOn());
        }
    }
    IEnumerator ViewOff()
    {
        View = Mathf.Clamp(View -= Time.deltaTime, 0, 1);
        SetStep();
        yield return new WaitForSeconds(Time.deltaTime);
        if (View != 0)
        {
            StartCoroutine(ViewOff());
        }
        else
        {
            StopCoroutine(ViewOff());
        }
    }
    //use collisionenter
   
    private void OnTriggerStay(Collider other)
    {

        if(other.GetComponent<Equipment>()!=null&&other.GetComponent<Equipment>() == Target)
        {
        if(other.GetComponent<Equipment>().Using != true)
        {
                EquipmentUse(other.GetComponent<Equipment>());
            }
           
        }
    }
    public void EquipmentUse(Equipment Target)
    {
        Target.Use(this);
        gameObject.SetActive(false);
      
    }
    public void ActiveOn()
    {
        gameObject.SetActive(true);
    }
    public void ActiveOff()
    {
        gameObject.SetActive(false);
    }
    #region animation Event
    public void SetRandomLoop()
    {
        LoopCount = Random.Range(5, 10);
    }
    public void UpCount()
    {
        Ani.SetInteger("LoopCount", Ani.GetInteger("LoopCount")+1);
    }
    public void ClearLoop()
    {
        Ani.SetInteger("LoopCount", 0);
        LoopCount = 0;
    }
    #endregion
    public void EndUsing(Equipment Target)
    {
        Target.UnUse();
        //Animation

    }
}
