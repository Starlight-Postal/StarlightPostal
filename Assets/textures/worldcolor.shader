// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/worldspace"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        texPower("texture weight",Range(0,1)) = 0.0
        _ColorA("Ground Color", Color) = (1,1,1,0)
        _ColorB("Wall Color", Color) = (1,1,1,0)
        _ColorC("BG Color", Color) = (1,1,1,0)
        _ColorD("Sky Color", Color) = (1,1,1,0)

        fgDepth("fg depth",Range(0,100)) = 10.0
        skyDepth("sky depth",Range(0,1000)) = 100.0
    }
        SubShader
    {
        Tags {"Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent"}
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha
        Cull back
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            //#pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                //UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
                float4 worldPos: TEXCOORD2;
                float3 worldNormal : TEXCOORD5;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            fixed4 _ColorA;
            fixed4 _ColorB;
            fixed4 _ColorC;
            fixed4 _ColorD;
            float fgDepth;
            float skyDepth;
            float texPower;

            v2f vert(appdata v)
            {
                v2f o;

                float ofset = float4(0, 1000, 0, 0);

                //o.vertex = UnityObjectToClipPos(v.vertex);
                o.vertex = UnityObjectToClipPos(v.vertex);

                o.uv = TRANSFORM_TEX(v.uv, _MainTex);

                o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                o.worldNormal = mul((float3x3)unity_ObjectToWorld, v.normal);
                //o.worldNormal = mul(unity_ObjectToWorld, objectNormal);
                //o.worldPos = ComputeScreenPos(o.vertex);

                //o.uv = mul(unity_ObjectToWorld, v.vertex).xyz;

                //UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {

                // sample the texture
                //fixed4 col = tex2D(_MainTex, i.uv);
                fixed4 base = _ColorB;
                if (i.worldPos.z < 0&&i.worldNormal.z<-0.5) {
                    base = _ColorA;
                }
                if (i.worldPos.z > fgDepth) {
                    float g = (i.worldPos.z - fgDepth) / (skyDepth - fgDepth);
                    base = (_ColorC*(1-g))+(_ColorD*g);
                }
                if (i.worldPos.z > skyDepth) {
                    base = _ColorD;
                }
                fixed4 col = base;
                col *= ((tex2D(_MainTex, i.uv) * texPower) + (1 - texPower));
                return col;
            }
            ENDCG
        }
    }
}