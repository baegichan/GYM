using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]
public class Guest : MonoBehaviour
{
  
    public GuestState GuestState = new GuestState();
    public NavMeshAgent Agent;
    public Equipment Target;
    public Equipment PreTarget;
    public Equipment UsingEquipment;
    Vector3 prevPos;

    
    //테스트용입니다.
    private void Update()
    {
        if (Target != null)
        {
            Agent.SetDestination(Target.transform.position);
        }
    }
    private void Start()
    {
        StartCoroutine(CheckRoutine(2));
    }

    public IEnumerator CheckRoutine(int time)
    {
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
            if(Target.Using)
            {
             Target = null;
            }
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