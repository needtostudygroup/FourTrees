    Shader "Cutom/GroundTracker" {
        Properties {
            _Tess ("Tessellation", Range(1,32)) = 4
            _DentColor ("Dent Color", Color) = (1,1,1,1)
            _DentTex ("Base (RGB)", 2D) = "white" {}
            _GroundColor ("Dent Color", Color) = (1,1,1,1)
            _GroundTex ("Base (RGB)", 2D) = "white" {}
            _Splat ("Splat Map", 2D) = "black" {}
            _MainTex ("Base (RGB)", 2D) = "white" {}
            _Displacement ("Displacement", Range(0, 1.0)) = 0.3
        }
        SubShader {
            Tags { "RenderType"="Opaque" }
            LOD 300
            
            CGPROGRAM
            #pragma surface surf BlinnPhong addshadow fullforwardshadows vertex:disp tessellate:tessDistance nolightmap
            #pragma target 4.6
            #include "Tessellation.cginc"

            struct appdata {
                float4 vertex : POSITION;
                float4 tangent : TANGENT;
                float3 normal : NORMAL;
                float2 texcoord : TEXCOORD0;
            };

            float _Tess;

            float4 tessDistance (appdata v0, appdata v1, appdata v2) {
                float minDist = 10.0;
                float maxDist = 25.0;
                return UnityDistanceBasedTess(v0.vertex, v1.vertex, v2.vertex, minDist, maxDist, _Tess);
            }

            sampler2D _Splat;
            float _Displacement;

            void disp (inout appdata v)
            {
                float d = tex2Dlod(_Splat, float4(v.texcoord.xy,0,0)).r * _Displacement;
                v.vertex.xyz -= v.normal * d;
            }
            
            sampler2D _GroundTex;
            fixed4 _GroundColor;
            sampler2D _DentTex;
            fixed4 _DentColor;

            struct Input {
                float2 uv_GroundTex;
                float2 uv_DentTex;
                float2 uv_Splat;
            };

            sampler2D _MainTex;
            fixed4 _Color;

            void surf (Input IN, inout SurfaceOutput o) {
                half amount = tex2Dlod(_Splat, float4(IN.uv_Splat, 0, 0));
                fixed4 c = lerp(tex2D(_DentTex, IN.uv_DentTex) * _DentColor, tex2D(_GroundTex, IN.uv_GroundTex) * _GroundColor, amount);
            
//                fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
                o.Albedo = c.rgb;
                o.Specular = 0.2;
                o.Gloss = 1.0;
            }
            ENDCG
        }
        FallBack "Diffuse"
    }