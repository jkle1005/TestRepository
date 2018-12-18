using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

using XInputDotNetPure;

using Witch;

public class InputManager : MonoBehaviour
{
    private static InputManager instance;
    public static InputManager Instance
    {
        get
        {
            if (InputManager.instance == null)
                InputManager.instance = FindObjectOfType<InputManager>();
            return InputManager.instance;
        }
    }

    // test
    public Tilemap tilemap;
    public void SetTile()
    {
        string str = FieldManager.Instance.GetTile(Player.Instance.GetTilePosition(true));

        switch(str)
        {
            case "1":
                FieldManager.Instance.SetTile(Player.Instance.GetTilePosition(true), 2);
                break;

            case "2":
                FieldManager.Instance.SetTile(Player.Instance.GetTilePosition(true), 1);
                break;

            default:
                break;
        }

        
    }

    public void GetTile()
    {
        Debug.Log(FieldManager.Instance.GetTile(Player.Instance.GetTilePosition(false)));

        ObjectBase ob = FieldManager.Instance.field_now.GetObject(Player.Instance.GetTilePosition(true));

        if (ob == null)
            Debug.Log("null");
        else
            Debug.Log(ob.Info_name);
    }


    // 패드
    private bool pc_indexSet;
    PlayerIndex pc_index;
    GamePadState pc_state_now;
    GamePadState pc_state_prev;

    // 키보드
    private bool kc_turn_ws;
    private bool kc_turn_ad;
    private bool kc_input;

    // 입력 프로세스
    private int move;
    public int Move { get { return move; } }
    private bool run;
    public bool Run { get { return run; } }
    private int move_prev;

    private bool tool_use;
    public bool Tool_use { get { return tool_use; } }
    private bool tool_use_active;
    private TOOL tool_use_now;
    private WAY tool_swap;
    public WAY Tool_swap { get { return tool_swap; } }
    private bool tool_swap_active;
    private bool item_use;
    public bool Item_use { get { return item_use; } }
    private bool item_use_active;
    private WAY item_swap;
    public WAY Item_swap { get { return item_swap; } }
    private bool item_swap_active;

    private bool active;
    public bool Active { get { return active; } }
    private bool active_active;

    private bool call_setting;
    public bool Call_setting { get { return call_setting; } }
    private bool call_setting_active;
    private bool call_inventory;
    public bool Call_inventory { get { return call_inventory; } }
    private bool call_inventory_active;
    private bool call_map;
    public bool Call_map { get { return call_map; } }
    private bool call_map_active;

    private bool nowUI;
    public bool NowUI { get { return nowUI; } }

    public void SetNowUI(bool _ui)
    {
        nowUI = _ui;
    }

    // 현재 처리 정보
    public enum CONTROL
    {
        __move,
        __tool,
        __inventory,
    }
    private CONTROL control_now;
    public CONTROL Control_now { get { return control_now; } }
    private float control_ui_delay;
    private bool control_ui_delay_none;



    public void UseToolEnd()
    {
        control_now = CONTROL.__move;
        tool_use_active = false;
        Debug.Log("InputManager::UseToolEnd()");
    }

    public void ButtonProcess(BUTTON_PROCESS _pro, int _num, bool _boo = false)
    {
        switch(_pro)
        {
            case BUTTON_PROCESS.__move:
                move = _num;
                break;

            case BUTTON_PROCESS.__tool_use:
                tool_use = _boo;
                break;

            case BUTTON_PROCESS.__tool_swap:
                tool_swap = (WAY)_num;
                break;

            case BUTTON_PROCESS.__item_use:
                item_use = _boo;
                break;

            case BUTTON_PROCESS.__item_swap:
                item_swap = (WAY)_num;
                break;

            case BUTTON_PROCESS.__active:
                active = _boo;
                break;

            case BUTTON_PROCESS.__call_setting:
                call_setting = _boo;
                break;

            case BUTTON_PROCESS.__call_inventory:
                call_inventory = _boo;
                break;

            case BUTTON_PROCESS.__call_map:
                call_map = _boo;
                break;
        }
    }

    private void UserInputs()
    {
        // Keyboard
        if (Input.GetKey(KeyCode.UpArrow))
            GameManager.Instance.SetDebugKey(KeyCode.UpArrow, 0);
        else
            GameManager.Instance.SetDebugKey(KeyCode.UpArrow, 1);

        if (Input.GetKey(KeyCode.DownArrow))
            GameManager.Instance.SetDebugKey(KeyCode.DownArrow, 0);
        else
            GameManager.Instance.SetDebugKey(KeyCode.DownArrow, 1);

        if (Input.GetKey(KeyCode.LeftArrow))
            GameManager.Instance.SetDebugKey(KeyCode.LeftArrow, 0);
        else
            GameManager.Instance.SetDebugKey(KeyCode.LeftArrow, 1);

        if (Input.GetKey(KeyCode.RightArrow))
            GameManager.Instance.SetDebugKey(KeyCode.RightArrow, 0);
        else
            GameManager.Instance.SetDebugKey(KeyCode.RightArrow, 1);

        if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.DownArrow))
        {
            if (!kc_turn_ws)
            {
                switch (move_prev)
                {
                    case 1:
                        move = 8;
                        break;

                    case 2:
                        move = 8;
                        break;

                    case 3:
                        move = 8;
                        break;

                    case 4:
                        move = 2;
                        break;

                    case 5:
                        move = 2;
                        break;

                    case 6:
                        move = 2;
                        break;

                    case 7:
                        move = 2;
                        break;

                    case 8:
                        move = 2;
                        break;

                    case 9:
                        move = 2;
                        break;
                }

                kc_turn_ws = true;
            }
            else
            {
                switch (move_prev)
                {
                    case 1:
                        move = 2;
                        break;

                    case 2:
                        move = 2;
                        break;

                    case 3:
                        move = 2;
                        break;

                    case 4:
                        move = 8;
                        break;

                    case 5:
                        move = 8;
                        break;

                    case 6:
                        move = 8;
                        break;

                    case 7:
                        move = 8;
                        break;

                    case 8:
                        move = 8;
                        break;

                    case 9:
                        move = 8;
                        break;
                }
            }
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            move = 8;
            kc_turn_ws = false;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            move = 2;
            kc_turn_ws = false;
        }
        else if (!Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow))
        {
            move = 5;
            kc_turn_ws = false;
        }

        if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow))
        {
            if (!kc_turn_ad)
            {
                switch (move_prev)
                {
                    case 1:
                        move += 1;
                        break;

                    case 2:
                        move += 1;
                        break;

                    case 3:
                        move -= 1;
                        break;

                    case 4:
                        move += 1;
                        break;

                    case 5:
                        move += 1;
                        break;

                    case 6:
                        move -= 1;
                        break;

                    case 7:
                        move += 1;
                        break;

                    case 8:
                        move += 1;
                        break;

                    case 9:
                        move -= 1;
                        break;
                }

                kc_turn_ad = true;
            }
            else
            {
                switch (move_prev)
                {
                    case 1:
                        move -= 1;
                        break;

                    case 2:
                        move -= 1;
                        break;

                    case 3:
                        move += 1;
                        break;

                    case 4:
                        move -= 1;
                        break;

                    case 5:
                        move -= 1;
                        break;

                    case 6:
                        move += 1;
                        break;

                    case 7:
                        move -= 1;
                        break;

                    case 8:
                        move -= 1;
                        break;

                    case 9:
                        move += 1;
                        break;
                }
            }

        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            move += 1;
            kc_turn_ad = false;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            move -= 1;
            kc_turn_ad = false;
        }
        else if (!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
        {
            kc_turn_ad = false;
        }

        if (move == 5)
        {
            move = move_prev;
            kc_input = false;
        }
        else
        {
            kc_input = true;
        }

        if (!kc_input)
            move = 5;

        if(Input.GetKey(KeyCode.LeftShift))
        {
            run = false;
        }
        else
        {
            run = true;
        }

        // move tool swap
        if (Input.GetKeyDown(KeyCode.S))
        {            
            tool_swap = WAY.__previous;
        }
        else
        {   
            if (tool_swap == WAY.__previous)
            {
                tool_swap = WAY.__stop;
                tool_swap_active = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            GameManager.Instance.SetDebugPad("P360_RightBumper", 1f);
            tool_swap = WAY.__next;
        }
        else
        {
            GameManager.Instance.SetDebugPad("P360_RightBumper", 0f);

            if (tool_swap == WAY.__next)
            {
                tool_swap = WAY.__stop;
                tool_swap_active = false;
            }
        }

        // move use tool
        if (Input.GetKeyDown(KeyCode.Z))
        {
            GameManager.Instance.SetDebugPad("P360_AButton", 1f);
            tool_use = true;

        }
        else
        {
            GameManager.Instance.SetDebugPad("P360_AButton", 0f);

            if (tool_use)
            {
                tool_use = false;
                tool_use_active = false;
            }
        }

        // move item swap
        if (Input.GetKeyDown(KeyCode.A))
        {
            item_swap = WAY.__previous;
        }
        else
        {
            if (item_swap == WAY.__previous)
            {
                item_swap = WAY.__stop;
                item_swap_active = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            item_swap = WAY.__next;
        }
        else
        {
            if (item_swap == WAY.__next)
            {
                item_swap = WAY.__stop;
                item_swap_active = false;
            }
        }

        // move use item
        if (Input.GetKeyDown(KeyCode.X))
        {
            GameManager.Instance.SetDebugPad("P360_BButton", 1f);
            item_use = true;
        }
        else
        {
            GameManager.Instance.SetDebugPad("P360_BButton", 0f);

            if (item_use)
            {
                item_use = false;
                item_use_active = false;
            }
        }

        // move active
        if (Input.GetKeyDown(KeyCode.C))
        {
            GameManager.Instance.SetDebugPad("P360_XButton", 1f);
            active = true;
        }
        else
        {
            GameManager.Instance.SetDebugPad("P360_XButton", 0f);
            if (active)
            {
                active = false;
                active_active = false;
            }
        }

        // move call inventory
        if (Input.GetKeyDown(KeyCode.E))
        {
            GameManager.Instance.SetDebugPad("P360_YButton", 1f);
            call_inventory = true;
        }
        else
        {
            GameManager.Instance.SetDebugPad("P360_YButton", 0f);

            if (call_inventory)
            {
                call_inventory = false;
                call_inventory_active = false;
            }
        }        

        // GamePad Setting
        if (!pc_indexSet || !pc_state_prev.IsConnected)
        {
            for (int i = 0; i < 4; ++i)
            {                
                PlayerIndex testPlayerIndex = (PlayerIndex)i;
                GamePadState testState = GamePad.GetState(testPlayerIndex);
                if (testState.IsConnected)
                {
                    Debug.Log(string.Format("GamePad found {0}", testPlayerIndex));
                    pc_index = testPlayerIndex;
                    pc_indexSet = true;
                }
            }
        }

        pc_state_prev = pc_state_now;
        pc_state_now = GamePad.GetState(pc_index);

#if UNITY_EDITOR
        if (!pc_state_prev.IsConnected)
            return;
#endif

        // move move
        int dpad = CheckDpadWay(pc_state_now.DPad);        
        GameManager.Instance.SetDebugPad("P360_DPAD", dpad);

        int lstick = CheckAxisWay(pc_state_now.ThumbSticks.Left.X, pc_state_now.ThumbSticks.Left.Y);        
        GameManager.Instance.SetDebugPad("P360_LeftStick", lstick);

        if (lstick == 5)
        {
            move = dpad;
            run = true;
        }            
        else
        {
            move = lstick;

            if (Mathf.Abs(pc_state_now.ThumbSticks.Left.X) + Mathf.Abs(pc_state_now.ThumbSticks.Left.Y) >= 0.5f)
                run = true;
            else
                run = false;
        }
            
        // move look
        GameManager.Instance.SetDebugPad("P360_RightStick", CheckAxisWay(pc_state_now.ThumbSticks.Right.X, pc_state_now.ThumbSticks.Right.Y));
        if(CheckAxisWay(pc_state_now.ThumbSticks.Right.X, pc_state_now.ThumbSticks.Right.Y) != 5)
        {
            Player.Instance.SetLookAt(CheckAxisWay(pc_state_now.ThumbSticks.Right.X, pc_state_now.ThumbSticks.Right.Y));
        }

        // move tool swap
        if (pc_state_now.Buttons.LeftShoulder == ButtonState.Pressed)
        {
            GameManager.Instance.SetDebugPad("P360_LeftBumper", 1f);
            tool_swap = WAY.__previous;
        }
        else
        {
            GameManager.Instance.SetDebugPad("P360_LeftBumper", 0f);

            if (tool_swap == WAY.__previous)
            {
                tool_swap = WAY.__stop;
                tool_swap_active = false;
            }
        }

        if (pc_state_now.Buttons.RightShoulder == ButtonState.Pressed)
        {
            GameManager.Instance.SetDebugPad("P360_RightBumper", 1f);
            tool_swap = WAY.__next;
        }
        else
        {
            GameManager.Instance.SetDebugPad("P360_RightBumper", 0f);

            if (tool_swap == WAY.__next)
            {
                tool_swap = WAY.__stop;
                tool_swap_active = false;
            }
        }

        // move use tool
        if (pc_state_now.Buttons.A == ButtonState.Pressed)
        {
            GameManager.Instance.SetDebugPad("P360_AButton", 1f);
            tool_use = true;

        }
        else
        {
            GameManager.Instance.SetDebugPad("P360_AButton", 0f);

            if (tool_use)
            {
                tool_use = false;
                tool_use_active = false;
            }
        }

        // move item swap
        GameManager.Instance.SetDebugPad("P360_LeftTrigger", pc_state_now.Triggers.Left);
        GameManager.Instance.SetDebugPad("P360_RightTrigger", pc_state_now.Triggers.Right);
        if(pc_state_now.Triggers.Left >= 0.3f)
        {
            item_swap = WAY.__previous;
        }
        else
        {
            if (item_swap == WAY.__previous)
            {
                item_swap = WAY.__stop;
                item_swap_active = false;
            }
        }

        if(pc_state_now.Triggers.Right >= 0.3f)
        {
            item_swap = WAY.__next;
        }
        else
        {
            if(item_swap == WAY.__next)
            {
                item_swap = WAY.__stop;
                item_swap_active = false;
            }            
        }
        
        // move use item
        if (pc_state_now.Buttons.B == ButtonState.Pressed)
        {
            GameManager.Instance.SetDebugPad("P360_BButton", 1f);
            item_use = true;
        }
        else
        {
            GameManager.Instance.SetDebugPad("P360_BButton", 0f);

            if(item_use)
            {
                item_use = false;
                item_use_active = false;
            }            
        }

        // move active
        if (pc_state_now.Buttons.X == ButtonState.Pressed)
        {
            GameManager.Instance.SetDebugPad("P360_XButton", 1f);
            active = true;            
        }
        else
        {
            GameManager.Instance.SetDebugPad("P360_XButton", 0f);
            if(active)
            {
                active = false;
                active_active = false;
            }            
        }

        // move call inventory
        if (pc_state_now.Buttons.Y == ButtonState.Pressed)
        {
            GameManager.Instance.SetDebugPad("P360_YButton", 1f);
            call_inventory = true;
        }
        else
        {
            GameManager.Instance.SetDebugPad("P360_YButton", 0f);

            if(call_inventory)
            {
                call_inventory = false;
                call_inventory_active = false;
            }            
        }

        // move call setting
        if (pc_state_now.Buttons.Start == ButtonState.Pressed)
        {
            GameManager.Instance.SetDebugPad("P360_Start", 1f);
        }
        else
        {
            GameManager.Instance.SetDebugPad("P360_Start", 0f);
        }

        // move call setting
        if (pc_state_now.Buttons.Back == ButtonState.Pressed)
        {
            GameManager.Instance.SetDebugPad("P360_Back", 1f);
            call_setting = true;
        }
        else
        {
            GameManager.Instance.SetDebugPad("P360_Back", 0f);
            call_setting = false;
        }

        if (pc_state_now.Buttons.LeftStick == ButtonState.Pressed)
        {
            GameManager.Instance.SetDebugPad("P360_LeftThumbStickButton", 1f);
        }
        else
        {
            GameManager.Instance.SetDebugPad("P360_LeftThumbStickButton", 0f);
        }

        if (pc_state_now.Buttons.RightStick == ButtonState.Pressed)
        {
            GameManager.Instance.SetDebugPad("P360_RightThumbStickButton", 1f);
        }
        else
        {
            GameManager.Instance.SetDebugPad("P360_RightThumbStickButton", 0f);
        }        

        

        
    }

    private int CheckAxisWay(float _x, float _y)
    {
        int way = 5;

        if (_x == 0f)
        {
            way = 5;
        }
        else if (_x > 0f)
        {
            way++;
        }
        else if (_x < 0f)
        {
            way--;
        }

        if (_y == 0f)
        {

        }
        else if (_y > 0f)
        {
            way += 3;
        }
        else if (_y < 0f)
        {
            way -= 3;
        }

        return way;
    }

    private int CheckDpadWay(GamePadDPad _dpad)
    {
        int way = 5;

        if (_dpad.Up == ButtonState.Pressed)
        {
            way += 3;
        }
        else if (_dpad.Down == ButtonState.Pressed)
        {
            way -= 3;
        }

        if (_dpad.Left == ButtonState.Pressed)
        {
            way--;
        }
        else if (_dpad.Right == ButtonState.Pressed)
        {
            way++;
        }

        return way;
    }


    // move update
    private void Update()
    {
        UserInputs();

        if (call_inventory && !call_inventory_active)
        {
            if(UIControl.Instance.Win_now == UI_WINDOW.__none)
            {
                UIControl.Instance.CallWindow(UI_WINDOW.__inventory);
                control_now = CONTROL.__inventory;
                call_inventory_active = true;
                move = 5;
            }
            else if(UIControl.Instance.Win_now == UI_WINDOW.__inventory)
            {
                UIControl.Instance.CloseWindow();
                control_now = CONTROL.__move;
                call_inventory_active = true;                
            }
            else
            {
                UIControl.Instance.SwapWindow(UI_WINDOW.__inventory);
                control_now = CONTROL.__inventory;
                call_inventory_active = true;
                move = 5;
            }            
        }

        switch (control_now)
        {
            case CONTROL.__move:
                Player.Instance.MoveCharacter();

                if (control_now != CONTROL.__tool)
                {
                    if (tool_swap != WAY.__stop && !tool_swap_active)
                    {
                        Player.Instance.SwapTool(tool_swap);
                        tool_swap_active = true;

                    }
                }

                if (item_swap != WAY.__stop && !item_swap_active)
                {
                    UIControl.Instance.SwapItem(item_swap);
                    item_swap_active = true;
                }

                if (tool_use && !tool_use_active)
                {
                    // SetTile();
                    // tool_use_active = true;
                    control_now = CONTROL.__tool;
                }

                if(active && !active_active)
                {
                    GetTile();
                    active_active = true;
                }

                if(item_use && !item_use_active)
                {
                    Item item = Player.Instance.GetItemNow();

                    if (item == null)
                        Debug.Log("Use Item: EMPTY");
                    else
                        Debug.Log("Use Item: " + Player.Instance.GetItemNow().name);

                    item_use_active = true;
                }
                break;

            case CONTROL.__tool:
                if(!tool_use_active)
                {
                    Player.Instance.UseTool();
                    tool_use_active = true;
                }
                break;

            case CONTROL.__inventory:
                if (move == 5)
                {
                    control_ui_delay = 0f;
                    control_ui_delay_none = false;
                }
                else
                {
                    if (control_ui_delay <= 0f)
                    {
                        Player.Instance.MoveInventory(!control_ui_delay_none);

                        if (control_ui_delay_none)
                            control_ui_delay = 0.04f;
                        else
                        {
                            control_ui_delay = 0.3f;
                            control_ui_delay_none = true;
                        }
                    }
                    else
                    {
                        control_ui_delay -= Time.deltaTime;
                    }
                }

                if (tool_use && !tool_use_active)
                {
                    UIControl.Instance.InvenSelectCell();
                    tool_use_active = true;
                }

                if(item_use && !item_use_active)
                {
                    UIControl.Instance.InvenSelectCellCancel();
                    item_use_active = true;
                }

                break;
        }

       
        

          
    }

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        move = 5;

        tool_use = false;
        tool_use_active = false;
        tool_use_now = TOOL.__axe;
        tool_swap = 0;
        tool_swap_active = false;
        item_use = false;
        item_use_active = false;
        item_swap = 0;
        item_swap_active = false;

        active = false;
        active_active = false;

        call_setting = false;
        call_setting_active = false;
        call_inventory = false;
        call_inventory_active = false;
        call_map = false;
        call_map_active = false;

        control_now = CONTROL.__move;
    }
}
