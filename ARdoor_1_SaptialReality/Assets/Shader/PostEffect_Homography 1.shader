Shader "PostEffect/Homography1"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
		Blend Off
		ZTest Always
		ZWrite Off
		Cull Off
		Lighting Off
		
		CGINCLUDE
		
					
		sampler2D _MainTex;
		float _Homography[9];
		float _InvHomography[9];

		
		ENDCG

        Pass
        {
       		CGPROGRAM
			#pragma vertex vert_clear//pragmaで処理を行うエントリ関数の指定、シェーダーの宣言
			#pragma fragment frag_clear
						
			#include "UnityCG.cginc"



			struct appdata_clear
			{
				float4 vertex : POSITION;
			};

			struct v2f_clear
			{
				float4 vertex : SV_POSITION;
			};

			v2f_clear vert_clear(appdata_clear v)
			{
				v2f_clear o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				return o;
			}

			fixed4 frag_clear(appdata_clear i) : SV_Target
			{
				return 0;
			}


			ENDCG
        }

		
        Pass
        {
            CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag_homography

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 screenPos : TEXCOORD1;
				float4 vertex : SV_POSITION;
			};


						
			v2f vert(appdata v)
			{
				v2f o;

				float4 p = v.vertex;
				float s =  _InvHomography[6] * p.x + _InvHomography[7] * p.y + _InvHomography[8];
				float x = (_InvHomography[0] * p.x + _InvHomography[1] * p.y + _InvHomography[2]) / s;
				float y = (_InvHomography[3] * p.x + _InvHomography[4] * p.y + _InvHomography[5]) / s;
				o.vertex = float4(2 * (x - 0.5), 2 * (0.5 - y), 0, 1);
				o.screenPos = float2(o.vertex.x, o.vertex.y);

				return o;
			}

			fixed4 frag_homography(v2f i) : SV_Target
			{
				float2 p = (i.screenPos + 1.0) * 0.5;
				p.y = 1 - p.y;
				float s =  _Homography[6] * p.x + _Homography[7] * p.y + _Homography[8];
				float u = (_Homography[0] * p.x + _Homography[1] * p.y + _Homography[2]) / s;
				float v = (_Homography[3] * p.x + _Homography[4] * p.y + _Homography[5]) / s;
				float2 uv = float2(u, v);
				return tex2D(_MainTex, uv);
			}

			ENDCG
        }
    }
}
