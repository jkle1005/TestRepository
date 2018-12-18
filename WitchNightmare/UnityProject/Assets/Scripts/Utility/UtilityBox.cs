using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Witch
{
    // enum
    public enum BUTTON_PROCESS
    {
        __move,
        __tool_use,
        __tool_swap,
        __item_use,
        __item_swap,
        __active,
        __call_setting,
        __call_inventory,
        __call_map,
    }

    public enum WAY
    {
        __previous = -1,
        __stop     =  0,
        __next     =  1,
    }

    public enum UI_WINDOW
    {
        __none,
        __inventory,
    }

    public enum FIELD_TYPE
    {
        __home,
        __village,
    }

    public enum IMAGEBOX
    {
        __motion_upper,
        __motion_lower,
        __shadow,
        __hatface,
        __fx_0,
        __fx_1,
    }

    public enum ANIMATION
    {
        __idle,
        __walk,
        __run,
        __tool_axe,
        __tool_hoe,         
        __tool_watering,
        __tool_net,
    }

    public enum WEATHER
    {
        // 일반
        __sunny  = 0,
        __cloudy = 1,
        __rainy  = 2,

        // 특수
        __snow,
        __thunder,
    }

    public enum TOOL
    {
        __axe,
        __hoe,
        __watering,
        __net,
    }


    // class
    public class TileInfo
    {
        public enum TYPE
        {

        }

    }

    public class Tool
    {
        public string name;
        public int number;
        public ANIMATION animation;

        public Tool(string _name, int _num, ANIMATION _ani)
        {
            name = _name;
            number = _num;
            animation = _ani;
        }
    }

    public class Item
    {
        public string id;
        public string name;
        public int number;

        public Vector2Int inven_coordinate;

        public Sprite icon;

        public void SetInven(Vector2Int _pos)
        {
            inven_coordinate = _pos;
        }

        public Item(string _id, string _name, int _num)
        {
            id = _id;
            name = _name;
            number = _num;
            inven_coordinate = Vector2Int.zero;

            icon = null;
        }

        public Item(string _id)
        {
            id = "EMPTY";
            name = "EMPTY";
            number = -1;
            inven_coordinate = Vector2Int.zero;
            icon = null;
        }
    }

    [Serializable]
    public class Gate
    {
        [SerializeField]
        public GameObject enter_gate;

        [SerializeField]
        public FIELD_TYPE exit_field_type;

        [SerializeField]
        public int exit_field_number;

        [SerializeField]
        public int exit_gate_number;


        public Gate(GameObject _gate, FIELD_TYPE _xft, int _xfn, int _xgn)
        {
            enter_gate = _gate;
            exit_field_type = _xft;
            exit_field_number = _xfn;
            exit_gate_number = _xgn;
        }

        public override string ToString()
        {
            return enter_gate.name + " -> " + exit_field_type.ToString() + "_" + exit_field_number.ToString() + "_" + exit_gate_number.ToString();
        }
    }

    public class TileCoordinate
    {
        public int mx;
        public int my;
        public int px;
        public int py;

        public TileCoordinate(int _mx, int _my, int _px, int _py)
        {
            if (_mx > 0 || _my > 0 || _px < 0 || _py < 0)
                Debug.LogError("TileCoordinate:: wlong way init");

            mx = _mx;
            my = _my;
            px = _px;
            py = _py;
        }
    }


    // struct
}
