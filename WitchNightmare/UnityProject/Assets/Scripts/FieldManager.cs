using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FieldManager : MonoBehaviour
{
    private static FieldManager instance;
    public static FieldManager Instance
    {
        get
        {
            if (FieldManager.instance == null)
                FieldManager.instance = FindObjectOfType<FieldManager>();
            return FieldManager.instance;
        }
    }

    public Field[] fields;
    public Field field_now;


    public void OpenField(int _num)
    {
        CloseFieldNow();
        fields[_num].Open();
        field_now = fields[_num];
    }

    public void OpenField(Field _field)
    {
        CloseFieldNow();
        _field.Open();
        field_now = _field;
    }

    public void CloseFieldNow()
    {
        field_now.Close();
    }


    public TileBase[] tiles;

    public void SetTile(Vector3Int _pos, int _tile)
    {
        field_now.SetTile(_pos, _tile);
    }

    public string GetTile(Vector3Int _pos)
    {
        return field_now.GetTile(_pos);
    }
       

    public TileBase GetTile(int _num)
    {
        if(_num == 0)
        {
            Debug.LogError("FieldManager::GetTile() _num is 0");
            return null;
        }

        return tiles[_num];
    }
}
