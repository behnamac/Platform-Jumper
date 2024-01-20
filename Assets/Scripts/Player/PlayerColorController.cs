using UnityEngine;

namespace Player
{
    public class PlayerColorController : MonoBehaviour
    {
        [SerializeField, Tooltip("Mesh renderers to change color.")]
        private Renderer[] meshRenderers;

        public Color PlayerColor { get; private set; }

        /// <summary>
        /// Changes the color of the player.
        /// </summary>
        /// <param name="color">The new color to apply.</param>
        public void ChangeColor(Color color)
        {
            PlayerColor = color;

            if (meshRenderers != null)
            {
                foreach (var renderer in meshRenderers)
                {
                    if (renderer != null)
                    {
                        renderer.material.color = PlayerColor;
                    }
                }
            }
        }
    }
}
