using UnityEngine;

namespace Player
{
    public class PlayerColorController : MonoBehaviour
    {
        [SerializeField] private Renderer[] meshRenderers;
        public Color playerColor { get; private set; }

        public void ChangeColor(Color color)
        {
            playerColor = color;

            for (int i = 0; i < meshRenderers.Length; i++)
            {
                meshRenderers[i].material.color = playerColor;
            }
        }
    }
}
