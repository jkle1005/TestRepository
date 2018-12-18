using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Com.LuisPedroFonseca.ProCamera2D;


using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

using Witch;

public class Field : MonoBehaviour
{
    public  FIELD_TYPE  type;
    public  int         number;
    public  Vector2Int  size;

    public  float  boundary_top;
    public  float  boundary_bottom;
    public  float  boundary_left;
    public  float  boundary_right;

    [SerializeField]
    Gate[] gates;
    
    [Serializable]
    public class FieldData
    {
        public int type;
        public int number;
        public int[] size;
        public int[,,] tiles;
    }

    private ObjectBase[] objects;

    public ObjectBase GetObject(Vector3Int _pos)
    {
        foreach(ObjectBase obj in objects)
        {
            if(obj.TilePosition == _pos)
            {
                return obj;
            }
        }

        return null;
    }

    public void Open()
    {
        ProCamera2DNumericBoundaries camera = GameManager.Instance.camera_main.GetComponent<ProCamera2DNumericBoundaries>();
        camera.TopBoundary = boundary_top;
        camera.BottomBoundary = boundary_bottom;
        camera.LeftBoundary = boundary_left;
        camera.RightBoundary = boundary_right;
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    private  int[,,]  tile_data;
    private  Tilemap[]  tilemap;

    public string GetTile(Vector3Int _pos)
    {
        List<string> str = new List<string>();

        for (int i = 0; i < tilemap.Length; i++)
        {
            TileBase tile = tilemap[i].GetTile(_pos);

            if (tile == null)
                str.Add("EMPTY");
            else
                str.Add(tile.name);
        }

        foreach(string _str in str)
        {
            if (_str != "EMPTY")
                return _str;
        }

        return "EMPTY";
    }

    public void SetTile(Vector3Int _pos, int _tile)
    {   
        tilemap[1].SetTile(_pos, FieldManager.Instance.tiles[_tile]);
        Vector3Int data = ConvertToData(_pos);

        if(data.x < 0 || data.y < 0 || data.x > size.x || data.y > size.y)
        {
            Debug.LogError("Field::SetTile() _pos = " + _pos.ToString() + ", data = " + data.ToString() + ", size = " + size.ToString());
        }

        tile_data[1, data.y, data.x] = _tile;
    }

    public void Save()
    {
        Debug.Log("Save Start: " + transform.name);

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create("Assets/Data/Field/" + transform.name + ".dat");

        FieldData data = new FieldData();

        data.type = (int)type;
        data.number = number;
        data.size = new int[] { size.x, size.y };
        data.tiles = tile_data;

        bf.Serialize(file, data);
        file.Close();

        Debug.Log("Save Complete");
    }

    public string load_id;
    public void Load()
    {
        Debug.Log("Load Start: " + load_id);

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open("Assets/Data/Field/" + load_id + ".dat", FileMode.Open);

        if(file != null && file.Length > 0)
        {
            FieldData data = (FieldData)bf.Deserialize(file);

            type = (FIELD_TYPE)data.type;
            number = data.number;
            size = new Vector2Int(data.size[0], data.size[1]);
            tile_data = data.tiles;
            InitTile(tile_data);
            file.Close();
            Debug.Log("Load Complete");
        }
        else
        {
            
            Debug.Log("Load Fail");

            if (file == null)
                Debug.Log("File is null");

            if (file.Length <= 0)
                Debug.Log("File is wlong");

            file.Close();
        }
    }

    private void Awake()
    {
        /*
        Debug.Log(type.ToString());
        Debug.Log(number.ToString());
        Debug.Log(size.ToString());
        
        foreach(Gate gate in gates)
        {
            Debug.Log(gate.ToString());
        }
        */

        Vector3Int pos = Vector3Int.zero;

        if (!Utility.Instance.isEven(size.x))
            pos.x = 10;

        if (!Utility.Instance.isEven(size.y))
            pos.y = 10;

        transform.position = pos;

        tilemap = new Tilemap[]
        {
            transform.GetChild(0).GetChild(0).GetComponent<Tilemap>(),
            transform.GetChild(0).GetChild(1).GetComponent<Tilemap>(),
            transform.GetChild(0).GetChild(2).GetComponent<Tilemap>()
        };

        tile_data = new int[tilemap.Length, size.y, size.x];

        objects = new ObjectBase[]
        {
            transform.GetChild(1).GetChild(2).GetComponent<ObjectBase>(),
            transform.GetChild(1).GetChild(3).GetComponent<ObjectBase>(),
            transform.GetChild(1).GetChild(4).GetComponent<ObjectBase>(),
            transform.GetChild(1).GetChild(5).GetComponent<ObjectBase>(),
            transform.GetChild(1).GetChild(6).GetComponent<ObjectBase>(),
        };

    }

    public Vector3Int ConvertToPosition(Vector3Int _pos)
    {
        if(_pos.x < 0 || _pos.y < 0)
        {
            Debug.LogError("Field::ConvertToPosition() Data is not negative int");
            return Vector3Int.zero;
        }

        return new Vector3Int(_pos.x - size.x / 2, _pos.y - size.y / 2, 0);
    }

    public Vector3Int ConvertToData(Vector3Int _pos)
    {
        return new Vector3Int(_pos.x + size.x / 2, _pos.y + size.y / 2, 0);
    }

    private void InitTile(int[,,] _tile)
    {
        for (int i = 0; i < tilemap.Length; i++)
        {
            for (int y = 0; y < size.y; y++)
            {
                for (int x = 0; x < size.x; x++)
                {
                    if(_tile[i, y, x] != 0)
                    {
                        Debug.Log(i.ToString() + ", " + x.ToString() + ", " + y.ToString() + " = " + _tile[i, y, x].ToString());
                        Vector3Int pos = ConvertToPosition(new Vector3Int(x, y, 0));
                        tilemap[i].SetTile(pos, FieldManager.Instance.tiles[_tile[i, y, x]]);
                    }
                        
                }
            }
        }
    }
}
