Shader "Custom/Unlit_Lightmap_ColorPlusTextureGlobalTiling"
{
    Properties
    {
        _GlobalTex ("Global Tiling Texture", 2D) = "white" {}
        _GlobalScale ("Global Tiling Scale", Float) = 1.0
        _GlobalStrength ("Global Texture Strength", Float) = 1.0
        _Color ("Color", Color) = (1,1,1,1)
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

            #pragma multi_compile_fwdbase _ LIGHTMAP_ON

            #include "UnityCG.cginc"
            #include "UnityLightingCommon.cginc"

            sampler2D _GlobalTex;

            float _GlobalScale;
            float _GlobalStrength;
            float4 _Color;

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 uv2 : TEXCOORD1;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float3 worldPos : TEXCOORD1;
                float3 normal : TEXCOORD2;
                float2 uvLM : TEXCOORD3;
            };

            v2f vert (appdata v)
            {
                v2f o;

                o.pos = UnityObjectToClipPos(v.vertex);

                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                o.normal = UnityObjectToWorldNormal(v.normal);

                o.uvLM = v.uv2 * unity_LightmapST.xy + unity_LightmapST.zw;

                return o;
            }

            float2 GetFaceUV(float3 worldPos, float3 normal)
            {
                float3 n = abs(normal);

                if (n.y > n.x && n.y > n.z)
                    return worldPos.xz;
                else if (n.x > n.z)
                    return worldPos.yz;
                else
                    return worldPos.xy;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // базовый цвет
                fixed4 baseCol = _Color;

                // глобальный тайлинг
                float2 globalUV = GetFaceUV(i.worldPos, normalize(i.normal)) * _GlobalScale;
                fixed4 globalCol = tex2D(_GlobalTex, globalUV);

                // смешивание
                fixed4 col = baseCol * lerp(fixed4(1,1,1,1), globalCol, _GlobalStrength);

                // lightmap
                #ifdef LIGHTMAP_ON
                    fixed3 lm = DecodeLightmap(UNITY_SAMPLE_TEX2D(unity_Lightmap, i.uvLM));
                    col.rgb *= lm;
                #endif

                return col;
            }
            ENDCG
        }
    }
}