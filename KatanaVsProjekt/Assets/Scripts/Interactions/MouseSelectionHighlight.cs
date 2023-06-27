using UnityEngine;

namespace haw.unitytutorium
{
    public class MouseSelectionHighlight : MonoBehaviour
    {
        [SerializeField] private Material highlightMaterial = null;

        private Material[] defaultMaterials;
        private MeshRenderer meshRenderer;

        private void Awake()
        {
            meshRenderer = GetComponent<MeshRenderer>();
            defaultMaterials = meshRenderer.materials;
        }

        private void OnMouseEnter()
        {
            var highLightMaterials = meshRenderer.materials;
            
            for (int i = 0; i < meshRenderer.materials.Length; i++)
            {
                highLightMaterials[i] = highlightMaterial;
            }

            meshRenderer.materials = highLightMaterials;
        }

        private void OnMouseExit()
        {
            meshRenderer.materials = defaultMaterials;
        }
    }
}