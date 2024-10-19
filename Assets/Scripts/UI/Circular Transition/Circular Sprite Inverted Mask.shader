Shader "Custom/TransparentCircleMask"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _MaskCenter ("Mask Center", Vector) = (0.5, 0.5, 0, 0)
        _MaskRadius ("Mask Radius", Float) = 0.5
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" }
        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
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
            float4 _MaskCenter;
            float _MaskRadius;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 uv = i.uv;
                float dist = distance(uv, _MaskCenter.xy);

                if (dist <= _MaskRadius)
                    discard;  // Make the inside transparent

                return fixed4(0, 0, 0, 1);  // Outside is black
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
