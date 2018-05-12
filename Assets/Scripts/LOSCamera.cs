﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LOSCamera : MonoBehaviour {
    public float BlurSize;
    public Shader GaussianBlurShader;
    public Material material;
	// Use this for initialization
	void Start () {
        var t = new RenderTexture(Screen.width, Screen.height, 16);
        GetComponent<Camera>().targetTexture = t;
        Shader.SetGlobalTexture("_LOSMaskTexture", t);
    }

    void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
    {
        if (BlurSize != 0 && GaussianBlurShader != null)
        {

            int rtW = sourceTexture.width / 8;
            int rtH = sourceTexture.height / 8;


            RenderTexture rtTempA = RenderTexture.GetTemporary(rtW, rtH, 0, sourceTexture.format);
            rtTempA.filterMode = FilterMode.Bilinear;


            Graphics.Blit(sourceTexture, rtTempA);

            for (int i = 0; i < 2; i++)
            {

                float iteraionOffs = i * 1.0f;
                material.SetFloat("_blurSize", BlurSize + iteraionOffs);

                //vertical blur  
                RenderTexture rtTempB = RenderTexture.GetTemporary(rtW, rtH, 0, sourceTexture.format);
                rtTempB.filterMode = FilterMode.Bilinear;
                Graphics.Blit(rtTempA, rtTempB, material, 0);
                RenderTexture.ReleaseTemporary(rtTempA);
                rtTempA = rtTempB;

                //horizontal blur  
                rtTempB = RenderTexture.GetTemporary(rtW, rtH, 0, sourceTexture.format);
                rtTempB.filterMode = FilterMode.Bilinear;
                Graphics.Blit(rtTempA, rtTempB, material, 1);
                RenderTexture.ReleaseTemporary(rtTempA);
                rtTempA = rtTempB;

            }
            Graphics.Blit(rtTempA, destTexture);

            RenderTexture.ReleaseTemporary(rtTempA);
        }

        else
        {
            Graphics.Blit(sourceTexture, destTexture);

        }


    }

}
