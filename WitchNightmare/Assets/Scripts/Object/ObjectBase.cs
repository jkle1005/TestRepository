using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBase : MonoBehaviour
{
    private LayerControl layerControl;
    private bool active;
    public bool Active { get { return active; } }

    private Vector3Int tilePosition;
    public Vector3Int TilePosition { get { return tilePosition; } }
    
    public string info_name;
    public string Info_name { get { return info_name; } }

    public Vector3Int info_pos;    

    public void SetPositionToTile(Vector3Int _pos)
    {
        Debug.Log("ObjectBase::SetPOsitionToTile()");
        tilePosition = _pos;
        Vector3 pos = Vector3.zero;

        pos.x = _pos.x * 20;
        pos.y = _pos.y * 20;

        /*
        if (_pos.x < 0)
            pos.x -= 20;

        if (_pos.y < 0)
            pos.y -= 20;
            */
        pos.x += 10;
        pos.y += 10;

        pos.y += GetComponent<LayerControl>().adjust_y * -1f;

        transform.position = pos;

        
    }

    private void Awake()
    {
        layerControl = GetComponent<LayerControl>();
    }
}
