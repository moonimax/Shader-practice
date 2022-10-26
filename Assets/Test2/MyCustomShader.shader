Shader "Custom/MyCustomShader"
{
    Properties
    {
        _Color("Color", Color) = (1,1,1,1)
        _MainTex("Albedo (RGB)", 2D) = "white" {}
    }
        SubShader
    {
        Tags {"Queue" = "Overlay"}
        Pass {
            Cull Front
            Material {
                Diffuse[_Color]
                Ambient[_Color]
            }
            Blend DstColor srcColor

            Lighting On
            SetTexture[_MainTex] {
                Combine Texture * Primary DOUBLE
            }
        }
    }
        FallBack "Diffuse"
}
