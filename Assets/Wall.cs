using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField]
    public bool horizontal;

    public bool IsHorizontal { get { return horizontal; } }
    public bool IsVertical { get { return !horizontal; } }

}
