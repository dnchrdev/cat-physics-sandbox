Shader "Custom/SilhouetteFillOutline_Clean_BIRP"
{
    Properties
    {
        _MainTex ("Pattern (RGBA)", 2D) = "white" {}
        _FillColor ("Fill Color", Color) = (1,1,1,1)

        _PatternScale ("Pattern Scale (repeats)", Float) = 5
        _PatternOffsetX ("Pattern Offset X", Float) = 0
        _PatternOffsetY ("Pattern Offset Y", Float) = 0

        _OutlineColor ("Outline Color", Color) = (0,0,0,1)
        _OutlineWidth ("Outline Width (world units)", Float) = 0.02

        [Enum(UnityEngine.Rendering.CompareFunction)] _ZTest ("ZTest", Float) = 4
    }

    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" "IgnoreProjector"="True" }

        // ---------- PASS 0: depth-only настоящего силуэта ----------
        // Пишет реальную глубину объекта, чтобы задние грани outline (Pass 1)
        // корректно резались ZTest'ом и не просвечивали сквозь прозрачный fill.
        Pass
        {
            Name "DepthOnly"
            Cull Back
            ZWrite On
            ZTest LEqual
            ColorMask 0

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata { float4 vertex : POSITION; };
            struct v2f { float4 pos : SV_POSITION; };

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target { return 0; }
            ENDCG
        }

        // Pass
        // {
        //     Name "Outline"
        //     Cull Front
        //     ZWrite On
        //     ZTest [_ZTest]
        //     Blend SrcAlpha OneMinusSrcAlpha

        //     CGPROGRAM
        //     #pragma vertex vert
        //     #pragma fragment frag
        //     #include "UnityCG.cginc"

        //     fixed4 _OutlineColor;
        //     float _OutlineWidth;

        //     struct appdata
        //     {
        //         float4 vertex : POSITION;
        //         float3 normal : NORMAL;
        //     };

        //     struct v2f { float4 pos : SV_POSITION; };

        //     v2f vert (appdata v)
        //     {
        //         v2f o;
        //         float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
        //         float3 worldNormal = normalize(mul((float3x3)unity_ObjectToWorld, v.normal));
        //         worldPos += worldNormal * _OutlineWidth;
        //         o.pos = mul(UNITY_MATRIX_VP, float4(worldPos, 1.0));
        //         return o;
        //     }

        //     fixed4 frag (v2f i) : SV_Target { return _OutlineColor; }
        //     ENDCG
        // }

        Pass
        {
            Name "SilhouetteFill"
            Cull Back
            ZWrite Off
            ZTest [_ZTest]
            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            fixed4 _FillColor;
            float _PatternScale;
            float _PatternOffsetX;
            float _PatternOffsetY;

            struct appdata { float4 vertex : POSITION; };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float4 screenPos : TEXCOORD0;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.screenPos = ComputeScreenPos(o.pos);
                return o;
            }
              
            fixed4 frag (v2f i) : SV_Target
            {
                float2 screenUV = i.screenPos.xy / i.screenPos.w;
                screenUV.x *= _ScreenParams.x / _ScreenParams.y;

                float2 patternUV = screenUV * _PatternScale + float2(_PatternOffsetX, _PatternOffsetY);
                fixed4 pat = tex2D(_MainTex, patternUV);

                fixed4 col = _FillColor;
                col.a *= pat.a;
                return col;
            }
            ENDCG
        }
    }

    FallBack Off
}