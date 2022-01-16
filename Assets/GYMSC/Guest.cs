using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(GuestMovement))]
public class Guest : MonoBehaviour
{
  
    public GuestState GuestState = new GuestState();
    public NavMeshAgent Agent;
    public Equipment Target;
    public Equipment PreTarget;
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
    Vector3 Preposition = new Vector3(0,0,0);
    //테스트용입니다.
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
        //StartCoroutine(ViewOff());
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
            Target = EquipmentManager.s_instance.TryGetRandomEquip();
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
    private void CollisionStay(Collision collision)
    {
        Debug.Log(collision.gameObject.name + " coll");
        if (collision.gameObject.GetComponent<Equipment>() != null && collision.gameObject.GetComponent<Equipment>() == Target)
        {
            EquipmentUse(collision.gameObject.GetComponent<Equipment>());
        }
    }
    private void OnTriggerStay(Collider other)
    {
        Debug.Log(other.name);
        if(other.GetComponent<Equipment>()!=null&&other.GetComponent<Equipment>() == Target)
        {
            EquipmentUse(other.GetComponent<Equipment>());
        }
    }
    public void EquipmentUse(Equipment Target)
    {
        Target.Use(this);
        gameObject.SetActive(false);
        switch (Target.equipment_type)
        {
            case Equipment.Equipments.Dumbbel:
                //Animation

                break;
            case Equipment.Equipments.Running:
                //Animation

                break;

            case Equipment.Equipments.smith:
                //Animation

                break;

        }
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
    private void AnimationUpdate()
    {
        switch(GuestState.CurrentAction)
        {
            case GuestState.Action.Moving:
                break;
            case GuestState.Action.Stopping:
                break;
            case GuestState.Action.UsingEquipment:
                break;
        }
    }
}


[System.Serializable]
public class GuestState
{
    public enum Action
    { 
    None,
    Moving,
    UsingEquipment,
    Stopping
    }
    public int id = 0;
    public bool Act;
    public Action CurrentAction;
    public GuestState()
    {
        Act= false;
        CurrentAction = Action.None;
    }
}