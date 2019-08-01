Shader "Cutom/GroundTracker" {
    Properties {
        _Tess ("Tessellation", Range(1,32)) = 4
        _DentColor ("Dent Color", Color) = (1,1,1,1)
        _DentTex ("DentTex (RGB)", 2D) = "white" {}
        _GroundColor ("Ground Color", Color) = (1,1,1,1)
        _GroundTex ("Ground Tex (RGB)", 2D) = "white" {}
        _Splat ("Splat Map", 2D) = "black" {}
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _Displacement ("Displacement", Range(0, 1.0)) = 0.3
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
    }
    SubShader {
        Tags { "RenderType"="Opaque" }
        LOD 200
        
        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows vertex:disp tessellate:tessDistance
        #pragma target 4.6
        #include "Tessellation.cginc"

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
            v.vertex.xyz += (v.normal * _Displacement);
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
        
        half _Glossiness;
        half _Metallic;

        UNITY_INSTANCING_BUFFER_START(Props)
        // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o) {
            half amount = tex2Dlod(_Splat, float4(IN.uv_Splat, 0, 0));
            fixed4 c = lerp(tex2D(_GroundTex, IN.uv_GroundTex) * _GroundColor, tex2D(_DentTex, IN.uv_DentTex) * _DentColor, amount);
        
            o.Albedo = c.rgb;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}