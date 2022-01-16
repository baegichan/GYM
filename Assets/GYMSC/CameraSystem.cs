using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystem : MonoBehaviour
{
    public CameraSetting[] CameraPos;
    public CameraSetting CurrnetPos;
    private float Current_Zoom_size = 5;
    public Transform View_Target;
    public Transform Sensor;
    public float Zoom_Scale = 10;
    public float Zoom_MaxSize = 5;
    public float Zoom_MinSize = 1;
    public float Speed = 5;
    Camera MainCam;
    private void Awake()
    {
        MainCam = Camera.main;
        CurrnetPos = CameraPos[0];
        Current_Zoom_size = Zoom_MaxSize;
    }
    private void Update()
    {
       
         if(Input.GetKeyDown(KeyCode.Q))
        {
            int Index = IndexReturner();
            if (Index != -1)
            {
                if (Index == 0)
                {
                    CurrnetPos = CameraPos[CameraPos.Length-1];

                }
                else
                {
                    CurrnetPos = CameraPos[Index-1];
                }

            }
            CameraMove();
         
        }
         if(Input.GetKeyDown(KeyCode.E))
        {
            int Index = IndexReturner();
            if (Index != -1)
            {
                if (Index == CameraPos.Length-1)
                {
                    CurrnetPos = CameraPos[0];
                    
                }
                else
                {
                    CurrnetPos = CameraPos[Index+1];
                }


            }
            CameraMove();
                    
        }
        if(Input.GetKey(KeyCode.W))
        {
            this.transform.position += Sensor.transform.forward * +0.01f* Speed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            this.transform.position += Sensor.transform.forward * -0.01f * Speed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            this.transform.position += MainCam.transform.right * -0.01f * Speed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            this.transform.position += MainCam.transform.right * 0.01f * Speed;
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {     
            MainCam.orthographicSize = Mathf.Clamp(Current_Zoom_size-=Time.deltaTime* Zoom_Scale, Zoom_MinSize, Zoom_MaxSize);

        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            MainCam.orthographicSize = Mathf.Clamp(Current_Zoom_size += Time.deltaTime * Zoom_Scale, Zoom_MinSize, Zoom_MaxSize);
        }
        MainCam.transform.LookAt(View_Target);
    }
    public void CameraMove()
    {
        MainCam.transform.position = CurrnetPos.transform.position;
      //  MainCam.transform.rotation = CurrnetPos.transform.rotation;
    }
    public int IndexReturner()
    {
        int count = 0;
        foreach (CameraSetting i in CameraPos)
        {
            if (i == CurrnetPos)
            {
                return count;
            }
            else
            {
                count++;
                continue;
            }
        }
        return -1;
    }

}
