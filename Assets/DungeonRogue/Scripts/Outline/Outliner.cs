using UnityEngine;
using UnityEngine.Assertions;

namespace Assets.DungeonRogue.Scripts
{
    public class Outliner : MonoBehaviour
    {
        [SerializeField]
        private Camera playerCamera = default;

        [SerializeField]
        private float maxRaycastDistance = 3.0f;

        private OutlinableObject currentOutlinable;

        private bool hasAcquiredObject;

        private void Awake()
        {
            Assert.IsNotNull(playerCamera);
        }

        private void Update()
        {
            CastRay();
        }

        private void CastRay()
        {
            RaycastHit hitInfo;
            bool raycastResult = Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hitInfo, maxRaycastDistance);

            if (raycastResult == true)
            {
                ScanHit(hitInfo);
            }
            else
            {
                FreeCurrent();
            }
        }

        private void ScanHit(RaycastHit hitInfo)
        {
            GameObject hitGameObject = hitInfo.collider.gameObject;

            bool isContainsPointable = hitGameObject.TryGetComponent(out OutlinableObject outlinable);
            if (isContainsPointable == true)
            {
                if (hasAcquiredObject == false)
                { 
                    hasAcquiredObject = true;

                    currentOutlinable = outlinable;

                    outlinable.SetOutline(true);
                }
            }
            else
            {
                FreeCurrent();
            }
        }

        private void FreeCurrent()
        {
            hasAcquiredObject = false;
            
            if (currentOutlinable != null)
            {
                currentOutlinable.SetOutline(false);

                currentOutlinable = null;
            }
        }
    }
}