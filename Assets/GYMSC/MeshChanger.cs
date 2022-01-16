using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshChanger : MonoBehaviour
{
    //listMatching
    public List<Mesh> Man;
    public List<Mesh> Women;
    public List<Mesh> CurrentMesh;

    
    public Material ManMaterial;
    public Material WomenMaterial;

    public Guest.Gender gender;
    public void Change(Guest.Gender Targetgender)
    {
        switch(Targetgender)
        {
            case Guest.Gender.M:
                for(int i =0; i<CurrentMesh.Count;i++)
                {
                    CurrentMesh[i] = Man[i];
                }
                break;
            case Guest.Gender.W:
                for (int i = 0; i < CurrentMesh.Count; i++)
                {
                    CurrentMesh[i] = Women[i];
                }
                break;
        }
    }
}
