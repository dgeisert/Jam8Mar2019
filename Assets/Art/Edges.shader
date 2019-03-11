﻿Shader "Custom/Edges"
{
    Properties
    {
		_Color("Color", Color) = (1,1,1,1)
		_RimColor("Rim Color", Color) = (1,1,1,1)
        _Edge("Edge", float) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };
			
			float4 _Color;	

			float4 _RimColor;
            float _Edge;
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                if(i.uv.x > (1-_Edge) || i.uv.x < _Edge
                || i.uv.y > (1-_Edge) || i.uv.y < _Edge){
                    return _RimColor;
                }
                else{
                    return _Color;
                }
            }
            ENDCG
        }
    }
}