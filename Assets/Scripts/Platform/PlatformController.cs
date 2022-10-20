using UnityEngine;

namespace Platform
{
    public class PlatformController : MonoBehaviour
    {
        [SerializeField] private Renderer[] meshRenderers;
        [SerializeField] private GameObject springMesh;
        [SerializeField] private GameObject gateMesh;
        
        public Color platformColor;
        public bool spring;
        [ConditionalHide(nameof(spring), true)]
        public float targetJump;
        [ConditionalHide(nameof(spring), true)]
        public float curve;
        public bool changeColor;

        private void Awake()
        {
            for (int i = 0; i < meshRenderers.Length; i++)
            {
                meshRenderers[i].material.color = platformColor;
            }

            springMesh.SetActive(spring);
            gateMesh.SetActive(changeColor);
        }

        private void OnValidate()
        {
            if (springMesh)
                springMesh.SetActive(spring);
            
            if (gateMesh)
                gateMesh.SetActive(changeColor);
        }
    }
}
