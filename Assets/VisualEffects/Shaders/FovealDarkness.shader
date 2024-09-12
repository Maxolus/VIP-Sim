﻿Shader "Hidden/FovealDarkness"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _MouseX("Mouse X Position (Normalized 0 to 1)", Float) = 0.5
        _MouseY("Mouse Y Position (Normalized 0 to 1)", Float) = 0.5
        _InnerCircleRadius("Inner Circle Radius", Float) = 0.25
        _FadeWidth("Fade Width", Float) = 0.05
        _Opacity("Opacity of Overlay", Float) = 0.5
        _ScreenHeight("Screen Height in px", int) = 1440
        _ScreenWidth("Screen Width in px", int) = 2560
    }

    SubShader
    {
        Tags { "Queue" = "Overlay" }
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float _MouseX;
            float _MouseY;
            float _InnerCircleRadius;
            float _FadeWidth;
            float _Opacity;
            int _ScreenHeight;
            int _ScreenWidth;

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert(appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }
            
            float rand(float2 co)
            {
                return frac(sin(dot(co.xy, float2(12.9898, 78.233))) * 43758.5453);
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float2 uv = i.uv;
                float2 mousePos = float2(_MouseX, _MouseY);

                // Calculate the aspect ratio
                float aspectRatio = _ScreenWidth / _ScreenHeight;

                // Adjust the UV coordinates to maintain a circular vortex
                float2 adjustedUV = uv;
                adjustedUV.x *= aspectRatio;
                float2 adjustedMousePos = mousePos;
                adjustedMousePos.x *= aspectRatio;

                // Calculate distance from mouse position
                float2 delta = adjustedUV - adjustedMousePos;
                float dist = length(delta);

                // Fetch the color from the vortexed coordinates
                fixed4 color = tex2D(_MainTex, adjustedUV);
   
                    
                if (dist < _InnerCircleRadius)
                {
                    // Set color to black
                    fixed4 blackColor = lerp(fixed4(0, 0, 0, 1.0), color, _Opacity);
                    // Calculate fade factor
                    float fade = smoothstep(_InnerCircleRadius - _FadeWidth, _InnerCircleRadius, dist);
                    
                    // Interpolate between grey and original color based on fade factor
                    //fixed4 outerColor = tex2D(_MainTex, uv);
                    color = lerp(blackColor, color, fade);
                }

                return color;
            }

            ENDCG
        }
    }
}
