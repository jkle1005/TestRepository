using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;

[ExecuteInEditMode]
public class LayerManager : MonoBehaviour
{
    private static LayerManager instance;
    public static LayerManager Instance
    {
        get
        {
            if (LayerManager.instance == null)
                LayerManager.instance = FindObjectOfType<LayerManager>();
            return LayerManager.instance;
        }
    }

    public int layer_minimum_obj;
    public int layer_minimum_tile;
    private List<LayerControl> layers;





    private LayerControl[] sortingLayers()
    {
        for (int i = 0; i < layers.Count; i++)
        {
            if (layers[i] == null)
            {
                RemoveLayers(i);
            }
        }

        var result = from data in layers
                     orderby data.GetY() descending
                     select data;

        return result.ToArray();
    }

    public bool OverlapCheck(LayerControl _la)
    {
        foreach (LayerControl la in layers)
        {
            if (la == _la)
                return true;
        }

        return false;
    }

    public void AddLayers(LayerControl _la)
    {
        if (_la.static_layer_type == LayerControl.STATIC_TYPE.static_absolute)
            return;

        if (OverlapCheck(_la))
            return;

        layers.Add(_la);
    }

    public void RemoveLayers(LayerControl _la)
    {
        // if (_la.static_layer_type == LayerControl.STATIC_TYPE.static_absolute)
        //    return;

        layers.Remove(_la);
    }

    public void RemoveLayers(int _at)
    {
        /*
        if (layers[_at].static_layer_type == LayerControl.STATIC_TYPE.static_absolute)
        {
            Debug.Log("GameManager::RemoveLayers(int)::layers[_at].static_layers_type is absolute");
            Debug.Log(layers[_at].name);
            return;
        }
            */

        layers.RemoveAt(_at);
    }

    private void Update()
    {
        var _layers = sortingLayers();
        int i = layer_minimum_obj;
        int t = layer_minimum_tile;
        foreach (LayerControl _layer in _layers)
        {
            // Debug.Log(_layer.name);
            if (_layer.static_layer_type == LayerControl.STATIC_TYPE.static_tile)
            {
                if (_layer.GetSrr() == null)
                {
                    layers.Remove(_layer);
                }
                else
                {
                    _layer.GetSrr().sortingOrder = t + 1;

                    /*
                    if (_layer.GetComponent<OutlineObject>() != null)
                    {
                        _layer.GetComponent<OutlineObject>().OutlineSprite.sortingOrder = t;
                    }*/

                    t += 2;
                }
            }
            else if (!_layer.static_layer)
            {
                if (_layer.GetSrr() == null || !_layer.gameObject.activeSelf)
                {
                    layers.Remove(_layer);
                }
                else
                {
                    _layer.GetSrr().sortingOrder = i + 1;
                    /*
                    if (_layer.GetComponent<OutlineObject>() != null && _layer.GetComponent<OutlineObject>().OutlineSprite != null)
                    {
                        if(_layer.GetComponent<OutlineObject>().includeChildren)
                        {
                            int min = int.MaxValue;
                            for(int j = 0; j < _layer.transform.childCount; j++)
                            {
                                SpriteRenderer _srr = _layer.transform.GetChild(j).GetComponent<SpriteRenderer>();
                                if (_srr.sortingOrder <= min && _srr.name != "Outline")
                                    min = _srr.sortingOrder;
                            }
                            Debug.Log("min: " + min);
                            _layer.GetComponent<OutlineObject>().OutlineSprite.sortingOrder = min - 1;
                        }
                        else
                        {
                            _layer.GetComponent<OutlineObject>().OutlineSprite.sortingOrder = i;
                        }
                    }*/

                    i += 2;
                }
            }
        }
    }

    private void Awake()
    {
        layers = new List<LayerControl>();
    }
}
