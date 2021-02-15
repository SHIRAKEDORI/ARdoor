Shader "PostEffect/Pixel"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Width("Width", Int) = 160//数値入力フィールド_IsHoge("IsHoge, Float") = 0
        _Height("Height",Int) = 90
        _DivX("DivX",Int) = 40
        _DivY("DivY",Int) = 40
        [Toggle(togle)]
        _Hoge("togle", Float) = 0
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
            #pragma shader_feature togle

            #include "UnityCG.cginc"

            struct appdata //vertex shaderに渡す
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f //Fragment shaderに渡す
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
            int _DivX;
            int _DivY;
            int _Width;
            int _Height;
        


            #ifdef togle
                fixed4 frag (v2f i) : SV_Target
                {
                    float2 grid;
                    
                    _DivX = _ScreenParams.x / _Width;
                    _DivY = _ScreenParams.y / _Height;
                    grid.x = floor(i.uv.x * _DivX) / _DivX;
                    grid.y = floor(i.uv.y * _DivY) / _DivY;
                    fixed4 col = tex2D(_MainTex, grid);
                    return col;
                }

            #else
                fixed4 frag (v2f i) : SV_Target
                {
                    float2 grid;
                            
                    grid.x = floor(i.uv.x * _DivX) / _DivX;
                    grid.y = floor(i.uv.y * _DivY) / _DivY;
                    fixed4 col = tex2D(_MainTex, grid);
                    return col;
                }
            #endif
            ENDCG
        }
    }
}
