using UnityEngine;

namespace Metro
{
    /// <summary>
    /// 
    /// </summary>
    [CreateAssetMenu(fileName = "Entity Physics Data", menuName = "Entity/PhysicsData")]
    public class EntityPhysicsDataSO : ScriptableObject
    {
        [Header("Collision")]
        [Tooltip("The size of the entity that should detect collision.")]
        [SerializeField] private Bounds _entityBounds;
        [Tooltip("The layer mask to look for when checking for ground.")]
        [SerializeField] private LayerMask _groundLayer;
        [Tooltip("The number of rays to shoot from each side, more is better at the cost of performance.")]
        [SerializeField] private int _detectorCount = 5;
        [Tooltip("How far out to check for collision from the entity.")]
        [SerializeField] private float _detectionRayLength = 0.1f;
        [Tooltip("Prevents side detectors from hitting the ground.")]
        [SerializeField] [Range(0.1f, 0.3f)] private float _rayBuffer = 0.1f;
        
        [Header("Gravity")]
        [Tooltip("Makes sure not to fall faster than this. (Including outside sources)")]
        [SerializeField] private float _fallClamp = -40f;
        [SerializeField] private float _minFallSpeed = 80f;
        [SerializeField] private float _maxFallSpeed = 120f;

        #region Properties

        public Bounds EntityBounds => _entityBounds;
        public LayerMask GroundLayer => _groundLayer;
        public int DetectorCount => _detectorCount;
        public float DetectionRayLength => _detectionRayLength;
        public float RayBuffer => _rayBuffer;
        
        public float FallClamp => _fallClamp;
        public float MinFallSpeed => _minFallSpeed;
        public float MaxFallSpeed => _maxFallSpeed;

        #endregion
    }
}