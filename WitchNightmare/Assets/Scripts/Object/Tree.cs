using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : ObjectBase
{
    private void Awake()
    {
        SetPositionToTile(info_pos);        
    }

    private void OnValidate()
    {
        SetPositionToTile(info_pos);
    }
}
