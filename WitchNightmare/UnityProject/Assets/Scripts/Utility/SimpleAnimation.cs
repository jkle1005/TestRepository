using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SimpleAnimation : MonoBehaviour {
    private SpriteRenderer sRenderer;
    private Image image;
    private Transform parents_ori;
    private Transform parents_now;

    
    public bool ui;
    public Sprite[] spr;
    public float[] delay;
    public bool always;
    public bool oneTime;
    public bool delete;

    private bool stop;
   

    // player 위치 보정 전용
    private float rsv;

    private Sprite[] ani_spr;
    private bool ani_color_con;
    private Color32[] ani_color;
    private int ani_nowNum;
    private int ani_count;

    private Transform trk_trf;
    private bool trk_now;

    private IEnumerator crt_animate;

    public void Stop()
    {
        if (crt_animate == null)
            return;

        StopCoroutine(crt_animate);
        init();
    }

    public void Animate()
    {
        if (spr.Length == 0)
            Debug.LogError("SimpleAnimation::Animate() in " + transform.name);
        else
            StartCoroutine(simpleAnimation());
    }

    public void AnimateLocal(bool _light, Vector3 _pos, AnimationCut[] _ac, int _count = 0, bool _del = false, Transform _trf = null, bool _tracking = false, float _rsv = 0f)
    {
        if (ui)
            image.sprite = null;
        else
            sRenderer.sprite = null;

        transform.localPosition = _pos;
        transform.localScale = Vector3.one;

        parents_now = transform.parent;
        parents_ori = transform.parent;

        List<Sprite> sprs = new List<Sprite>();
        List<float> delays = new List<float>();

        for (int i = 0; i < _ac.Length; i++)
        {
            sprs.Add(_ac[i].spr);
            delays.Add(_ac[i].delay);
        }

        ani_spr = sprs.ToArray();
        delay = delays.ToArray();
        ani_nowNum = 0;
        ani_count = _count;
        delete = _del;

        if (!ui)
        {
            if (_light)
                sRenderer.material = GameManager.Instance.mat_light;
            else
                sRenderer.material = GameManager.Instance.mat_nonLight;
        }

        trk_now = _tracking;
        trk_trf = _trf;

        rsv = _rsv;

        crt_animate = animate();
        StartCoroutine(crt_animate);
    }

    public void Animate(bool _light, Vector3 _pos, Sprite[] _spr, float[] _delay, int _count = 0, bool _del = false, Transform _trf = null, bool _tracking = false, float _rsv = 0f)
    {
        if (ui)
            image.sprite = null;
        else
            sRenderer.sprite = null;

        transform.position = _pos;
        transform.localScale = Vector3.one;

        parents_now = transform.parent;
        parents_ori = transform.parent;

        ani_spr = _spr;
        delay = _delay;
        ani_nowNum = 0;
        ani_count = _count;
        delete = _del;

        trk_now = _tracking;
        trk_trf = _trf;

        rsv = _rsv;

        if (!ui)
        {
            if (_light)
                sRenderer.material = GameManager.Instance.mat_light;
            else
                sRenderer.material = GameManager.Instance.mat_nonLight;
        }

        crt_animate = animate();
        StartCoroutine(crt_animate);
    }

    public void Animate(bool _light, Vector3 _pos, AnimationCut[] _ac, int _count = 0, bool _del = false, Transform _trf = null, bool _tracking = false, float _rsv = 0f)
    {
        if (ui)
            image.sprite = null;
        else
            sRenderer.sprite = null;

        transform.position = _pos;
        transform.localScale = Vector3.one;

        parents_now = transform.parent;
        parents_ori = transform.parent;

        List<Sprite> sprs = new List<Sprite>();
        List<float> delays = new List<float>();

        for(int i = 0; i < _ac.Length; i++)
        {
            sprs.Add(_ac[i].spr);
            delays.Add(_ac[i].delay);
        }

        ani_spr = sprs.ToArray();
        delay = delays.ToArray();
        ani_nowNum = 0;
        ani_count = _count;
        delete = _del;

        if (!ui)
        {
            if (_light)
                sRenderer.material = GameManager.Instance.mat_light;
            else
                sRenderer.material = GameManager.Instance.mat_nonLight;
        }

        trk_now = _tracking;
        trk_trf = _trf;

        rsv = _rsv;

        crt_animate = animate();
        StartCoroutine(crt_animate);
    }

    public void Animate(bool _light, Vector3 _pos, AnimationCut[] _ac, Color32[] _col, int _layer)
    {
        if (_ac.Length != _col.Length)
            Debug.LogError("SimpleAnimation::Animate:: Not equle _col and _ac");

        if (ui)
            image.sprite = null;
        else
            sRenderer.sprite = null;

        transform.position = _pos;
        transform.localScale = Vector3.one;

        parents_now = transform.parent;
        parents_ori = transform.parent;

        List<Sprite> sprs = new List<Sprite>();
        List<float> delays = new List<float>();

        for (int i = 0; i < _ac.Length; i++)
        {
            sprs.Add(_ac[i].spr);
            delays.Add(_ac[i].delay);
        }

        ani_spr = sprs.ToArray();
        delay = delays.ToArray();
        ani_nowNum = 0;
        ani_count = 0;
        ani_color = _col;
        ani_color_con = true;
        delete = false;

        if (!ui)
        {
            if (_light)
                sRenderer.material = GameManager.Instance.mat_light;
            else
                sRenderer.material = GameManager.Instance.mat_nonLight;
        }

        gameObject.layer = _layer;

        trk_now = false;
        trk_trf = null;

        rsv = 0f;

        crt_animate = animate();
        StartCoroutine(crt_animate);
    }

    public void Animate(bool _light, Vector3 _pos, AnimationCut[] _ac, Vector3 _scale)
    {
        if (ui)
            image.sprite = null;
        else
            sRenderer.sprite = null;

        transform.position = _pos;
        transform.localScale = _scale;

        parents_now = transform.parent;
        parents_ori = transform.parent;

        List<Sprite> sprs = new List<Sprite>();
        List<float> delays = new List<float>();

        for (int i = 0; i < _ac.Length; i++)
        {
            sprs.Add(_ac[i].spr);
            delays.Add(_ac[i].delay);
        }

        ani_spr = sprs.ToArray();
        delay = delays.ToArray();
        ani_nowNum = 0;
        ani_count = 0;
        delete = false;

        trk_now = false;
        trk_trf = null;

        if(!ui)
        {
            if (_light)
                sRenderer.material = GameManager.Instance.mat_light;
            else
                sRenderer.material = GameManager.Instance.mat_nonLight;
        }

        rsv = 0f;

        crt_animate = animate();
        StartCoroutine(crt_animate);
    }

    public void Animate(AnimationCut[] _ac)
    {
        if (ui)
            image.sprite = null;
        else
            sRenderer.sprite = null;

        parents_now = transform.parent;
        parents_ori = transform.parent;

        List<Sprite> sprs = new List<Sprite>();
        List<float> delays = new List<float>();

        for (int i = 0; i < _ac.Length; i++)
        {
            sprs.Add(_ac[i].spr);
            delays.Add(_ac[i].delay);
        }

        ani_spr = sprs.ToArray();
        delay = delays.ToArray();
        ani_nowNum = 0;
        ani_count = 0;
        delete = false;

        trk_now = false;
        trk_trf = null;

        rsv = 0f;

        crt_animate = animate();
        StartCoroutine(crt_animate);
    }

    public void Animate(bool _light, Vector3 _pos, AnimationCut[] _ac, Vector3 _scale, Transform _parents)
    {
        if (ui)
            image.sprite = null;
        else
            sRenderer.sprite = null;

        transform.position = _pos;
        transform.localScale = _scale;
        parents_ori = transform.parent;
        parents_now = _parents;
        transform.parent = _parents;        

        List<Sprite> sprs = new List<Sprite>();
        List<float> delays = new List<float>();

        for (int i = 0; i < _ac.Length; i++)
        {
            sprs.Add(_ac[i].spr);
            delays.Add(_ac[i].delay);
        }

        ani_spr = sprs.ToArray();
        delay = delays.ToArray();
        ani_nowNum = 0;
        ani_count = 0;
        delete = false;

        trk_now = false;
        trk_trf = null;

        if (!ui)
        {
            if (_light)
                sRenderer.material = GameManager.Instance.mat_light;
            else
                sRenderer.material = GameManager.Instance.mat_nonLight;
        }

        rsv = 0f;

        crt_animate = animate();
        StartCoroutine(crt_animate);
    }

    IEnumerator animate()
    {
        if (ui)
            image = GetComponent<Image>();
        else
            sRenderer = GetComponent<SpriteRenderer>();
        
        int c = 0;
        float dly = 0f;

        while (true)
        {
            if (ui)
                image.sprite = ani_spr[ani_nowNum];
            else
                sRenderer.sprite = ani_spr[ani_nowNum];

            dly = delay[ani_nowNum];

            
            if (ani_color_con)
            {
                if (ui)
                    image.color = ani_color[ani_nowNum];
                else
                    sRenderer.color = ani_color[ani_nowNum];
            }                

            ani_nowNum++;
            

            if (stop)
                break;

            yield return new WaitForSeconds(dly);

            if (stop)
                break;


            if(ani_nowNum == ani_spr.Length)
            {
                ani_nowNum = 0;

                if(ani_count != 0)
                {
                    if (c == ani_count)
                        break;
                    else
                        c++;
                }
            }
        }
        
        if(delete)
            Destroy(gameObject);
        else
            init();
    }

    public void init()
    {
        if(ui)
        {
            if (image == null)
                image = GetComponent<Image>();

            image.sprite = null;
        }
        else
        {
            if (sRenderer == null)
                sRenderer = GetComponent<SpriteRenderer>();

            sRenderer.sprite = null;
            sRenderer.material = GameManager.Instance.mat_nonLight;
        }

        if(parents_now != parents_ori)
        {
            transform.parent = parents_ori;
            parents_now = parents_ori;
        }
        
        trk_trf = null;
        trk_now = false;


        ani_color_con = false;
        ani_count = 0;
        ani_nowNum = 0;
        stop = false;
        
        transform.position = Vector3.zero;
        crt_animate = null;
    }

    private void OnEnable()
    {
        if (oneTime || always)
            StartCoroutine(simpleAnimation());
    }

    private void Start()
    {
        if(always)
            StartCoroutine(simpleAnimation());
    }

    private void FixedUpdate()
    {
        if (trk_now)
        {
            Debug.Log("trk");
            if(rsv == 0f)
                transform.position = trk_trf.position;
            else
            {
                Vector3 pos = trk_trf.position;
                pos.z += rsv;
                transform.position = pos;
            }
        }
            
    }

    IEnumerator simpleAnimation()
    {
        if (ui)
            image = GetComponent<Image>();
        else
            sRenderer = GetComponent<SpriteRenderer>();
        
        int i = 0;
        float dly = 0f;
        float t = 0f;
        bool ret = false;
        
        while(true)
        {
            if (ui)
                image.sprite = spr[i];
            else
                sRenderer.sprite = spr[i];

            if (delay.Length == 1)
                dly = delay[0];
            else
                dly = delay[i];
            
            i++;

            if (i == spr.Length)
            {
                if (oneTime)
                    break;
                else
                    i = 0;

            }

            while(true)
            {
                t += Time.deltaTime;

                if (t >= dly)
                {
                    ret = true;
                    t = 0f;
                    break;
                }                   

                yield return null;
            }

            if(ret)
            {
                yield return null;
                ret = false;
            }
                
        }

        if (delete)
            Destroy(gameObject);
    }

    private void Awake()
    {
        if (ui)
            image = GetComponent<Image>();
        else
            sRenderer = GetComponent<SpriteRenderer>();
    }
}
