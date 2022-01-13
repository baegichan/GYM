using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSetting : MonoBehaviour
{

    public enum CameraPos
    {
    None,
    Left,
    Right,
    Front,
    Back
    }
    public CameraPos Current_pos = CameraPos.None;

}
