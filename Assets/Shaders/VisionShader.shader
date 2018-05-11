Shader "Unlit/VisionShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		
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
			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;

			uniform float4 _Points[100];  // 数组变量
			uniform float _Points_Num;  // 数组长度变量
			uniform float4 _PawnPos;

			int vertical(float4 a, float4 b,float4 c){
				float4 dir = c; 
				float t = (a.x) / dir.x;
				if(t < 0 || t > 1) return 0;
				else if ((( +  dir.y * t) - a.y) * ((  + dir.y * t) - b.y) < 0 ) return 1;
				else return 0;

			}
			int horizontal(float4 a, float4 b,float4 c){
				float4 dir = c; 
				float t = (a.y) / dir.y;
				if(t < 0 || t > 1) return 0;
				else if ((( + dir.x * t) - a.x) * (( + dir.x * t) - b.x) < 0 ) return 1;
				else return 0;

			}

			int isintersect(float4 tile,float4 pos){
				float4 a = UnityObjectToClipPos((tile - float4(0.5,-0.5,0,0)) * 1.35f);
				float4 b = UnityObjectToClipPos((tile + float4(0.5,0.5,0,0)) * 1.35f);
				float4 c = UnityObjectToClipPos((tile + float4(0.5,-0.5,0,0)) * 1.35f);
				float4 d = UnityObjectToClipPos((tile - float4(0.5,0.5,0,0)) * 1.35f);
				return horizontal(a,b,pos) || horizontal(c,d,pos) || vertical(b,c,pos) || vertical(a,d,pos);
			}

			


			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f a) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, a.uv);
				for(int i = 0;i < _Points_Num;i++){
					if(_Points[i].z != 1.0){
					if(isintersect(_Points[i],a.vertex)){
						col = fixed4(0,0,0,1);
			    	}
					}
				}
				// apply fog
				UNITY_APPLY_FOG(a.fogCoord, col);
				return col;
			}
			ENDCG
		}
	}
}
