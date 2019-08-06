Shader "Unlit/SuckMouseShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Coordinate ("Coordinate", Vector) = (0,0,0,0)
        _Color ("Color", Color) = (1,0,0,0)
        _CursorSize ("CursorSize", Range(1, 100)) = 1
        _CursorTexture ("CursorTexture", 2D) = "black" {}
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

            sampler2D _MainTex, _CursorTexture;
            float4 _MainTex_ST, _Coordinate;
            fixed4 _Color;
            float _CursorSize;
            static const float CURSOR_SIZE = _CursorSize / 10000;
            static const float CURSOR_RADIUS = CURSOR_SIZE / 2;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = (0,0,0,0);
                float2 dist = i.uv - _Coordinate.xy;
                if (dist.x >= -CURSOR_RADIUS && dist.y >= -CURSOR_RADIUS && dist.x <= CURSOR_RADIUS && dist.y <= CURSOR_RADIUS) {
                    float2 cursorUV = (dist.xy + CURSOR_RADIUS) / CURSOR_SIZE;
                    return tex2Dlod(_CursorTexture, float4(cursorUV, 0, 0)) * _Color;
                }
                
                return col;
            }
            ENDCG
        }
    }
}
