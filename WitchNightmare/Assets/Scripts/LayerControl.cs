using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[ExecuteInEditMode]
public class LayerControl : MonoBehaviour {
    public enum STATIC_TYPE
    {
        none,
        static_absolute,
        static_relative,
        static_tile,
    }
    
    public float adjust_y;
    private float standard_y;
    public bool static_layer;
    
    public STATIC_TYPE static_layer_type;
    public SpriteRenderer static_layer_srr;
    public int static_layer_val;

       
    private SpriteRenderer sRenderer;

    public void SetStaticLayer(STATIC_TYPE _type)
    {
        static_layer = true;
        static_layer_type = _type;
        static_layer_srr = null;
        static_layer_val = 0;
        LayerManager.Instance.AddLayers(this);
    }

    public void SetStaticLayer(STATIC_TYPE _type, SpriteRenderer _srr, int _val)
    {
        static_layer = true;
        static_layer_type = _type;
        static_layer_srr = _srr;
        static_layer_val = _val;
        LayerManager.Instance.AddLayers(this);
    }

    public void SetStaticLayer(STATIC_TYPE _type, int _val)
    {
        static_layer = true;
        static_layer_type = _type;
        static_layer_srr = null;
        static_layer_val = _val;
        LayerManager.Instance.AddLayers(this);
    }

    public void SetNormalLayer(float _ad = 0)
    {
        static_layer = false;
        adjust_y = _ad;
        LayerManager.Instance.AddLayers(this);
    }

    public float GetY()
    {        
        return transform.position.y + adjust_y;
    }

    public SpriteRenderer GetSrr()
    {
        if (sRenderer)
            return sRenderer;
        else
        {
            sRenderer = GetComponent<SpriteRenderer>();
            return sRenderer;
        }
    }

    private void Awake()
    {
        sRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
         if (static_layer_type == STATIC_TYPE.static_absolute)
            return;

        LayerManager.Instance.AddLayers(this);
    }

    private void OnDisable()
    {
        if (static_layer_type == STATIC_TYPE.static_absolute)
            return;

        if (LayerManager.Instance == null)
            return;

        LayerManager.Instance.RemoveLayers(this);
    }

    private void OnDestroy()
    {
        if (static_layer_type == STATIC_TYPE.static_absolute)
            return;

        if (LayerManager.Instance == null)
            return;

        LayerManager.Instance.RemoveLayers(this);
    }

    private void LateUpdate()
    {
        standard_y = transform.position.y + adjust_y;

        if(static_layer)
        {
            switch(static_layer_type)
            {
                case STATIC_TYPE.static_absolute:
                    sRenderer.sortingOrder = static_layer_val;
                    break;

                case STATIC_TYPE.static_relative:
                    sRenderer.sortingOrder = static_layer_srr.sortingOrder + static_layer_val;
                    break;
            }
        }
    }
    
}
