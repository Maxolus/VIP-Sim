using UnityEngine;

namespace VisSim
{
    public class myCataract : LinkableBaseEffect
    {
        // intensity
        [Linkable, Range(0.0f, 1.0f)]
        public float severityIndex = 0.0f;

        // options
        [Linkable]
        public bool useBrightness = true;
        [Linkable]
        public bool useContrast = true;
        [Linkable]
        public bool useFrosting = true;
        [Linkable]
        public Vector3 ContrastCoeff = new Vector3(0.7f, 0.7f, 0.4f);
        [Linkable, Range(0.1f, 9.9f), Tooltip("Simple power function.")]
        public float Gamma = 1f;

        private Shader secondaryShader;
        private Material secondaryMaterial;
        private Texture2D noiseTexture;

        // init
        protected new void OnEnable()
        {
            base.OnEnable();

            secondaryShader = Shader.Find("Hidden/VisSim/cfxFrost");
            secondaryMaterial = new Material(secondaryShader);

            noiseTexture = GenerateNoiseTexture(256, 256);
            secondaryMaterial.SetTexture("_NoiseTex", noiseTexture);
        }

        protected new void OnDisable()
        {
            if (secondaryMaterial)
            {
                DestroyImmediate(secondaryMaterial);
            }
            secondaryMaterial = null;

            if (noiseTexture)
            {
                DestroyImmediate(noiseTexture);
            }

            base.OnDisable();
        }

        protected override void OnUpdate()
        {
        }

        protected override void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            if (severityIndex <= 0f)
            {
                Graphics.Blit(source, destination);
                return;
            }

            float Brightness = useBrightness ? severityIndex * -100f : 0f;
            float Contrast = useContrast ? severityIndex * -100f : 0f;
            Material.SetVector("_BCG", new Vector4((Brightness + 100f) * 0.01f, (Contrast + 100f) * 0.01f, 1.0f / Gamma));
            Material.SetVector("_Coeffs", ContrastCoeff);

            if (!useFrosting)
            {
                Graphics.Blit(source, destination, Material);
            }
            else
            {
                RenderTexture tmp = RenderTexture.GetTemporary(source.width, source.height, 0);
                Graphics.Blit(source, tmp, Material);

                float Scale = 10f * severityIndex;
                secondaryMaterial.SetFloat("_Scale", Scale);
                Graphics.Blit(tmp, destination, secondaryMaterial, 0);

                RenderTexture.active = null;
                RenderTexture.ReleaseTemporary(tmp);
            }
        }

        protected override string GetShaderName()
        {
            return "Hidden/VisSim/myBrightnessContrastGamma";
        }

        private Texture2D GenerateNoiseTexture(int width, int height)
        {
            Texture2D noiseTex = new Texture2D(width, height);
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    float xCoord = (float)x / width * 20.0f;
                    float yCoord = (float)y / height * 20.0f;
                    float sample = Mathf.PerlinNoise(xCoord, yCoord);
                    noiseTex.SetPixel(x, y, new Color(sample, sample, sample));
                }
            }
            noiseTex.Apply();
            return noiseTex;
        }
    }
}
