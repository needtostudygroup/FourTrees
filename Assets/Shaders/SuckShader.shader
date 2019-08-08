Shader "Unlit/SuckShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Coordinate ("Coordinate", Vector) = (0,0,0,0)
        _CursorSize ("CursorSize", Range(1, 100)) = 1
        _Direction ("Direction", Float) = 1
        _Speed ("Speed", Range(0, 1)) = 1
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
            float4 _MainTex_ST, _Coordinate;
            float _CursorSize, _Direction, _Speed;
            static const float CURSOR_SIZE = _CursorSize / 10000;
            static const float CURSOR_RADIUS = CURSOR_SIZE / 2;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed frag (v2f i) : SV_Target
            {
                fixed col = tex2D(_MainTex, i.uv);
                fixed amount = pow(saturate(1 - distance(i.uv, _Coordinate.xy)), 1000);
                fixed color = lerp(0, _Direction, amount * _Speed);
                return clamp(col + color, -1, 1);
            }
            ENDCG
        }
    }
}
