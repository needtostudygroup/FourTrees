Shader "Unlit/PaintShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Coordinate ("Coordinate", Vector) = (0,0,0,0)
        _Color ("Color", Color) = (1,0,0,0)
        _PaintTexture ("PaintTexture", 2D) = "white" {}
        _Size ("Size", Range(0, 1)) = 0.3
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

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _Coordinate, _Color;
            sampler2D _PaintTexture;
            float _Size;
            
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }
            
            fixed4 frag (v2f i) : SV_Target
            {
                _Size /= 10;
                fixed4 col = tex2D(_MainTex, i.uv) * _Color;
                half4 amount = (0,0,0,0);
                
                float dist = distance(i.uv, _Coordinate);
                if (dist >= 0 && dist <= _Size) {
                    amount = tex2Dlod(_PaintTexture, (float4(i.uv - _Coordinate, 0, 0) + float4(_Size, _Size, 0, 0)) * (1 / (_Size * 2)));
                }
                
                fixed4 draw = _Color * (amount * 5);
                return saturate(col + draw);
            }
            ENDCG
        }
    }
}