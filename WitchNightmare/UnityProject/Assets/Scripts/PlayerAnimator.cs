using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Witch;

public class PlayerAnimator : MonoBehaviour
{
    private  SpriteRenderer[]  imagebox;


    // Animation variable
    private  ANIMATION  ani_now;
    public   ANIMATION  Ani_now { get { return ani_now; } }
    private  int        ani_nowNum;
    private  int        ani_way;
    public   int        Ani_way { get { return ani_way; } }
    private  bool       ani_skip;
    private  float      ani_nowDelay;
    private  float      ani_nowTime;
    public   bool       ani_variable;

    private  List<SpriteRenderer>  ani_srr_fx;

    // Sprite
    private AnimationCut[][] ac_idle_motion_upper;
    private AnimationCut[][] ac_idle_motion_lower;
    private Sprite[][] spr_idle_hatface;
    private Sprite[] spr_idle_shadow;    

    private AnimationCut[][] ac_walk_motion_upper;
    private AnimationCut[][] ac_walk_motion_lower;
    private Sprite[][] spr_walk_hatface;
    private Sprite[] spr_walk_shadow;
    
    private AnimationCut[][] ac_run_motion_upper;
    private AnimationCut[][] ac_run_motion_lower;
    private Sprite[][] spr_run_hatface;
    private Sprite[] spr_run_shadow;

    private AnimationCut[][] ac_hoe_motion_upper;
    private AnimationCut[][] ac_hoe_motion_lower;
    private Sprite[][] spr_hoe_hatface;
    private Sprite[] spr_hoe_shadow;
    private Sprite[][] spr_hoe_fx_ground;
    private Sprite[][] spr_hoe_fx_swing;

    private AnimationCut[][] ac_axe_motion_upper;
    private AnimationCut[][] ac_axe_motion_lower;
    private Sprite[][] spr_axe_hatface;
    private Sprite[] spr_axe_shadow;
    private Sprite[][] spr_axe_fx_swing;

    private AnimationCut[][] ac_wat_motion_upper;
    private AnimationCut[][] ac_wat_motion_lower;
    private Sprite[][] spr_wat_hatface;
    private Sprite[] spr_wat_shadow;
    private Sprite[][] spr_wat_fx_water;


    public void SetImageBoxPosition(ANIMATION _ani, int _way)
    {
        switch (_ani)
        {
            case ANIMATION.__idle:
                imagebox[0].transform.localPosition = Vector3Int.zero;
                imagebox[1].transform.localPosition = new Vector3Int(0, -25, 0);
                imagebox[2].transform.localPosition = new Vector3Int(0, -7, 0);
                imagebox[3].transform.localPosition = new Vector3Int(0, -6, 0);
                imagebox[4].transform.localPosition = new Vector3Int(0, -6, 0);
                imagebox[5].transform.localPosition = new Vector3Int(0, -6, 0);
                break;

            case ANIMATION.__walk:
                imagebox[0].transform.localPosition = Vector3Int.zero;
                imagebox[1].transform.localPosition = new Vector3Int(0, -25, 0);
                imagebox[2].transform.localPosition = new Vector3Int(0, -7, 0);
                imagebox[3].transform.localPosition = new Vector3Int(0, -6, 0);
                imagebox[4].transform.localPosition = new Vector3Int(0, -6, 0);
                imagebox[5].transform.localPosition = new Vector3Int(0, -6, 0);
                break;

            case ANIMATION.__run:
                imagebox[0].transform.localPosition = Vector3Int.zero;
                imagebox[1].transform.localPosition = new Vector3Int(0, -25, 0);
                imagebox[2].transform.localPosition = new Vector3Int(0, -7, 0);
                imagebox[3].transform.localPosition = new Vector3Int(0, -6, 0);
                imagebox[4].transform.localPosition = new Vector3Int(0, -6, 0);
                imagebox[5].transform.localPosition = new Vector3Int(0, -6, 0);
                break;

            case ANIMATION.__tool_axe:
                if (_way == 2 || _way == 5 || _way == 8 || _way == 7 || _way == 9)
                {
                    imagebox[0].transform.localPosition = new Vector3Int(0, -6, 0);
                    imagebox[1].transform.localPosition = new Vector3Int(0, -6, 0);
                }
                else
                {
                    imagebox[0].transform.localPosition = new Vector3(0f, 7.5f, 0f);
                    imagebox[1].transform.localPosition = new Vector3(0f, -32.5f, 0f);
                }                

                imagebox[4].GetComponent<LayerControl>().SetStaticLayer(LayerControl.STATIC_TYPE.static_relative, imagebox[0], 1);
                imagebox[2].transform.localPosition = new Vector3Int(0, -6, 0);
                imagebox[3].transform.localPosition = new Vector3Int(0, -6, 0);
                imagebox[4].transform.localPosition = new Vector3Int(0, -6, 0);
                break;

            case ANIMATION.__tool_hoe:    
                if(_way == 8)
                {
                    imagebox[0].transform.localPosition = new Vector3Int(0, -6, 0);
                    imagebox[1].transform.localPosition = new Vector3Int(0, -6, 0);
                    imagebox[4].GetComponent<LayerControl>().SetStaticLayer(LayerControl.STATIC_TYPE.static_relative, imagebox[0], 1);
                    imagebox[5].GetComponent<LayerControl>().SetStaticLayer(LayerControl.STATIC_TYPE.static_relative, imagebox[0], -1);
                }
                else
                {
                    if (_way == 2 || _way == 5)
                    {
                        imagebox[0].transform.localPosition = new Vector3Int(0, -6, 0);
                    }
                    else
                    {
                        imagebox[0].transform.localPosition = new Vector3(0f, 7.5f, 0f);
                    }
                    imagebox[1].transform.localPosition = new Vector3(0f, -32.5f, 0f);

                    imagebox[4].GetComponent<LayerControl>().SetStaticLayer(LayerControl.STATIC_TYPE.static_relative, imagebox[0], 1);
                    imagebox[5].GetComponent<LayerControl>().SetStaticLayer(LayerControl.STATIC_TYPE.static_relative, imagebox[0], 1);
                }
                
                imagebox[2].transform.localPosition = new Vector3Int(0, -6, 0);
                imagebox[3].transform.localPosition = new Vector3Int(0, -6, 0);
                imagebox[4].transform.localPosition = new Vector3Int(0, -6, 0);

                // imagebox[5].transform.position = Player.Instance.GetTilePosition(true) * new Vector3Int(20, 20, 0) + new Vector3Int(30, 24, 0);
                imagebox[5].transform.localPosition = new Vector3Int(0, -6, 0);

                imagebox[4].color = Color.white;


                imagebox[5].color = new Color32(140, 120, 60, 255);
                break;

            case ANIMATION.__tool_watering:
                if(_way == 2)
                {
                    imagebox[0].transform.localPosition = new Vector3(0f, -6f, 0f);
                    imagebox[1].transform.localPosition = new Vector3(0f, -6f, 0f);
                }
                else
                {
                    imagebox[0].transform.localPosition = new Vector3(0f, 7.5f, 0f);
                    imagebox[1].transform.localPosition = new Vector3(0f, -32.5f, 0f);
                }
                
                imagebox[2].transform.localPosition = new Vector3Int(0, -7, 0);
                imagebox[3].transform.localPosition = new Vector3Int(0, -6, 0);
                imagebox[4].transform.localPosition = new Vector3Int(0, -6, 0);
                imagebox[5].transform.localPosition = new Vector3Int(0, -6, 0);

                if(_way <= 6)
                {
                    imagebox[4].GetComponent<LayerControl>().SetStaticLayer(LayerControl.STATIC_TYPE.static_relative, imagebox[0], 1);
                }
                else
                {
                    imagebox[4].GetComponent<LayerControl>().SetStaticLayer(LayerControl.STATIC_TYPE.static_relative, imagebox[0], -1);
                }
                break;

            case ANIMATION.__tool_net:
                break;
        }
    }

    public void SetAnimationWay(int _way)
    {
        ani_way = _way;
    }

    public void SetAnimation(ANIMATION _ani, int _way, bool _skip = false)
    {
        if (!ani_variable)
        {
            Debug.Log("PlayerAnimator::SetAnimation(_ani, _way, _skip) " + _ani.ToString() + ", " + _way.ToString() + ", " + _skip.ToString());            
            return;
        }            

        // Debug.Log("PlayerAnimator::SetAnimation(_ani, _way, _skip) " + _ani.ToString() + ", " + _way.ToString() + ", " + _skip.ToString());
        switch(ani_now)
        {
            case ANIMATION.__idle:
                break;

            case ANIMATION.__walk:
                break;

            case ANIMATION.__run:
                break;

            case ANIMATION.__tool_axe:
                break;

            case ANIMATION.__tool_hoe:
                break;

            case ANIMATION.__tool_watering:
                break;

            case ANIMATION.__tool_net:
                break;
        }

        switch (_ani)
        {
            case ANIMATION.__idle:
                GetImageBox(IMAGEBOX.__shadow).sprite = spr_idle_shadow[_way];
                ani_variable = true;
                break;

            case ANIMATION.__walk:
                break;

            case ANIMATION.__run:
                GetImageBox(IMAGEBOX.__shadow).sprite = spr_run_shadow[_way];
                ani_variable = true;
                break;

            case ANIMATION.__tool_axe:
                ani_variable = false;
                break;

            case ANIMATION.__tool_hoe:
                ani_variable = false;
                break;

            case ANIMATION.__tool_watering:
                ani_variable = false;
                break;

            case ANIMATION.__tool_net:
                ani_variable = false;
                break;
        }

        ani_now = _ani;
        ani_way = _way;
        ani_nowNum = 0;
        ani_skip = _skip;
    }

    IEnumerator animate()
    {
        float time_now = 0f;
        ANIMATION next_ani = ANIMATION.__idle;
        bool next = false;

        while(true)
        {
            SetImageBoxPosition(ani_now, ani_way);
            switch(ani_now)
            {
                case ANIMATION.__idle:                    
                    GetImageBox(IMAGEBOX.__motion_upper).sprite = ac_idle_motion_upper[ani_way][ani_nowNum].spr;
                    GetImageBox(IMAGEBOX.__motion_lower).sprite = ac_idle_motion_lower[ani_way][ani_nowNum].spr;
                    GetImageBox(IMAGEBOX.__shadow).sprite = spr_idle_shadow[ani_way];                        

                    if (ani_way == 8)
                        GetImageBox(IMAGEBOX.__hatface).sprite = null;
                    else
                        GetImageBox(IMAGEBOX.__hatface).sprite = spr_idle_hatface[ani_way][ani_nowNum];

                    ani_nowDelay = ac_idle_motion_upper[ani_way][ani_nowNum].delay;
                    ani_nowNum++;

                    if(ani_nowNum == ac_idle_motion_upper[ani_way].Length)
                    {
                        ani_nowNum = 0;
                    }
                    break;

                case ANIMATION.__walk:
                    GetImageBox(IMAGEBOX.__motion_upper).sprite = ac_walk_motion_upper[ani_way][ani_nowNum].spr;
                    GetImageBox(IMAGEBOX.__motion_lower).sprite = ac_walk_motion_lower[ani_way][ani_nowNum].spr;
                    GetImageBox(IMAGEBOX.__shadow).sprite = spr_walk_shadow[ani_way];

                    if (ani_way == 8)
                        GetImageBox(IMAGEBOX.__hatface).sprite = null;
                    else
                        GetImageBox(IMAGEBOX.__hatface).sprite = spr_walk_hatface[ani_way][ani_nowNum];

                    ani_nowDelay = ac_walk_motion_upper[ani_way][ani_nowNum].delay;
                    ani_nowNum++;

                    if (ani_nowNum == ac_run_motion_upper[ani_way].Length)
                    {
                        ani_nowNum = 0;
                    }
                    break;

                case ANIMATION.__run:
                    GetImageBox(IMAGEBOX.__motion_upper).sprite = ac_run_motion_upper[ani_way][ani_nowNum].spr;
                    GetImageBox(IMAGEBOX.__motion_lower).sprite = ac_run_motion_lower[ani_way][ani_nowNum].spr;
                    GetImageBox(IMAGEBOX.__shadow).sprite = spr_run_shadow[ani_way];                    

                    if (ani_way == 8)
                        GetImageBox(IMAGEBOX.__hatface).sprite = null;
                    else
                        GetImageBox(IMAGEBOX.__hatface).sprite = spr_run_hatface[ani_way][ani_nowNum];

                    ani_nowDelay = ac_run_motion_upper[ani_way][ani_nowNum].delay;
                    ani_nowNum++;

                    if (ani_nowNum == ac_run_motion_upper[ani_way].Length)
                    {
                        ani_nowNum = 0;
                    }
                    break;

                case ANIMATION.__tool_axe:
                    if(ani_nowNum == 0)
                    {
                        GetImageBox(IMAGEBOX.__shadow).sprite = spr_axe_shadow[ani_way];
                    }

                    GetImageBox(IMAGEBOX.__motion_upper).sprite = ac_axe_motion_upper[ani_way][ani_nowNum].spr;
                    GetImageBox(IMAGEBOX.__motion_lower).sprite = ac_axe_motion_lower[ani_way][ani_nowNum].spr;

                    if (ani_nowNum >= 2 && ani_nowNum <= 3)
                    {
                        GetImageBox(IMAGEBOX.__fx_0).sprite = spr_axe_fx_swing[ani_way][ani_nowNum - 2];
                    }
                    else
                        GetImageBox(IMAGEBOX.__fx_0).sprite = null;

                    if (ani_way == 8)
                        GetImageBox(IMAGEBOX.__hatface).sprite = null;
                    else
                        GetImageBox(IMAGEBOX.__hatface).sprite = spr_axe_hatface[ani_way][ani_nowNum];

                    ani_nowDelay = ac_axe_motion_upper[ani_way][ani_nowNum].delay;
                    ani_nowNum++;

                    if(ani_nowNum == ac_axe_motion_upper[ani_way].Length)
                    {
                        GetImageBox(IMAGEBOX.__fx_0).sprite = null;
                        if (InputManager.Instance.Tool_use)
                        {
                            ani_nowNum = 0;
                        }
                        else
                        {
                            next = true;
                            next_ani = ANIMATION.__idle;
                            ani_variable = true;
                            InputManager.Instance.UseToolEnd();
                        }
                    }
                    break;

                case ANIMATION.__tool_hoe:
                    if(ani_nowNum == 0)
                    {
                        GetImageBox(IMAGEBOX.__shadow).sprite = spr_hoe_shadow[ani_way];
                    }

                    GetImageBox(IMAGEBOX.__motion_upper).sprite = ac_hoe_motion_upper[ani_way][ani_nowNum].spr;
                    GetImageBox(IMAGEBOX.__motion_lower).sprite = ac_hoe_motion_lower[ani_way][ani_nowNum].spr;
                    

                    if(ani_nowNum >= 3 && ani_nowNum <= 4)
                    {
                        GetImageBox(IMAGEBOX.__fx_0).sprite = spr_hoe_fx_swing[ani_way][ani_nowNum - 3];
                    }
                    else
                    {
                        GetImageBox(IMAGEBOX.__fx_0).sprite = null;
                    }

                    if (ani_nowNum >= 3 && ani_nowNum <= 5)
                    {
                        GetImageBox(IMAGEBOX.__fx_1).sprite = spr_hoe_fx_ground[ani_way][ani_nowNum - 3];
                    }
                    else
                    {
                        GetImageBox(IMAGEBOX.__fx_1).sprite = null;
                    }                    

                    if (ani_way == 8)
                        GetImageBox(IMAGEBOX.__hatface).sprite = null;
                    else
                        GetImageBox(IMAGEBOX.__hatface).sprite = spr_hoe_hatface[ani_way][ani_nowNum];

                    ani_nowDelay = ac_hoe_motion_upper[ani_way][ani_nowNum].delay;
                    ani_nowNum++;

                    if (ani_nowNum == ac_hoe_motion_upper[ani_way].Length)
                    {
                        GetImageBox(IMAGEBOX.__fx_0).sprite = null;
                        GetImageBox(IMAGEBOX.__fx_1).sprite = null;
                        if (InputManager.Instance.Tool_use)
                        {
                            ani_nowNum = 0;
                        }
                        else
                        {
                            next = true;
                            next_ani = ANIMATION.__idle;
                            ani_variable = true;
                            InputManager.Instance.UseToolEnd();
                        }                        
                    }
                    break;

                case ANIMATION.__tool_watering:
                    GetImageBox(IMAGEBOX.__motion_upper).sprite = ac_wat_motion_upper[ani_way][ani_nowNum].spr;

                    if (ani_way == 2)
                        GetImageBox(IMAGEBOX.__motion_lower).sprite = null;
                    else
                        GetImageBox(IMAGEBOX.__motion_lower).sprite = ac_wat_motion_lower[ani_way][ani_nowNum].spr;

                    GetImageBox(IMAGEBOX.__shadow).sprite = spr_wat_shadow[ani_way];

                    if (ani_way == 8)
                    {
                        GetImageBox(IMAGEBOX.__hatface).sprite = null;
                        GetImageBox(IMAGEBOX.__fx_0).sprite = null;
                    }                        
                    else
                    {
                        GetImageBox(IMAGEBOX.__hatface).sprite = spr_wat_hatface[ani_way][ani_nowNum];

                        if(ani_nowNum >= 1)
                        {
                            GetImageBox(IMAGEBOX.__fx_0).sprite = spr_wat_fx_water[ani_way][ani_nowNum - 1];
                        }
                    }                        

                    ani_nowDelay = ac_wat_motion_upper[ani_way][ani_nowNum].delay;
                    ani_nowNum++;

                    if (ani_nowNum == ac_wat_motion_upper[ani_way].Length)
                    {
                        GetImageBox(IMAGEBOX.__fx_0).sprite = null;
                        if (InputManager.Instance.Tool_use)
                        {
                            ani_nowNum = 0;
                        }
                        else
                        {
                            next = true;
                            next_ani = ANIMATION.__idle;
                            ani_variable = true;
                            InputManager.Instance.UseToolEnd();
                        }
                    }
                    break;

                case ANIMATION.__tool_net:
                    next = true;
                    next_ani = ANIMATION.__idle;
                    ani_variable = true;
                    InputManager.Instance.UseToolEnd();
                    break;
            }

            while (true)
            {
                if (time_now >= ani_nowDelay)
                {
                    time_now = 0f;
                    break;
                }
                else
                    time_now += Time.deltaTime;

                if(ani_skip)
                {
                    ani_skip = false;
                    time_now = 0f;                    
                    break;
                }

                if(next)
                {
                    SetAnimation(next_ani, ani_way);
                    next = false;
                    next_ani = ANIMATION.__idle;
                }

                yield return null;
            }


            if (ani_srr_fx.Count != 0)
            {
                foreach (SpriteRenderer srr in ani_srr_fx)
                {
                    srr.sprite = null;
                    srr.gameObject.SetActive(false);
                }
                ani_srr_fx.Clear();
            }
            else if (ani_srr_fx.Count == 1)
            {
                ani_srr_fx[0].sprite = null;
                ani_srr_fx[0].gameObject.SetActive(false);
                ani_srr_fx.Clear();
            }

            yield return null;
        }
    }

	public SpriteRenderer GetImageBox(IMAGEBOX _box)
    {
        return imagebox[(int)_box];
    }

    private void Start()
    {
        StartCoroutine(animate());
    }

    private void Awake()
    {       
        Init();
    }

    private void Init()
    {
        InitVar();
        InitSprite();
    }

    private void InitVar()
    {
        imagebox = new SpriteRenderer[]
        {
            transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>(),
            transform.GetChild(0).GetChild(1).GetComponent<SpriteRenderer>(),
            transform.GetChild(0).GetChild(2).GetComponent<SpriteRenderer>(),
            transform.GetChild(0).GetChild(3).GetComponent<SpriteRenderer>(),
            transform.GetChild(0).GetChild(4).GetComponent<SpriteRenderer>(),
            transform.GetChild(0).GetChild(5).GetComponent<SpriteRenderer>()
        };

        ani_now = ANIMATION.__idle;
        ani_nowNum = 0;
        ani_nowDelay = 0f;
        ani_nowTime = 0f;
        ani_skip = false;
        ani_way = 5;

        ani_variable = true;

        ani_srr_fx = new List<SpriteRenderer>();
    }

    private void InitSprite()
    {

        // idle
        Sprite[][] spr_dump = new Sprite[][]
        {
            null,
            Resources.LoadAll<Sprite>("Sprites/Player/Motion/Idle/idle_1"),
            Resources.LoadAll<Sprite>("Sprites/Player/Motion/Idle/idle_2"),
            Resources.LoadAll<Sprite>("Sprites/Player/Motion/Idle/idle_3"),
            Resources.LoadAll<Sprite>("Sprites/Player/Motion/Idle/idle_7"),
            Resources.LoadAll<Sprite>("Sprites/Player/Motion/Idle/idle_8"),
            Resources.LoadAll<Sprite>("Sprites/Player/Motion/Idle/idle_9"),
        };
        Sprite[][] spr_upper = new Sprite[][]
        {
            null,
            new Sprite[] { spr_dump[1][0], spr_dump[1][1], spr_dump[1][2], spr_dump[1][3], },
            new Sprite[] { spr_dump[2][0], spr_dump[2][1], spr_dump[2][2], spr_dump[2][3], },
            new Sprite[] { spr_dump[3][0], spr_dump[3][1], spr_dump[3][2], spr_dump[3][3], },
            new Sprite[] { spr_dump[1][0], spr_dump[1][1], spr_dump[1][2], spr_dump[1][3], },
            new Sprite[] { spr_dump[2][0], spr_dump[2][1], spr_dump[2][2], spr_dump[2][3], },
            new Sprite[] { spr_dump[3][0], spr_dump[3][1], spr_dump[3][2], spr_dump[3][3], },
            new Sprite[] { spr_dump[4][0], spr_dump[4][1], spr_dump[4][2], spr_dump[4][3], },
            new Sprite[] { spr_dump[5][0], spr_dump[5][1], spr_dump[5][2], spr_dump[5][3], },
            new Sprite[] { spr_dump[6][0], spr_dump[6][1], spr_dump[6][2], spr_dump[6][3], },
        };
        Sprite[][] spr_lower = new Sprite[][]
        {
            null,
            new Sprite[] { spr_dump[1][4], spr_dump[1][5], spr_dump[1][6], spr_dump[1][7], },
            new Sprite[] { spr_dump[2][4], spr_dump[2][5], spr_dump[2][6], spr_dump[2][7], },
            new Sprite[] { spr_dump[3][4], spr_dump[3][5], spr_dump[3][6], spr_dump[3][7], },
            new Sprite[] { spr_dump[1][4], spr_dump[1][5], spr_dump[1][6], spr_dump[1][7], },
            new Sprite[] { spr_dump[2][4], spr_dump[2][5], spr_dump[2][6], spr_dump[2][7], },
            new Sprite[] { spr_dump[3][4], spr_dump[3][5], spr_dump[3][6], spr_dump[3][7], },
            new Sprite[] { spr_dump[4][4], spr_dump[4][5], spr_dump[4][6], spr_dump[4][7], },
            new Sprite[] { spr_dump[5][4], spr_dump[5][5], spr_dump[5][6], spr_dump[5][7], },
            new Sprite[] { spr_dump[6][4], spr_dump[6][5], spr_dump[6][6], spr_dump[6][7], },
        };

        ac_idle_motion_upper = new AnimationCut[][]
        {
            null,
            Utility.Instance.CombineAnimationCut(spr_upper[1], new float[] { 0.2f }),
            Utility.Instance.CombineAnimationCut(spr_upper[2], new float[] { 0.2f }),
            Utility.Instance.CombineAnimationCut(spr_upper[3], new float[] { 0.2f }),
            Utility.Instance.CombineAnimationCut(spr_upper[4], new float[] { 0.2f }),
            Utility.Instance.CombineAnimationCut(spr_upper[5], new float[] { 0.2f }),
            Utility.Instance.CombineAnimationCut(spr_upper[6], new float[] { 0.2f }),
            Utility.Instance.CombineAnimationCut(spr_upper[7], new float[] { 0.2f }),
            Utility.Instance.CombineAnimationCut(spr_upper[8], new float[] { 0.2f }),
            Utility.Instance.CombineAnimationCut(spr_upper[9], new float[] { 0.2f }),
        };

        ac_idle_motion_lower = new AnimationCut[][]
        {
            null,
            Utility.Instance.CombineAnimationCut(spr_lower[1], new float[] { 0.2f }),
            Utility.Instance.CombineAnimationCut(spr_lower[2], new float[] { 0.2f }),
            Utility.Instance.CombineAnimationCut(spr_lower[3], new float[] { 0.2f }),
            Utility.Instance.CombineAnimationCut(spr_lower[4], new float[] { 0.2f }),
            Utility.Instance.CombineAnimationCut(spr_lower[5], new float[] { 0.2f }),
            Utility.Instance.CombineAnimationCut(spr_lower[6], new float[] { 0.2f }),
            Utility.Instance.CombineAnimationCut(spr_lower[7], new float[] { 0.2f }),
            Utility.Instance.CombineAnimationCut(spr_lower[8], new float[] { 0.2f }),
            Utility.Instance.CombineAnimationCut(spr_lower[9], new float[] { 0.2f }),
        };

        spr_dump = new Sprite[][]
        {
            new Sprite[] { Resources.Load<Sprite>("Sprites/Player/Motion/Idle/idle_28_shadow"), Resources.Load<Sprite>("Sprites/Player/Motion/Idle/idle_1379_shadow") }
        };

        spr_idle_shadow = new Sprite[]
        {
            null,
            spr_dump[0][1],
            spr_dump[0][0],
            spr_dump[0][1],
            spr_dump[0][1],
            spr_dump[0][1],
            spr_dump[0][1],
            spr_dump[0][1],
            spr_dump[0][0],
            spr_dump[0][1]            
        };

        spr_dump = new Sprite[][]
        {
            null,
            Resources.LoadAll<Sprite>("Sprites/Player/Motion/Idle/idle_1_hatface"),
            Resources.LoadAll<Sprite>("Sprites/Player/Motion/Idle/idle_2_hatface"),
            Resources.LoadAll<Sprite>("Sprites/Player/Motion/Idle/idle_3_hatface"),
            Resources.LoadAll<Sprite>("Sprites/Player/Motion/Idle/idle_7_hatface"),
            Resources.LoadAll<Sprite>("Sprites/Player/Motion/Idle/idle_9_hatface")
        };

        spr_idle_hatface = new Sprite[][]
        {
            null,
            spr_dump[1],
            spr_dump[2],
            spr_dump[3],
            spr_dump[1],
            spr_dump[2],
            spr_dump[3],
            spr_dump[4],
            null,
            spr_dump[5],
        };



        // run
        spr_dump = new Sprite[][]
        {
            null,
            Resources.LoadAll<Sprite>("Sprites/Player/Motion/Run/run_1"),
            Resources.LoadAll<Sprite>("Sprites/Player/Motion/Run/run_2"),
            Resources.LoadAll<Sprite>("Sprites/Player/Motion/Run/run_3"),
            Resources.LoadAll<Sprite>("Sprites/Player/Motion/Run/run_7"),
            Resources.LoadAll<Sprite>("Sprites/Player/Motion/Run/run_8"),
            Resources.LoadAll<Sprite>("Sprites/Player/Motion/Run/run_9"),
        };
        spr_upper = new Sprite[][]
        {
            null,
            new Sprite[] { spr_dump[1][0], spr_dump[1][1], spr_dump[1][2], spr_dump[1][3], spr_dump[1][4], spr_dump[1][5]},
            new Sprite[] { spr_dump[2][0], spr_dump[2][1], spr_dump[2][2], spr_dump[2][3], spr_dump[2][4], spr_dump[2][5]},
            new Sprite[] { spr_dump[3][0], spr_dump[3][1], spr_dump[3][2], spr_dump[3][3], spr_dump[3][4], spr_dump[3][5]},
            new Sprite[] { spr_dump[1][0], spr_dump[1][1], spr_dump[1][2], spr_dump[1][3], spr_dump[1][4], spr_dump[1][5]},
            new Sprite[] { spr_dump[2][0], spr_dump[2][1], spr_dump[2][2], spr_dump[2][3], spr_dump[2][4], spr_dump[2][5]},
            new Sprite[] { spr_dump[3][0], spr_dump[3][1], spr_dump[3][2], spr_dump[3][3], spr_dump[3][4], spr_dump[3][5]},
            new Sprite[] { spr_dump[4][0], spr_dump[4][1], spr_dump[4][2], spr_dump[4][3], spr_dump[4][4], spr_dump[4][5]},
            new Sprite[] { spr_dump[5][0], spr_dump[5][1], spr_dump[5][2], spr_dump[5][3], spr_dump[5][4], spr_dump[5][5]},
            new Sprite[] { spr_dump[6][0], spr_dump[6][1], spr_dump[6][2], spr_dump[6][3], spr_dump[6][4], spr_dump[6][5]},
        };
        spr_lower = new Sprite[][]
        {
            null,
            new Sprite[] { spr_dump[1][6], spr_dump[1][7], spr_dump[1][8], spr_dump[1][9], spr_dump[1][10], spr_dump[1][11]},
            new Sprite[] { spr_dump[2][6], spr_dump[2][7], spr_dump[2][8], spr_dump[2][9], spr_dump[2][10], spr_dump[2][11]},
            new Sprite[] { spr_dump[3][6], spr_dump[3][7], spr_dump[3][8], spr_dump[3][9], spr_dump[3][10], spr_dump[3][11]},
            new Sprite[] { spr_dump[1][6], spr_dump[1][7], spr_dump[1][8], spr_dump[1][9], spr_dump[1][10], spr_dump[1][11]},
            new Sprite[] { spr_dump[2][6], spr_dump[2][7], spr_dump[2][8], spr_dump[2][9], spr_dump[2][10], spr_dump[2][11]},
            new Sprite[] { spr_dump[3][6], spr_dump[3][7], spr_dump[3][8], spr_dump[3][9], spr_dump[3][10], spr_dump[3][11]},
            new Sprite[] { spr_dump[4][6], spr_dump[4][7], spr_dump[4][8], spr_dump[4][9], spr_dump[4][10], spr_dump[4][11]},
            new Sprite[] { spr_dump[5][6], spr_dump[5][7], spr_dump[5][8], spr_dump[5][9], spr_dump[5][10], spr_dump[5][11]},
            new Sprite[] { spr_dump[6][6], spr_dump[6][7], spr_dump[6][8], spr_dump[6][9], spr_dump[6][10], spr_dump[6][11]},
        };

        ac_run_motion_upper = new AnimationCut[][]
        {
            null,
            Utility.Instance.CombineAnimationCut(spr_upper[1], new float[] { 0.1f, 0.13f, 0.1f, 0.1f, 0.13f, 0.1f }),
            Utility.Instance.CombineAnimationCut(spr_upper[2], new float[] { 0.1f, 0.13f, 0.1f, 0.1f, 0.13f, 0.1f }),
            Utility.Instance.CombineAnimationCut(spr_upper[3], new float[] { 0.1f, 0.13f, 0.1f, 0.1f, 0.13f, 0.1f }),
            Utility.Instance.CombineAnimationCut(spr_upper[4], new float[] { 0.1f, 0.13f, 0.1f, 0.1f, 0.13f, 0.1f }),
            Utility.Instance.CombineAnimationCut(spr_upper[5], new float[] { 0.1f, 0.13f, 0.1f, 0.1f, 0.13f, 0.1f }),
            Utility.Instance.CombineAnimationCut(spr_upper[6], new float[] { 0.1f, 0.13f, 0.1f, 0.1f, 0.13f, 0.1f }),
            Utility.Instance.CombineAnimationCut(spr_upper[7], new float[] { 0.1f, 0.13f, 0.1f, 0.1f, 0.13f, 0.1f }),
            Utility.Instance.CombineAnimationCut(spr_upper[8], new float[] { 0.1f, 0.13f, 0.1f, 0.1f, 0.13f, 0.1f }),
            Utility.Instance.CombineAnimationCut(spr_upper[9], new float[] { 0.1f, 0.13f, 0.1f, 0.1f, 0.13f, 0.1f }),
        };

        ac_run_motion_lower = new AnimationCut[][]
        {
            null,
            Utility.Instance.CombineAnimationCut(spr_lower[1], new float[] { 0.1f, 0.13f, 0.1f, 0.1f, 0.13f, 0.1f }),
            Utility.Instance.CombineAnimationCut(spr_lower[2], new float[] { 0.1f, 0.13f, 0.1f, 0.1f, 0.13f, 0.1f }),
            Utility.Instance.CombineAnimationCut(spr_lower[3], new float[] { 0.1f, 0.13f, 0.1f, 0.1f, 0.13f, 0.1f }),
            Utility.Instance.CombineAnimationCut(spr_lower[4], new float[] { 0.1f, 0.13f, 0.1f, 0.1f, 0.13f, 0.1f }),
            Utility.Instance.CombineAnimationCut(spr_lower[5], new float[] { 0.1f, 0.13f, 0.1f, 0.1f, 0.13f, 0.1f }),
            Utility.Instance.CombineAnimationCut(spr_lower[6], new float[] { 0.1f, 0.13f, 0.1f, 0.1f, 0.13f, 0.1f }),
            Utility.Instance.CombineAnimationCut(spr_lower[7], new float[] { 0.1f, 0.13f, 0.1f, 0.1f, 0.13f, 0.1f }),
            Utility.Instance.CombineAnimationCut(spr_lower[8], new float[] { 0.1f, 0.13f, 0.1f, 0.1f, 0.13f, 0.1f }),
            Utility.Instance.CombineAnimationCut(spr_lower[9], new float[] { 0.1f, 0.13f, 0.1f, 0.1f, 0.13f, 0.1f }),
        };

        spr_dump = new Sprite[][]
        {
            new Sprite[] { Resources.Load<Sprite>("Sprites/Player/Motion/Run/run_28_shadow"), Resources.Load<Sprite>("Sprites/Player/Motion/Run/run_1379_shadow") }
        };

        spr_run_shadow = new Sprite[]
        {
            null,
            spr_dump[0][1],
            spr_dump[0][0],
            spr_dump[0][1],
            spr_dump[0][1],
            spr_dump[0][1],
            spr_dump[0][1],
            spr_dump[0][1],
            spr_dump[0][0],
            spr_dump[0][1]
        };

        spr_dump = new Sprite[][]
        {
            null,
            Resources.LoadAll<Sprite>("Sprites/Player/Motion/Run/run_1_hatface"),
            Resources.LoadAll<Sprite>("Sprites/Player/Motion/Run/run_2_hatface"),
            Resources.LoadAll<Sprite>("Sprites/Player/Motion/Run/run_3_hatface"),
            Resources.LoadAll<Sprite>("Sprites/Player/Motion/Run/run_7_hatface"),
            Resources.LoadAll<Sprite>("Sprites/Player/Motion/Run/run_9_hatface")
        };

        spr_run_hatface = new Sprite[][]
        {
            null,
            spr_dump[1],
            spr_dump[2],
            spr_dump[3],
            spr_dump[1],
            spr_dump[2],
            spr_dump[3],
            spr_dump[4],
            null,
            spr_dump[5],
        };


        // walk
        spr_dump = new Sprite[][]
        {
            null,
            Resources.LoadAll<Sprite>("Sprites/Player/Motion/Walk/walk_1"),
            Resources.LoadAll<Sprite>("Sprites/Player/Motion/Walk/walk_2"),
            Resources.LoadAll<Sprite>("Sprites/Player/Motion/Walk/walk_3"),
            Resources.LoadAll<Sprite>("Sprites/Player/Motion/Walk/walk_7"),
            Resources.LoadAll<Sprite>("Sprites/Player/Motion/Walk/walk_8"),
            Resources.LoadAll<Sprite>("Sprites/Player/Motion/Walk/walk_9"),
        };
        spr_upper = new Sprite[][]
        {
            null,
            new Sprite[] { spr_dump[1][0], spr_dump[1][1], spr_dump[1][2], spr_dump[1][3], spr_dump[1][4], spr_dump[1][5], spr_dump[1][6], spr_dump[1][7]},
            new Sprite[] { spr_dump[2][0], spr_dump[2][1], spr_dump[2][2], spr_dump[2][3], spr_dump[2][4], spr_dump[2][5], spr_dump[2][6], spr_dump[2][7]},
            new Sprite[] { spr_dump[3][0], spr_dump[3][1], spr_dump[3][2], spr_dump[3][3], spr_dump[3][4], spr_dump[3][5], spr_dump[3][6], spr_dump[3][7]},
            new Sprite[] { spr_dump[1][0], spr_dump[1][1], spr_dump[1][2], spr_dump[1][3], spr_dump[1][4], spr_dump[1][5], spr_dump[1][6], spr_dump[1][7]},
            new Sprite[] { spr_dump[2][0], spr_dump[2][1], spr_dump[2][2], spr_dump[2][3], spr_dump[2][4], spr_dump[2][5], spr_dump[2][6], spr_dump[2][7]},
            new Sprite[] { spr_dump[3][0], spr_dump[3][1], spr_dump[3][2], spr_dump[3][3], spr_dump[3][4], spr_dump[3][5], spr_dump[3][6], spr_dump[3][7]},
            new Sprite[] { spr_dump[4][0], spr_dump[4][1], spr_dump[4][2], spr_dump[4][3], spr_dump[4][4], spr_dump[4][5], spr_dump[4][6], spr_dump[4][7]},
            new Sprite[] { spr_dump[5][0], spr_dump[5][1], spr_dump[5][2], spr_dump[5][3], spr_dump[5][4], spr_dump[5][5], spr_dump[5][6], spr_dump[5][7]},
            new Sprite[] { spr_dump[6][0], spr_dump[6][1], spr_dump[6][2], spr_dump[6][3], spr_dump[6][4], spr_dump[6][5], spr_dump[6][6], spr_dump[6][7]},
        };
        spr_lower = new Sprite[][]
        {
            null,
            new Sprite[] { spr_dump[1][8], spr_dump[1][9], spr_dump[1][10], spr_dump[1][11], spr_dump[1][12], spr_dump[1][13], spr_dump[1][14], spr_dump[1][15]},
            new Sprite[] { spr_dump[2][8], spr_dump[2][9], spr_dump[2][10], spr_dump[2][11], spr_dump[2][12], spr_dump[2][13], spr_dump[2][14], spr_dump[2][15]},
            new Sprite[] { spr_dump[3][8], spr_dump[3][9], spr_dump[3][10], spr_dump[3][11], spr_dump[3][12], spr_dump[3][13], spr_dump[3][14], spr_dump[3][15]},
            new Sprite[] { spr_dump[1][8], spr_dump[1][9], spr_dump[1][10], spr_dump[1][11], spr_dump[1][12], spr_dump[1][13], spr_dump[1][14], spr_dump[1][15]},
            new Sprite[] { spr_dump[2][8], spr_dump[2][9], spr_dump[2][10], spr_dump[2][11], spr_dump[2][12], spr_dump[2][13], spr_dump[2][14], spr_dump[2][15]},
            new Sprite[] { spr_dump[3][8], spr_dump[3][9], spr_dump[3][10], spr_dump[3][11], spr_dump[3][12], spr_dump[3][13], spr_dump[3][14], spr_dump[3][15]},
            new Sprite[] { spr_dump[4][8], spr_dump[4][9], spr_dump[4][10], spr_dump[4][11], spr_dump[4][12], spr_dump[4][13], spr_dump[4][14], spr_dump[4][15]},
            new Sprite[] { spr_dump[5][8], spr_dump[5][9], spr_dump[5][10], spr_dump[5][11], spr_dump[5][12], spr_dump[5][13], spr_dump[5][14], spr_dump[5][15]},
            new Sprite[] { spr_dump[6][8], spr_dump[6][9], spr_dump[6][10], spr_dump[6][11], spr_dump[6][12], spr_dump[6][13], spr_dump[6][14], spr_dump[6][15]},
        };

        ac_walk_motion_upper = new AnimationCut[][]
        {
            null,
            Utility.Instance.CombineAnimationCut(spr_upper[1], new float[] { 0.16f }),
            Utility.Instance.CombineAnimationCut(spr_upper[2], new float[] { 0.16f }),
            Utility.Instance.CombineAnimationCut(spr_upper[3], new float[] { 0.16f }),
            Utility.Instance.CombineAnimationCut(spr_upper[4], new float[] { 0.16f }),
            Utility.Instance.CombineAnimationCut(spr_upper[5], new float[] { 0.16f }),
            Utility.Instance.CombineAnimationCut(spr_upper[6], new float[] { 0.16f }),
            Utility.Instance.CombineAnimationCut(spr_upper[7], new float[] { 0.16f }),
            Utility.Instance.CombineAnimationCut(spr_upper[8], new float[] { 0.16f }),
            Utility.Instance.CombineAnimationCut(spr_upper[9], new float[] { 0.16f }),
        };

        ac_walk_motion_lower = new AnimationCut[][]
        {
            null,
            Utility.Instance.CombineAnimationCut(spr_lower[1], new float[] { 0.16f }),
            Utility.Instance.CombineAnimationCut(spr_lower[2], new float[] { 0.16f }),
            Utility.Instance.CombineAnimationCut(spr_lower[3], new float[] { 0.16f }),
            Utility.Instance.CombineAnimationCut(spr_lower[4], new float[] { 0.16f }),
            Utility.Instance.CombineAnimationCut(spr_lower[5], new float[] { 0.16f }),
            Utility.Instance.CombineAnimationCut(spr_lower[6], new float[] { 0.16f }),
            Utility.Instance.CombineAnimationCut(spr_lower[7], new float[] { 0.16f }),
            Utility.Instance.CombineAnimationCut(spr_lower[8], new float[] { 0.16f }),
            Utility.Instance.CombineAnimationCut(spr_lower[9], new float[] { 0.16f }),
        };

        spr_walk_shadow = spr_run_shadow;

        spr_dump = new Sprite[][]
        {
            null,
            Resources.LoadAll<Sprite>("Sprites/Player/Motion/Walk/walk_1_hatface"),
            Resources.LoadAll<Sprite>("Sprites/Player/Motion/Walk/walk_2_hatface"),
            Resources.LoadAll<Sprite>("Sprites/Player/Motion/Walk/walk_3_hatface"),
            Resources.LoadAll<Sprite>("Sprites/Player/Motion/Walk/walk_7_hatface"),
            Resources.LoadAll<Sprite>("Sprites/Player/Motion/Walk/walk_9_hatface")
        };

        spr_walk_hatface = new Sprite[][]
        {
            null,
            spr_dump[1],
            spr_dump[2],
            spr_dump[3],
            spr_dump[1],
            spr_dump[2],
            spr_dump[3],
            spr_dump[4],
            null,
            spr_dump[5],
        };


        // hoe        
        spr_dump = new Sprite[][]
        {
            null,
            Resources.LoadAll<Sprite>("Sprites/Player/Motion/Hoe/hoe_1"),
            Resources.LoadAll<Sprite>("Sprites/Player/Motion/Hoe/hoe_2_upper"),            
            Resources.LoadAll<Sprite>("Sprites/Player/Motion/Hoe/hoe_3"),
            Resources.LoadAll<Sprite>("Sprites/Player/Motion/Hoe/hoe_7"),
            Resources.LoadAll<Sprite>("Sprites/Player/Motion/Hoe/hoe_8_upper"),
            Resources.LoadAll<Sprite>("Sprites/Player/Motion/Hoe/hoe_9"),
            Resources.LoadAll<Sprite>("Sprites/Player/Motion/Hoe/hoe_2_lower"),
            Resources.LoadAll<Sprite>("Sprites/Player/Motion/Hoe/hoe_8_lower"),
        };
        spr_upper = new Sprite[][]
        {
            null,
            new Sprite[] { spr_dump[1][0], spr_dump[1][1], spr_dump[1][2], spr_dump[1][3], spr_dump[1][4], spr_dump[1][5]},
            new Sprite[] { spr_dump[2][0], spr_dump[2][1], spr_dump[2][2], spr_dump[2][3], spr_dump[2][4], spr_dump[2][5]},
            new Sprite[] { spr_dump[3][0], spr_dump[3][1], spr_dump[3][2], spr_dump[3][3], spr_dump[3][4], spr_dump[3][5]},
            new Sprite[] { spr_dump[1][0], spr_dump[1][1], spr_dump[1][2], spr_dump[1][3], spr_dump[1][4], spr_dump[1][5]},
            new Sprite[] { spr_dump[2][0], spr_dump[2][1], spr_dump[2][2], spr_dump[2][3], spr_dump[2][4], spr_dump[2][5]},
            new Sprite[] { spr_dump[3][0], spr_dump[3][1], spr_dump[3][2], spr_dump[3][3], spr_dump[3][4], spr_dump[3][5]},
            new Sprite[] { spr_dump[4][0], spr_dump[4][1], spr_dump[4][2], spr_dump[4][3], spr_dump[4][4], spr_dump[4][5]},
            new Sprite[] { spr_dump[5][0], spr_dump[5][1], spr_dump[5][2], spr_dump[5][3], spr_dump[5][4], spr_dump[5][5]},
            new Sprite[] { spr_dump[6][0], spr_dump[6][1], spr_dump[6][2], spr_dump[6][3], spr_dump[6][4], spr_dump[6][5]},
        };
        spr_lower = new Sprite[][]
        {
            null,
            new Sprite[] { spr_dump[1][6], spr_dump[1][7], spr_dump[1][8], spr_dump[1][9], spr_dump[1][10], spr_dump[1][11]},
            new Sprite[] { spr_dump[7][0], spr_dump[7][1], spr_dump[7][2], spr_dump[7][3], spr_dump[7][4], spr_dump[7][5]},
            new Sprite[] { spr_dump[3][6], spr_dump[3][7], spr_dump[3][8], spr_dump[3][9], spr_dump[3][10], spr_dump[3][11]},
            new Sprite[] { spr_dump[1][6], spr_dump[1][7], spr_dump[1][8], spr_dump[1][9], spr_dump[1][10], spr_dump[1][11]},
            new Sprite[] { spr_dump[7][0], spr_dump[7][1], spr_dump[7][2], spr_dump[7][3], spr_dump[7][4], spr_dump[7][5]},
            new Sprite[] { spr_dump[3][6], spr_dump[3][7], spr_dump[3][8], spr_dump[3][9], spr_dump[3][10], spr_dump[3][11]},
            new Sprite[] { spr_dump[4][6], spr_dump[4][7], spr_dump[4][8], spr_dump[4][9], spr_dump[4][10], spr_dump[4][11]},
            new Sprite[] { spr_dump[8][0], spr_dump[8][1], spr_dump[8][2], spr_dump[8][3], spr_dump[8][4], spr_dump[8][5]},
            new Sprite[] { spr_dump[6][6], spr_dump[6][7], spr_dump[6][8], spr_dump[6][9], spr_dump[6][10], spr_dump[6][11]},
        };

        ac_hoe_motion_upper = new AnimationCut[][]
        {
            null,
            Utility.Instance.CombineAnimationCut(spr_upper[1], new float[] { 0.1f, 0.1f, 0.075f, 0.075f, 0.1f, 0.1f }),
            Utility.Instance.CombineAnimationCut(spr_upper[2], new float[] { 0.1f, 0.1f, 0.075f, 0.075f, 0.1f, 0.1f }),
            Utility.Instance.CombineAnimationCut(spr_upper[3], new float[] { 0.1f, 0.1f, 0.075f, 0.075f, 0.1f, 0.1f }),
            Utility.Instance.CombineAnimationCut(spr_upper[4], new float[] { 0.1f, 0.1f, 0.075f, 0.075f, 0.1f, 0.1f }),
            Utility.Instance.CombineAnimationCut(spr_upper[5], new float[] { 0.1f, 0.1f, 0.075f, 0.075f, 0.1f, 0.1f }),
            Utility.Instance.CombineAnimationCut(spr_upper[6], new float[] { 0.1f, 0.1f, 0.075f, 0.075f, 0.1f, 0.1f }),
            Utility.Instance.CombineAnimationCut(spr_upper[7], new float[] { 0.1f, 0.1f, 0.075f, 0.075f, 0.1f, 0.1f }),
            Utility.Instance.CombineAnimationCut(spr_upper[8], new float[] { 0.1f, 0.1f, 0.075f, 0.075f, 0.1f, 0.1f }),
            Utility.Instance.CombineAnimationCut(spr_upper[9], new float[] { 0.1f, 0.1f, 0.075f, 0.075f, 0.1f, 0.1f }),
        };

        ac_hoe_motion_lower = new AnimationCut[][]
        {
            null,
            Utility.Instance.CombineAnimationCut(spr_lower[1], new float[] { 0.1f, 0.1f, 0.075f, 0.075f, 0.1f, 0.1f }),
            Utility.Instance.CombineAnimationCut(spr_lower[2], new float[] { 0.1f, 0.1f, 0.075f, 0.075f, 0.1f, 0.1f }),
            Utility.Instance.CombineAnimationCut(spr_lower[3], new float[] { 0.1f, 0.1f, 0.075f, 0.075f, 0.1f, 0.1f }),
            Utility.Instance.CombineAnimationCut(spr_lower[4], new float[] { 0.1f, 0.1f, 0.075f, 0.075f, 0.1f, 0.1f }),
            Utility.Instance.CombineAnimationCut(spr_lower[5], new float[] { 0.1f, 0.1f, 0.075f, 0.075f, 0.1f, 0.1f }),
            Utility.Instance.CombineAnimationCut(spr_lower[6], new float[] { 0.1f, 0.1f, 0.075f, 0.075f, 0.1f, 0.1f }),
            Utility.Instance.CombineAnimationCut(spr_lower[7], new float[] { 0.1f, 0.1f, 0.075f, 0.075f, 0.1f, 0.1f }),
            Utility.Instance.CombineAnimationCut(spr_lower[8], new float[] { 0.1f, 0.1f, 0.075f, 0.075f, 0.1f, 0.1f }),
            Utility.Instance.CombineAnimationCut(spr_lower[9], new float[] { 0.1f, 0.1f, 0.075f, 0.075f, 0.1f, 0.1f }),
        };        

        spr_hoe_shadow = spr_idle_shadow;

        spr_dump = new Sprite[][]
        {
            null,
            Resources.LoadAll<Sprite>("Sprites/Player/Motion/Hoe/hoe_1_hatface"),
            Resources.LoadAll<Sprite>("Sprites/Player/Motion/Hoe/hoe_2_hatface"),
            Resources.LoadAll<Sprite>("Sprites/Player/Motion/Hoe/hoe_3_hatface"),
            Resources.LoadAll<Sprite>("Sprites/Player/Motion/Hoe/hoe_7_hatface"),
            Resources.LoadAll<Sprite>("Sprites/Player/Motion/Hoe/hoe_9_hatface")
        };

        spr_hoe_hatface = new Sprite[][]
        {
            null,
            spr_dump[1],
            spr_dump[2],
            spr_dump[3],
            spr_dump[1],
            spr_dump[2],
            spr_dump[3],
            spr_dump[4],
            null,
            spr_dump[5],
        };

        spr_dump = new Sprite[][]
        {
            Resources.LoadAll<Sprite>("Sprites/Player/Motion/Hoe/hoe_1_fx_ground"),
            Resources.LoadAll<Sprite>("Sprites/Player/Motion/Hoe/hoe_2_fx_ground"),
            Resources.LoadAll<Sprite>("Sprites/Player/Motion/Hoe/hoe_3_fx_ground"),
            Resources.LoadAll<Sprite>("Sprites/Player/Motion/Hoe/hoe_7_fx_ground"),
            Resources.LoadAll<Sprite>("Sprites/Player/Motion/Hoe/hoe_8_fx_ground"),
            Resources.LoadAll<Sprite>("Sprites/Player/Motion/Hoe/hoe_9_fx_ground")
        };

        spr_hoe_fx_ground = new Sprite[][]
        {
            null,
            spr_dump[0],
            spr_dump[1],
            spr_dump[2],
            spr_dump[0],
            spr_dump[1],
            spr_dump[2],
            spr_dump[3],
            spr_dump[4],
            spr_dump[5]
        };

        spr_dump = new Sprite[][]
        {
            Resources.LoadAll<Sprite>("Sprites/Player/Motion/Hoe/hoe_1_fx_swing"),
            Resources.LoadAll<Sprite>("Sprites/Player/Motion/Hoe/hoe_2_fx_swing"),
            Resources.LoadAll<Sprite>("Sprites/Player/Motion/Hoe/hoe_3_fx_swing"),
            Resources.LoadAll<Sprite>("Sprites/Player/Motion/Hoe/hoe_7_fx_swing"),
            Resources.LoadAll<Sprite>("Sprites/Player/Motion/Hoe/hoe_8_fx_swing"),
            Resources.LoadAll<Sprite>("Sprites/Player/Motion/Hoe/hoe_9_fx_swing")
        };

        spr_hoe_fx_swing = new Sprite[][]
        {
            null,
            spr_dump[0],
            spr_dump[1],
            spr_dump[2],
            spr_dump[0],
            spr_dump[1],
            spr_dump[2],
            spr_dump[3],
            spr_dump[4],
            spr_dump[5]
        };


        // axe
        spr_dump = new Sprite[][]
        {
            null,
            Resources.LoadAll<Sprite>("Sprites/Player/Motion/Axe/ax_1"),
            Resources.LoadAll<Sprite>("Sprites/Player/Motion/Axe/ax_2_upper"),
            Resources.LoadAll<Sprite>("Sprites/Player/Motion/Axe/ax_3"),
            Resources.LoadAll<Sprite>("Sprites/Player/Motion/Axe/ax_7_upper"),
            Resources.LoadAll<Sprite>("Sprites/Player/Motion/Axe/ax_8_upper"),
            Resources.LoadAll<Sprite>("Sprites/Player/Motion/Axe/ax_9_upper"),
            Resources.LoadAll<Sprite>("Sprites/Player/Motion/Axe/ax_2_lower"),
            Resources.LoadAll<Sprite>("Sprites/Player/Motion/Axe/ax_8_lower"),
            Resources.LoadAll<Sprite>("Sprites/Player/Motion/Axe/ax_7_lower"),
            Resources.LoadAll<Sprite>("Sprites/Player/Motion/Axe/ax_9_lower"),
        };
        spr_upper = new Sprite[][]
        {
            null,
            new Sprite[] { spr_dump[1][0], spr_dump[1][1], spr_dump[1][2], spr_dump[1][3], spr_dump[1][4], spr_dump[1][5] },
            new Sprite[] { spr_dump[2][0], spr_dump[2][1], spr_dump[2][2], spr_dump[2][3], spr_dump[2][4], spr_dump[2][5] },
            new Sprite[] { spr_dump[3][0], spr_dump[3][1], spr_dump[3][2], spr_dump[3][3], spr_dump[3][4], spr_dump[3][5] },
            new Sprite[] { spr_dump[1][0], spr_dump[1][1], spr_dump[1][2], spr_dump[1][3], spr_dump[1][4], spr_dump[1][5] },
            new Sprite[] { spr_dump[2][0], spr_dump[2][1], spr_dump[2][2], spr_dump[2][3], spr_dump[2][4], spr_dump[2][5] },
            new Sprite[] { spr_dump[3][0], spr_dump[3][1], spr_dump[3][2], spr_dump[3][3], spr_dump[3][4], spr_dump[3][5] },
            new Sprite[] { spr_dump[4][0], spr_dump[4][1], spr_dump[4][2], spr_dump[4][3], spr_dump[4][4], spr_dump[4][5] },
            new Sprite[] { spr_dump[5][0], spr_dump[5][1], spr_dump[5][2], spr_dump[5][3], spr_dump[5][4], spr_dump[5][5] },
            new Sprite[] { spr_dump[6][0], spr_dump[6][1], spr_dump[6][2], spr_dump[6][3], spr_dump[6][4], spr_dump[6][5] },
        };
        spr_lower = new Sprite[][]
        {
            null,
            new Sprite[] { spr_dump[1][6], spr_dump[1][7], spr_dump[1][8], spr_dump[1][9], spr_dump[1][10], spr_dump[1][11]},
            new Sprite[] { spr_dump[7][0], spr_dump[7][1], spr_dump[7][2], spr_dump[7][3], spr_dump[7][4], spr_dump[7][5]},
            new Sprite[] { spr_dump[3][6], spr_dump[3][7], spr_dump[3][8], spr_dump[3][9], spr_dump[3][10], spr_dump[3][11]},
            new Sprite[] { spr_dump[1][6], spr_dump[1][7], spr_dump[1][8], spr_dump[1][9], spr_dump[1][10], spr_dump[1][11]},
            new Sprite[] { spr_dump[7][0], spr_dump[7][1], spr_dump[7][2], spr_dump[7][3], spr_dump[7][4], spr_dump[7][5]},
            new Sprite[] { spr_dump[3][6], spr_dump[3][7], spr_dump[3][8], spr_dump[3][9], spr_dump[3][10], spr_dump[3][11]},
            new Sprite[] { spr_dump[9][0], spr_dump[9][1], spr_dump[9][2], spr_dump[9][3], spr_dump[9][4], spr_dump[9][5]},
            new Sprite[] { spr_dump[8][0], spr_dump[8][1], spr_dump[8][2], spr_dump[8][3], spr_dump[8][4], spr_dump[8][5]},
            new Sprite[] { spr_dump[10][0], spr_dump[10][1], spr_dump[10][2], spr_dump[10][3], spr_dump[10][4], spr_dump[10][5]},
        };

        ac_axe_motion_upper = new AnimationCut[][]
        {
            null,
            Utility.Instance.CombineAnimationCut(spr_upper[1], new float[] { 0.1f, 0.1f, 0.075f, 0.075f, 0.1f, 0.1f }),
            Utility.Instance.CombineAnimationCut(spr_upper[2], new float[] { 0.1f, 0.1f, 0.075f, 0.075f, 0.1f, 0.1f }),
            Utility.Instance.CombineAnimationCut(spr_upper[3], new float[] { 0.1f, 0.1f, 0.075f, 0.075f, 0.1f, 0.1f }),
            Utility.Instance.CombineAnimationCut(spr_upper[4], new float[] { 0.1f, 0.1f, 0.075f, 0.075f, 0.1f, 0.1f }),
            Utility.Instance.CombineAnimationCut(spr_upper[5], new float[] { 0.1f, 0.1f, 0.075f, 0.075f, 0.1f, 0.1f }),
            Utility.Instance.CombineAnimationCut(spr_upper[6], new float[] { 0.1f, 0.1f, 0.075f, 0.075f, 0.1f, 0.1f }),
            Utility.Instance.CombineAnimationCut(spr_upper[7], new float[] { 0.1f, 0.1f, 0.075f, 0.075f, 0.1f, 0.1f }),
            Utility.Instance.CombineAnimationCut(spr_upper[8], new float[] { 0.1f, 0.1f, 0.075f, 0.075f, 0.1f, 0.1f }),
            Utility.Instance.CombineAnimationCut(spr_upper[9], new float[] { 0.1f, 0.1f, 0.075f, 0.075f, 0.1f, 0.1f }),
        };

        ac_axe_motion_lower = new AnimationCut[][]
        {
            null,
            Utility.Instance.CombineAnimationCut(spr_lower[1], new float[] { 0.1f, 0.1f, 0.075f, 0.075f, 0.1f, 0.1f }),
            Utility.Instance.CombineAnimationCut(spr_lower[2], new float[] { 0.1f, 0.1f, 0.075f, 0.075f, 0.1f, 0.1f }),
            Utility.Instance.CombineAnimationCut(spr_lower[3], new float[] { 0.1f, 0.1f, 0.075f, 0.075f, 0.1f, 0.1f }),
            Utility.Instance.CombineAnimationCut(spr_lower[4], new float[] { 0.1f, 0.1f, 0.075f, 0.075f, 0.1f, 0.1f }),
            Utility.Instance.CombineAnimationCut(spr_lower[5], new float[] { 0.1f, 0.1f, 0.075f, 0.075f, 0.1f, 0.1f }),
            Utility.Instance.CombineAnimationCut(spr_lower[6], new float[] { 0.1f, 0.1f, 0.075f, 0.075f, 0.1f, 0.1f }),
            Utility.Instance.CombineAnimationCut(spr_lower[7], new float[] { 0.1f, 0.1f, 0.075f, 0.075f, 0.1f, 0.1f }),
            Utility.Instance.CombineAnimationCut(spr_lower[8], new float[] { 0.1f, 0.1f, 0.075f, 0.075f, 0.1f, 0.1f }),
            Utility.Instance.CombineAnimationCut(spr_lower[9], new float[] { 0.1f, 0.1f, 0.075f, 0.075f, 0.1f, 0.1f }),
        };

        spr_axe_shadow = spr_idle_shadow;
        spr_axe_hatface = spr_hoe_hatface;

        spr_dump = new Sprite[][]
        {
            Resources.LoadAll<Sprite>("Sprites/Player/Motion/Axe/ax_1_fx_swing"),
            Resources.LoadAll<Sprite>("Sprites/Player/Motion/Axe/ax_2_fx_swing"),
            Resources.LoadAll<Sprite>("Sprites/Player/Motion/Axe/ax_3_fx_swing"),
            Resources.LoadAll<Sprite>("Sprites/Player/Motion/Axe/ax_7_fx_swing"),
            Resources.LoadAll<Sprite>("Sprites/Player/Motion/Axe/ax_8_fx_swing"),
            Resources.LoadAll<Sprite>("Sprites/Player/Motion/Axe/ax_9_fx_swing")
        };

        spr_axe_fx_swing = new Sprite[][]
        {
            null,
            spr_dump[0],
            spr_dump[1],
            spr_dump[2],
            spr_dump[0],
            spr_dump[1],
            spr_dump[2],
            spr_dump[3],
            spr_dump[4],
            spr_dump[5]
        };



        // watering      
        spr_dump = new Sprite[][]
        {
            null,
            Resources.LoadAll<Sprite>("Sprites/Player/Motion/Watering/watering_1"),
            Resources.LoadAll<Sprite>("Sprites/Player/Motion/Watering/watering_2"),
            Resources.LoadAll<Sprite>("Sprites/Player/Motion/Watering/watering_3"),
            Resources.LoadAll<Sprite>("Sprites/Player/Motion/Watering/watering_7"),
            Resources.LoadAll<Sprite>("Sprites/Player/Motion/Watering/watering_8"),
            Resources.LoadAll<Sprite>("Sprites/Player/Motion/Watering/watering_9"),
        };

        spr_upper = new Sprite[][]
        {
            null,
            new Sprite[] { spr_dump[1][0], spr_dump[1][1], spr_dump[1][2], spr_dump[1][3], spr_dump[1][4], spr_dump[1][5]},
            new Sprite[] { spr_dump[2][0], spr_dump[2][1], spr_dump[2][2], spr_dump[2][3], spr_dump[2][4], spr_dump[2][5]},
            new Sprite[] { spr_dump[3][0], spr_dump[3][1], spr_dump[3][2], spr_dump[3][3], spr_dump[3][4], spr_dump[3][5]},
            new Sprite[] { spr_dump[1][0], spr_dump[1][1], spr_dump[1][2], spr_dump[1][3], spr_dump[1][4], spr_dump[1][5]},
            new Sprite[] { spr_dump[2][0], spr_dump[2][1], spr_dump[2][2], spr_dump[2][3], spr_dump[2][4], spr_dump[2][5]},
            new Sprite[] { spr_dump[3][0], spr_dump[3][1], spr_dump[3][2], spr_dump[3][3], spr_dump[3][4], spr_dump[3][5]},
            new Sprite[] { spr_dump[4][0], spr_dump[4][1], spr_dump[4][2], spr_dump[4][3], spr_dump[4][4], spr_dump[4][5]},
            new Sprite[] { spr_dump[5][0], spr_dump[5][1], spr_dump[5][2], spr_dump[5][3], spr_dump[5][4], spr_dump[5][5]},
            new Sprite[] { spr_dump[6][0], spr_dump[6][1], spr_dump[6][2], spr_dump[6][3], spr_dump[6][4], spr_dump[6][5]},
        };
        spr_lower = new Sprite[][]
        {
            null,
            new Sprite[] { spr_dump[1][6], spr_dump[1][7], spr_dump[1][8], spr_dump[1][9], spr_dump[1][10], spr_dump[1][11]},
            null,
            new Sprite[] { spr_dump[3][6], spr_dump[3][7], spr_dump[3][8], spr_dump[3][9], spr_dump[3][10], spr_dump[3][11]},
            new Sprite[] { spr_dump[1][6], spr_dump[1][7], spr_dump[1][8], spr_dump[1][9], spr_dump[1][10], spr_dump[1][11]},
            null,
            new Sprite[] { spr_dump[3][6], spr_dump[3][7], spr_dump[3][8], spr_dump[3][9], spr_dump[3][10], spr_dump[3][11]},
            new Sprite[] { spr_dump[4][6], spr_dump[4][7], spr_dump[4][8], spr_dump[4][9], spr_dump[4][10], spr_dump[4][11]},
            new Sprite[] { spr_dump[5][6], spr_dump[5][7], spr_dump[5][8], spr_dump[5][9], spr_dump[5][10], spr_dump[5][11]},
            new Sprite[] { spr_dump[6][6], spr_dump[6][7], spr_dump[6][8], spr_dump[6][9], spr_dump[6][10], spr_dump[6][11]},
        };

        ac_wat_motion_upper = new AnimationCut[][]
        {
            null,
            Utility.Instance.CombineAnimationCut(spr_upper[1], new float[] { 0.1f }),
            Utility.Instance.CombineAnimationCut(spr_upper[2], new float[] { 0.1f }),
            Utility.Instance.CombineAnimationCut(spr_upper[3], new float[] { 0.1f }),
            Utility.Instance.CombineAnimationCut(spr_upper[4], new float[] { 0.1f }),
            Utility.Instance.CombineAnimationCut(spr_upper[5], new float[] { 0.1f }),
            Utility.Instance.CombineAnimationCut(spr_upper[6], new float[] { 0.1f }),
            Utility.Instance.CombineAnimationCut(spr_upper[7], new float[] { 0.1f }),
            Utility.Instance.CombineAnimationCut(spr_upper[8], new float[] { 0.1f }),
            Utility.Instance.CombineAnimationCut(spr_upper[9], new float[] { 0.1f }),
        };

        ac_wat_motion_lower = new AnimationCut[][]
        {
            null,
            Utility.Instance.CombineAnimationCut(spr_lower[1], new float[] { 0.1f }),
            null,
            Utility.Instance.CombineAnimationCut(spr_lower[3], new float[] { 0.1f }),
            Utility.Instance.CombineAnimationCut(spr_lower[4], new float[] { 0.1f }),
            null,
            Utility.Instance.CombineAnimationCut(spr_lower[6], new float[] { 0.1f }),
            Utility.Instance.CombineAnimationCut(spr_lower[7], new float[] { 0.1f }),
            Utility.Instance.CombineAnimationCut(spr_lower[8], new float[] { 0.1f }),
            Utility.Instance.CombineAnimationCut(spr_lower[9], new float[] { 0.1f }),
        };

        spr_wat_shadow = spr_idle_shadow;

        spr_dump = new Sprite[][]
        {
            null,
            Resources.LoadAll<Sprite>("Sprites/Player/Motion/Watering/watering_1_hatface"),
            Resources.LoadAll<Sprite>("Sprites/Player/Motion/Watering/watering_2_hatface"),
            Resources.LoadAll<Sprite>("Sprites/Player/Motion/Watering/watering_3_hatface"),
            Resources.LoadAll<Sprite>("Sprites/Player/Motion/Watering/watering_7_hatface"),
            Resources.LoadAll<Sprite>("Sprites/Player/Motion/Watering/watering_9_hatface")
        };

        spr_wat_hatface = new Sprite[][]
        {
            null,
            spr_dump[1],
            spr_dump[2],
            spr_dump[3],
            spr_dump[1],
            spr_dump[2],
            spr_dump[3],
            spr_dump[4],
            null,
            spr_dump[5],
        };

        spr_dump = new Sprite[][]
        {
            Resources.LoadAll<Sprite>("Sprites/Player/Motion/Watering/watering_1_fx_water"),
            Resources.LoadAll<Sprite>("Sprites/Player/Motion/Watering/watering_2_fx_water"),
            Resources.LoadAll<Sprite>("Sprites/Player/Motion/Watering/watering_3_fx_water"),
            Resources.LoadAll<Sprite>("Sprites/Player/Motion/Watering/watering_7_fx_water"),
            Resources.LoadAll<Sprite>("Sprites/Player/Motion/Watering/watering_9_fx_water")
        };

        spr_wat_fx_water = new Sprite[][]
        {
            null,
            spr_dump[0],
            spr_dump[1],
            spr_dump[2],
            spr_dump[0],
            spr_dump[1],
            spr_dump[2],
            spr_dump[3],
            null,
            spr_dump[4]
        };
    }



}
