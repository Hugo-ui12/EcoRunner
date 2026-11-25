
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class TMPTextArc : MonoBehaviour
{
    [Header("Curvatura del titulo")]
    [Range(0f, 100f)]
    public float arcHeight = 30f;

    private TMP_Text textComponent;

    void Awake()
    {
        textComponent = GetComponent<TMP_Text>();
        textComponent.OnPreRenderText += ApplyArc;
    }

    void OnDestroy()
    {
        if (textComponent != null)
            textComponent.OnPreRenderText -= ApplyArc;
    }

    void ApplyArc(TMP_TextInfo textInfo)
    {
        if (textInfo.characterCount == 0) return;

        float minX = float.MaxValue;
        float maxX = float.MinValue;

        // Calcular el ancho del titulo
        for (int i = 0; i < textInfo.characterCount; i++)
        {
            TMP_CharacterInfo character = textInfo.characterInfo[i];

            if (!character.isVisible) continue;

            minX = Mathf.Min(minX, character.bottomLeft.x);
            maxX = Mathf.Max(maxX, character.topRight.x);
        }

        float halfWidth = (maxX - minX) / 2f;

        if (halfWidth <= 0f) return;

        float centerX = (minX + maxX) / 2f;

        // Aplicar la curvatura
        for (int i = 0; i < textInfo.characterCount; i++)
        {
            TMP_CharacterInfo character = textInfo.characterInfo[i];

            if (!character.isVisible) continue;

            int materialIndex = character.materialReferenceIndex;
            int vertexIndex = character.vertexIndex;

            Vector3[] vertices =
                textInfo.meshInfo[materialIndex].vertices;

            for (int j = 0; j < 4; j++)
            {
                Vector3 vertex = vertices[vertexIndex + j];

                float x = (vertex.x - centerX) / halfWidth;

                vertex.y += arcHeight * (1f - x * x);

                vertices[vertexIndex + j] = vertex;
            }
        }

        textComponent.UpdateVertexData(
            TMP_VertexDataUpdateFlags.Vertices
        );
    }
}
