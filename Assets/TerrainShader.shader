// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.36 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.36;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:4013,x:33253,y:32763,varname:node_4013,prsc:2|diff-4846-OUT;n:type:ShaderForge.SFN_NormalVector,id:3221,x:31889,y:32713,prsc:2,pt:False;n:type:ShaderForge.SFN_ComponentMask,id:7113,x:32123,y:32713,varname:node_7113,prsc:2,cc1:0,cc2:2,cc3:-1,cc4:-1|IN-3221-OUT;n:type:ShaderForge.SFN_Abs,id:6533,x:32299,y:32735,varname:node_6533,prsc:2|IN-7113-OUT;n:type:ShaderForge.SFN_ChannelBlend,id:4846,x:33004,y:33003,varname:node_4846,prsc:2,chbt:0|M-1297-OUT,R-8742-RGB,G-7944-RGB,B-8742-RGB;n:type:ShaderForge.SFN_Multiply,id:2139,x:32466,y:32729,varname:node_2139,prsc:2|A-6533-OUT,B-6533-OUT;n:type:ShaderForge.SFN_Add,id:2815,x:32828,y:32739,varname:node_2815,prsc:2|A-3444-R,B-3444-G;n:type:ShaderForge.SFN_ComponentMask,id:3444,x:32632,y:32729,varname:node_3444,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-2139-OUT;n:type:ShaderForge.SFN_If,id:8246,x:32252,y:32917,varname:node_8246,prsc:2|A-6262-OUT,B-2279-OUT,GT-6262-OUT,EQ-6262-OUT,LT-2279-OUT;n:type:ShaderForge.SFN_ComponentMask,id:6262,x:31889,y:32923,varname:node_6262,prsc:2,cc1:1,cc2:-1,cc3:-1,cc4:-1|IN-3221-OUT;n:type:ShaderForge.SFN_Vector1,id:2279,x:32037,y:32989,varname:node_2279,prsc:2,v1:0;n:type:ShaderForge.SFN_Append,id:1297,x:32840,y:32993,varname:node_1297,prsc:2|A-2815-OUT,B-9051-OUT,C-2309-OUT;n:type:ShaderForge.SFN_If,id:3892,x:32265,y:33074,varname:node_3892,prsc:2|A-6262-OUT,B-2279-OUT,GT-2279-OUT,EQ-7764-OUT,LT-7764-OUT;n:type:ShaderForge.SFN_Negate,id:7764,x:32082,y:33093,varname:node_7764,prsc:2|IN-6262-OUT;n:type:ShaderForge.SFN_Color,id:7944,x:32717,y:33166,ptovrint:False,ptlb:Grass,ptin:_Grass,varname:node_7944,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.1550071,c2:0.6691177,c3:0.1928094,c4:1;n:type:ShaderForge.SFN_Color,id:8742,x:32717,y:33347,ptovrint:False,ptlb:Rock,ptin:_Rock,varname:node_8742,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Multiply,id:3787,x:32425,y:32917,varname:node_3787,prsc:2|A-8246-OUT,B-8246-OUT;n:type:ShaderForge.SFN_Multiply,id:2309,x:32468,y:33083,varname:node_2309,prsc:2|A-3892-OUT,B-3892-OUT;n:type:ShaderForge.SFN_Multiply,id:9051,x:32466,y:32600,varname:node_9051,prsc:2|A-3787-OUT,B-3787-OUT;proporder:7944-8742;pass:END;sub:END;*/

Shader "Shader Forge/TerrainShader" {
    Properties {
        _Grass ("Grass", Color) = (0.1550071,0.6691177,0.1928094,1)
        _Rock ("Rock", Color) = (0.5,0.5,0.5,1)
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal d3d11_9x 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float4 _Grass;
            uniform float4 _Rock;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 posWorld : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
                LIGHTING_COORDS(2,3)
                UNITY_FOG_COORDS(4)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos(v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb; // Ambient Light
                float2 node_6533 = abs(i.normalDir.rb);
                float2 node_3444 = (node_6533*node_6533).rg;
                float node_6262 = i.normalDir.g;
                float node_2279 = 0.0;
                float node_8246_if_leA = step(node_6262,node_2279);
                float node_8246_if_leB = step(node_2279,node_6262);
                float node_8246 = lerp((node_8246_if_leA*node_2279)+(node_8246_if_leB*node_6262),node_6262,node_8246_if_leA*node_8246_if_leB);
                float node_3787 = (node_8246*node_8246);
                float node_3892_if_leA = step(node_6262,node_2279);
                float node_3892_if_leB = step(node_2279,node_6262);
                float node_7764 = (-1*node_6262);
                float node_3892 = lerp((node_3892_if_leA*node_7764)+(node_3892_if_leB*node_2279),node_7764,node_3892_if_leA*node_3892_if_leB);
                float3 node_1297 = float3((node_3444.r+node_3444.g),(node_3787*node_3787),(node_3892*node_3892));
                float3 diffuseColor = (node_1297.r*_Rock.rgb + node_1297.g*_Grass.rgb + node_1297.b*_Rock.rgb);
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal d3d11_9x 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float4 _Grass;
            uniform float4 _Rock;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 posWorld : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
                LIGHTING_COORDS(2,3)
                UNITY_FOG_COORDS(4)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos(v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float2 node_6533 = abs(i.normalDir.rb);
                float2 node_3444 = (node_6533*node_6533).rg;
                float node_6262 = i.normalDir.g;
                float node_2279 = 0.0;
                float node_8246_if_leA = step(node_6262,node_2279);
                float node_8246_if_leB = step(node_2279,node_6262);
                float node_8246 = lerp((node_8246_if_leA*node_2279)+(node_8246_if_leB*node_6262),node_6262,node_8246_if_leA*node_8246_if_leB);
                float node_3787 = (node_8246*node_8246);
                float node_3892_if_leA = step(node_6262,node_2279);
                float node_3892_if_leB = step(node_2279,node_6262);
                float node_7764 = (-1*node_6262);
                float node_3892 = lerp((node_3892_if_leA*node_7764)+(node_3892_if_leB*node_2279),node_7764,node_3892_if_leA*node_3892_if_leB);
                float3 node_1297 = float3((node_3444.r+node_3444.g),(node_3787*node_3787),(node_3892*node_3892));
                float3 diffuseColor = (node_1297.r*_Rock.rgb + node_1297.g*_Grass.rgb + node_1297.b*_Rock.rgb);
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse;
                fixed4 finalRGBA = fixed4(finalColor * 1,0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
