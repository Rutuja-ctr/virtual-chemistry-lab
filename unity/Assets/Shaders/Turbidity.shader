Shader "ARLab/UnlitTurbidity"
{
    Properties
    {
        _BaseColor ("Base Color", Color) = (0.8,0.93,1,0.6)
        _Turbidity ("Turbidity", Range(0,1)) = 0
        _NoiseTex ("Noise (optional)", 2D) = "white" {}
        _NoiseScale ("Noise Scale", Float) = 3
        _EdgeColor ("Edge Color", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 100
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        Cull Back

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _NoiseTex;
            float4 _BaseColor;
            float4 _EdgeColor;
            float _Turbidity;
            float _NoiseScale;

            struct appdata { float4 vertex:POSITION; float3 normal:NORMAL; float2 uv:TEXCOORD0; };
            struct v2f { float4 pos:SV_POSITION; float3 worldNormal:TEXCOORD0; float3 viewDir:TEXCOORD1; float2 uv:TEXCOORD2; };

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                o.viewDir = _WorldSpaceCameraPos - worldPos;
                o.uv = v.uv * _NoiseScale;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float3 N = normalize(i.worldNormal);
                float3 V = normalize(i.viewDir);
                // fresnel edge brightening to simulate opalescence
                float fres = pow(1 - saturate(dot(N, V)), 3);
                float noise = tex2D(_NoiseTex, i.uv).r;
                float turb = saturate(_Turbidity + (noise - 0.5) * 0.2 * _Turbidity);

                float alpha = saturate(0.15 + turb * 0.7);
                float3 col = lerp(_BaseColor.rgb, _EdgeColor.rgb, fres * turb);

                return fixed4(col, alpha);
            }
            ENDCG
        }
    }
}
