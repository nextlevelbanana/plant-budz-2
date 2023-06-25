using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WobblyText : MonoBehaviour
{
    public TMP_Text text;
    public float frequency = 2f;
    public float rippleAmt = 0.01f;
    public float height = 10f;
    private void Update()
    {
        text.ForceMeshUpdate();
        var textInfo = text.textInfo;

        for (int i = 0; i < textInfo.characterCount; ++i)
        {
            var charInfo = textInfo.characterInfo[i];

            if (!charInfo.isVisible) { continue; }
            //each character mesh contains 4 verts that need independent modification...
            var verts = textInfo.meshInfo[charInfo.materialReferenceIndex].vertices;

            for (int j = 0; j < 4; ++j)
            {
                //loop through each vert and adjust position
                var orig = verts[charInfo.vertexIndex + j];
                verts[charInfo.vertexIndex + j] = orig + new Vector3(0, Mathf.Sin(Time.time * frequency + orig.x * rippleAmt) * height, 0);
            }
        }

        for(int i = 0; i < textInfo.meshInfo.Length; ++i)
        {
            //update display
            var meshInfo = textInfo.meshInfo[i];
            meshInfo.mesh.vertices = meshInfo.vertices;
            text.UpdateGeometry(meshInfo.mesh, i);
        }
    }
}
