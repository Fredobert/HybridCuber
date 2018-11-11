// NOT USED AND NOT FINSHED

Shader "Unlit/test2"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" "DisableBatching" = "True"}
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
			
			v2f vert (appdata v)
			{
				v2f o;
				//v.vertex.x *= v.vertex.x * 0.;
				//v.vertex.x = v.vertex.x / 0.5;

				//o.vertex = UnityObjectToClipPos(o.vertex);
				//o.vertex += ComputeScreenPos(float4(2, 2, 2,2)) *0.5;
				float4 te = mul(unity_ObjectToWorld,v.vertex);
				//float4 tenew= mul(unity_ObjectToWorld, float4(v.vertex.x, v.vertex.y,v.vertex.z , 1));
				float4 tenew = float4(te.x, te.y, 0,0);
				float4 te2 =   normalize(tenew-te)*abs(_SinTime[1]);
				//te = te * 0.5;
				//o.vertex = UnityObjectToClipPos(te);
				//float4 te2 = mul(unity_ObjectToWorld, ;
				float perc = 0;
				o.vertex = UnityObjectToClipPos(te2.xyz);
				
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				//UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv);
				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);
				return col;
			}
			ENDCG
		}
	}
}
