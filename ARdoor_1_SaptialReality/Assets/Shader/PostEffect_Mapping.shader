Shader "PostEffect/Mapping"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _translate("Translate", Float) = 0
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            uniform float4x4 _mv;

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

            v2f vert (appdata v)
            {
                float4 vec = mul(_mv, v.vertex); // ここで変換されたベクトルのW成分(奥行きに比例)はテクスチャを正しく貼るために残したい
                v2f o;
                o.vertex = float4(vec.xy * 2.0 - vec.w, vec.zw); // UnityObjectToClipPos(vec)だとW成分が消されるようなので、自前で2倍に拡大し、消失点が画像隅から画面中心に変わるので、それを加味してX、YからWを引く
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                return col;
            }
            ENDCG
        }
    }
}