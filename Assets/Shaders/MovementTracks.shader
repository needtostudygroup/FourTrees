Shader "Unlit/MovementTracks"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Coordinate ("Coordiate", Vector) = (0,0,0,0)
        _Color ("Color", Color) = (1,0,0,0)
        _Size ("Size", Range(1, 500)) = 1
        _Strength ("Strength", Range(0, 1)) = 1
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
			#pragma enable_d3d11_debug_symbols

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
            float _Size, _Strength;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }
            
            fixed4 _Coordinate, _Color, _PreviousCoordinate;

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                
                // 3000 = 스키드 마크의 크기
                float draw = pow(saturate(1 - distance(i.uv, _Coordinate.xy)), _Size);
                fixed4 drawColor = _Color * (draw * _Strength);
                return saturate(col + drawColor);
            }
            ENDCG
        }
    }
}
