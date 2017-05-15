namespace SeekWhale
{
    /*
	* 功 能： N/A
	* 类 名： Opshader
	* Email: paris3@163.com
	* 作 者： NSWell
	* Copyright (c) SeekWhale. All rights reserved.
	*/

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using DG.Tweening;
    using System;

    public class Opshader
    {
        public void Setmaterialrenderingmode(Material _material, RenderingMode _renderingMode, Baserendererop _op, Dissectedaction _action)
        {
            switch (_renderingMode)
            {
                case RenderingMode.OPAQUE:
                    _material.DOFade(1, _op.duration).OnComplete(() =>
                    {
                        _material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                        _material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                        _material.SetInt("_ZWrite", 1);
                        _material.DisableKeyword("_ALPHATEST_ON");
                        _material.DisableKeyword("_ALPHABLEND_ON");
                        _material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                        _material.renderQueue = -1;
                        if (_action != null)
                            _action.Beforeanatomy.Invoke();
                    });
                    _material.DOFloat(_op.metallic, "_Metallic", _op.duration);
                    _material.DOFloat(_op.smoothness, "_Glossiness", _op.duration);
                    break;
                case RenderingMode.CUTOUT:
                    _material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                    _material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                    _material.SetInt("_ZWrite", 1);
                    _material.EnableKeyword("_ALPHATEST_ON");
                    _material.DisableKeyword("_ALPHABLEND_ON");
                    _material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                    _material.renderQueue = 2450;
                    break;
                case RenderingMode.FADE:
                    _material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                    _material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                    _material.SetInt("_ZWrite", 0);
                    _material.DisableKeyword("_ALPHATEST_ON");
                    _material.EnableKeyword("_ALPHABLEND_ON");
                    _material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                    _material.renderQueue = 3000;
                    break;
                case RenderingMode.TRANSPARENT:
                    _material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                    _material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                    _material.SetInt("_ZWrite", 0);
                    _material.DisableKeyword("_ALPHATEST_ON");
                    _material.DisableKeyword("_ALPHABLEND_ON");
                    _material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
                    _material.renderQueue = 3000;
                    _material.DOFade(0, _op.duration).OnComplete(() =>
                    {
                        if (_action != null)
                            _action.Afteranatomy.Invoke();
                    });
                    _material.DOFloat(0, "_Metallic", _op.duration);
                    _material.DOFloat(0, "_Glossiness", _op.duration);
                    break;
            }
        }
    }
}
