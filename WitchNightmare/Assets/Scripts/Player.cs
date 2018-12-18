using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.LuisPedroFonseca.ProCamera2D;

using XInputDotNetPure;

using Witch;

public class Player : MonoBehaviour
{
    private static Player instance;
    public static Player Instance
    {
        get
        {
            if (Player.instance == null)
                Player.instance = FindObjectOfType<Player>();
            return Player.instance;
        }
    }

    // 애니메이션
    private PlayerAnimator animator;


    // 정보
    private List<Item> info_item;
    public Item[] Info_item { get { return info_item.ToArray(); } }
    private List<Tool> info_tool;
    public Tool[] Info_tool { get { return info_tool.ToArray(); } }
    private int info_tool_now;
    public int Info_tool_now { get { return info_tool_now; } }
    private int info_item_now;
    public int Info_item_now { get { return info_item_now; } }
    

    // 움직임
    public   Transform  guide;
    public   float      moveWay_speed;
    private  Vector3    moveWay_nowPos;
    private  int        moveWay_before;
    private  int        moveWay_look;
    private  int        moveWay_look_prev;

    // 테스트
    public Transform select_tile;

    public void SetPosition(Vector3Int _pos)
    {
        guide.position = _pos;
        transform.position = _pos;
    }

    public void UseTool()
    {
        animator.SetAnimation(info_tool[info_tool_now].animation, moveWay_look);
        MoveStop();
    }

    public void SetItemNow(int _num)
    {
        info_item_now = _num;
    }

    public Item GetItemNow()
    {
        Debug.Log("Player::GetItemNow() info_item_now = " + info_item_now.ToString() + "info_item.Count = " + info_item.Count.ToString());
        if(info_item.Count <= info_item_now || info_item_now == -1)
        {
            return null;
        }

        return info_item[info_item_now];
    }

    public void AddItem(Item _item)
    {
        info_item.Add(_item);
        UIControl.Instance.InvenAddItem(_item);
    }

    public Tool GetToolNow()
    {
        return info_tool[Info_tool_now];
    }

    public void SwapTool(WAY _way)
    {
        switch (_way)
        {
            case WAY.__previous:
                info_tool_now--;
                if (info_tool_now < 0)
                    info_tool_now = info_tool.Count - 1;
                break;

            case WAY.__stop:
                break;

            case WAY.__next:
                info_tool_now++;
                if (info_tool_now == info_tool.Count)
                    info_tool_now = 0;
                break;
        }
        UIControl.Instance.SetTool(info_tool[info_tool_now]);
    }

    // move update
    private void Update()
    {
        GameManager.Instance.SetPlayerPosition(GetTilePosition(false));
        select_tile.position = GetTilePosition(true) * new Vector3Int(20, 20, 0) + new Vector3Int(10, 10, 0);
    }

    // move fixedupdate
    private void FixedUpdate()
    {
        Interpolate();
    }

    private void Start()
    {
        /*
        Debug.Log(UIControl.Instance.InvenAddItem(new Item("test_0", "Seed", 0)).ToString());
        Debug.Log(UIControl.Instance.InvenAddItem(new Item("test_1", "Plate", 0)).ToString());
        Debug.Log(UIControl.Instance.InvenAddItem(new Item("test_2", "Green Potion", 0)).ToString());
        Debug.Log(UIControl.Instance.InvenAddItem(new Item("test_3", "Red Potion", 0)).ToString());
        Debug.Log(UIControl.Instance.InvenAddItem(new Item("test_4", "Frog", 0)).ToString());*/

        AddItem(new Item("test_0", "Seed", 0));
        AddItem(new Item("test_1", "Plate", 0));
        AddItem(new Item("test_2", "Green Potion", 0));
        AddItem(new Item("test_3", "Red Potion", 0));
        AddItem(new Item("test_4", "Frog", 0));

    }

    // move awake
    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        InitVar();
        InitUI();
    }

    // move initvar
    private void InitVar()
    {
        animator = GetComponent<PlayerAnimator>();

        info_item = new List<Item>();
        info_item_now = 0;        

        info_tool = new List<Tool>();
        info_tool.Add(new Tool("Axe", 0, ANIMATION.__tool_axe));
        info_tool.Add(new Tool("Hoe", 0, ANIMATION.__tool_hoe));        
        info_tool.Add(new Tool("Watering", 0, ANIMATION.__tool_watering));
        info_tool.Add(new Tool("Net", 0, ANIMATION.__tool_net));
        info_tool_now = 0;

        moveWay_before = 5;
        moveWay_look = 5;
    }
    
    private void InitUI()
    {
        UIControl.Instance.SetTool(info_tool[0]);
        UIControl.Instance.SetItemUse();
    }

    public void TurnOnUI()
    {
        MoveStop();

        if(animator.Ani_now == ANIMATION.__run)
        {
            animator.SetAnimation(ANIMATION.__idle, moveWay_before, true);
        }
        Interpolate(true);
    }

    public void TurnOffUI()
    {

    }

    public void MoveInventory(bool _jump)
    {
        UIControl.Instance.InvenMove(InputManager.Instance.Move, _jump);
    }

    public void MoveStop()
    {
        guide.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    }

    public void MoveCharacter()
    {
        // move control     
        float z = transform.position.z;
        moveWay_nowPos = guide.position;
        
        float speed_run = 1f;
        bool run = InputManager.Instance.Run;

        if (run)
        {
            speed_run = 2f;
        }
        else
        {
            speed_run = 1f;
        }
            

        int move = InputManager.Instance.Move;

        switch (move)
        {
            case 1:
                guide.GetComponent<Rigidbody2D>().velocity = new Vector3(-1f, -1f) * moveWay_speed * speed_run * Time.deltaTime;                
                break;

            case 2:
                guide.GetComponent<Rigidbody2D>().velocity = new Vector3(0f, -1.4f) * moveWay_speed * speed_run * Time.deltaTime;
                break;

            case 3:
                guide.GetComponent<Rigidbody2D>().velocity = new Vector3(1f, -1f) * moveWay_speed * speed_run * Time.deltaTime;
                break;

            case 4:
                guide.GetComponent<Rigidbody2D>().velocity = new Vector3(-1.4f, 0) * moveWay_speed * speed_run * Time.deltaTime;
                break;

            case 5:
                guide.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                break;

            case 6:
                guide.GetComponent<Rigidbody2D>().velocity = new Vector3(1.4f, 0) * moveWay_speed * speed_run * Time.deltaTime;
                break;

            case 7:
                guide.GetComponent<Rigidbody2D>().velocity = new Vector3(-1f, 1f) * moveWay_speed * speed_run * Time.deltaTime;
                break;

            case 8:
                guide.GetComponent<Rigidbody2D>().velocity = new Vector3(0f, 1.4f) * moveWay_speed * speed_run * Time.deltaTime;
                break;

            case 9:
                guide.GetComponent<Rigidbody2D>().velocity = new Vector3(1f, 1f) * moveWay_speed * speed_run * Time.deltaTime;
                break;
        }

        if(move == 5)
        {
            if (animator.Ani_now != ANIMATION.__idle)
                animator.SetAnimation(ANIMATION.__idle, moveWay_before, true);
        }
        else 
        {
            if (run)
            {
                if (animator.Ani_now != ANIMATION.__run)
                    animator.SetAnimation(ANIMATION.__run, move, true);
                else if (animator.Ani_way != move)
                    animator.SetAnimationWay(move);
            }
            else
            {
                if (animator.Ani_now != ANIMATION.__walk)
                    animator.SetAnimation(ANIMATION.__walk, move, true);
                else if (animator.Ani_way != move)
                    animator.SetAnimationWay(move);
            }            

            moveWay_before = move;
            SetLookAt(moveWay_before);
        }        
    }

    public void SetLookAt(int _way)
    {
        moveWay_look = _way;

        if (animator.Ani_now == ANIMATION.__idle)
            animator.SetAnimationWay(moveWay_look);
    }

    public int GetToolAnimationWay(ANIMATION _tool)
    {
        int result = 0;

        switch(_tool)
        {
            case ANIMATION.__tool_axe:
                result = moveWay_look;

                if (result == 1 || result == 3 || result == 7 || result == 9)
                    result = moveWay_look_prev;
                break;

            case ANIMATION.__tool_hoe:
                result = moveWay_look;

                if (result == 1 || result == 3 || result == 7 || result == 9)
                    result = moveWay_look_prev;
                break;

            case ANIMATION.__tool_watering:
                break;

            case ANIMATION.__tool_net:
                break;

            default:
                Debug.LogError("Player::GetToolAnimationWay() _tool = " + _tool.ToString() + " is not tool animation");
                break;
        }

        return result;
    }

    public Vector3Int GetTilePosition(bool _front)
    {
        Vector3Int result = Vector3Int.zero;
        result.x = (int)transform.position.x / 20;
        result.y = (int)transform.position.y / 20;

        if (transform.position.x < 0)
        {
            result.x -= 1;
        }

        if(transform.position.y < 0)
        {
            result.y -= 1;
        }

        if(_front)
        {
            // int way = InputManager.Instance.Move;
            int way = moveWay_look;
            /*
            if(way == 1 || way == 4 || way == 7)
            {
                result += Vector3Int.left;
            }
            else if(way == 9 || way == 6 || way == 3)
            {
                result += Vector3Int.right;
            }
            else if(way == 8)
            {
                result += Vector3Int.up;
            }
            else if(way == 2 || way == 5)
            {
                result += Vector3Int.down;
            }
            else
            {
                Debug.LogError("Player::GetTilePosition(_front) _front = " + _front.ToString() + ", way = " + way.ToString());
            }*/

            if (way == 1 || way == 3 || way == 7 || way == 9)
                way = moveWay_look_prev;
            else
                moveWay_look_prev = way;

            if (way == 4)
            {
                result += Vector3Int.left;
            }
            else if (way == 6)
            {
                result += Vector3Int.right;
            }
            else if (way == 8)
            {
                result += Vector3Int.up;
            }
            else if (way == 2 || way == 5)
            {
                result += Vector3Int.down;
            }
            else
            {
                // Debug.LogError("Player::GetTilePosition(_front) _front = " + _front.ToString() + ", way = " + way.ToString());
            }
            return result;
        }            
        else
            return result;
    }

    private void Interpolate(bool _arr = false)
    {
        if (UIControl.Instance.Win_now != UI_WINDOW.__none && !_arr)
            return;

        if (InputManager.Instance.Move == 5 || _arr || animator.Ani_now != ANIMATION.__run)
        {
            float fx = Mathf.Round(guide.position.x);
            float fy = Mathf.Round(guide.position.y);

            // fx = (float)System.Math.Truncate(guide.position.x);
            // fy = (float)System.Math.Truncate(guide.position.y);

            transform.position = new Vector3(fx, fy, 0f);
            GameManager.Instance.camera_main.GetComponent<ProCamera2DPixelPerfect>().SnapMovementToGrid = true;
            GameManager.Instance.camera_main.GetComponent<ProCamera2DPixelPerfect>().SnapCameraToGrid = true;
        }
        else
        {
            transform.position = moveWay_nowPos;
            GameManager.Instance.camera_main.GetComponent<ProCamera2DPixelPerfect>().SnapMovementToGrid = false;
            GameManager.Instance.camera_main.GetComponent<ProCamera2DPixelPerfect>().SnapCameraToGrid = false;
        }

        // GameManager.Instance.camera_main.transform.position = new Vector3(transform.position.x, transform.position.y, -10f);
    }    
}
