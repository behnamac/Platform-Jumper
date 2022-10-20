using UnityEngine;

namespace Controllers
{
    public class CameraController : MonoBehaviour
    {
        public static CameraController instance { get; private set; }


        #region SERIALIZE FIELDS

        [SerializeField] private Transform target;
        [SerializeField] private float followSpeed = 0.1f;
        
        #endregion

        #region PRIVATE METHODS

        private void Initialize()
        {
            // SET DEFAULT OFFSET
        }

        private void SmoothFollow()
        {
            var targetPos = target.position;
            var thisTransform = transform;
            var smoothFollow = Vector3.Lerp(thisTransform.position, targetPos, followSpeed);
            thisTransform.position = smoothFollow;
            thisTransform.rotation = Quaternion.Lerp(thisTransform.rotation, target.rotation, 0.1f);
        }

        #endregion

        #region UNITY EVENT METHODS

        private void Awake()
        {
            if (instance == null) instance = this;
        }

        private void Start() => Initialize();

        private void LateUpdate() => SmoothFollow();

        #endregion
    }
}