using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class PixelationEffect : MonoBehaviour
{
    public Shader pixelationShader;

    [Range(10, 1000f)]
    public float pixelRadius = 10f;

    private Material pixelationMaterial;

    void Start()
    {
        if (pixelationShader == null)
        {
            Debug.LogError("Vortex shader not assigned!");
            enabled = false;
            return;
        }

        pixelationMaterial = new Material(pixelationShader);
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (pixelationMaterial == null)
        {
            Graphics.Blit(source, destination);
            return;
        }

        pixelationMaterial.SetFloat("_PixelSize", pixelRadius);


        Graphics.Blit(source, destination, pixelationMaterial);
    }
}
