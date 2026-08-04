Shader "Custom/ProceduralGrid_Lightmap_WorldAligned"
{
    Properties
    {
        _Color ("Base Color", Color) = (1,1,1,1)

        _MajorLineColor ("Major Line Color", Color) = (0,0,0,1)
        _MinorLineColor ("Minor Line Color", Color) = (0.2,0.2,0.2,1)

        _MajorGridSize ("Major Grid Size", Float) = 5.0
        _MinorGridSize ("Minor Grid Size", Float) = 1.0

        _MajorLineWidth ("Major Line Width", Range(0.001,10)) = 1.0
        _MinorLineWidth ("Minor Line Width", Range(0.001,10)) = 0.5

        _MajorGridOffset ("Major Grid Offset", Vector) = (0,0,0,0)
        _MinorGridOffset ("Minor Grid Offset", Vector) = (0,0,0,0)
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

            float4 _Color;

            float4 _MajorLineColor;
            float4 _MinorLineColor;

            float _MajorGridSize;
            float _MinorGridSize;

            float _MajorLineWidth;
            float _MinorLineWidth;

            float4 _MajorGridOffset;
            float4 _MinorGridOffset;

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 uv2 : TEXCOORD1;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float3 worldPos : TEXCOORD0;
                float3 normal : TEXCOORD1;
                float2 uvLM : TEXCOORD2;
            };

            v2f vert(appdata v)
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
                {
                    return worldPos.xz;
                }
                else if (n.x > n.z)
                {
                    return worldPos.yz;
                }
                else
                {
                    return worldPos.xy;
                }
            }

            float GridLine(float2 uv, float gridSize, float lineWidth)
            {
                float2 coord = uv / gridSize;

                float2 grid = abs(frac(coord) - 0.5) / fwidth(coord);

                float lineValue = min(grid.x, grid.y);

                return 1.0 - saturate(lineValue - lineWidth);
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float3 majorWorldPos = i.worldPos + _MajorGridOffset.xyz;
                float3 minorWorldPos = i.worldPos + _MinorGridOffset.xyz;

                float2 majorUV = GetFaceUV(majorWorldPos, normalize(i.normal));
                float2 minorUV = GetFaceUV(minorWorldPos, normalize(i.normal));

                fixed4 col = _Color;

                float minorMask = GridLine(minorUV, _MinorGridSize, _MinorLineWidth);
                float majorMask = GridLine(majorUV, _MajorGridSize, _MajorLineWidth);

                col.rgb = lerp(col.rgb, _MinorLineColor.rgb, minorMask);
                col.rgb = lerp(col.rgb, _MajorLineColor.rgb, majorMask);

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