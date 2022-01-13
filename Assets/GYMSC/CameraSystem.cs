using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystem : MonoBehaviour
{
    public CameraSetting[] CameraPos;
    public CameraSetting CurrnetPos;
    Camera MainCam;
    private void Awake()
    {
        MainCam = Camera.main;
        CurrnetPos = CameraPos[0];
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

    }
    public void CameraMove()
    {
        MainCam.transform.position = CurrnetPos.transform.position;
        MainCam.transform.rotation = CurrnetPos.transform.rotation;
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
