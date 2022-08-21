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
                TryFreeCurrent();
            }
        }

        private void ScanHit(RaycastHit hitInfo)
        {
            GameObject hitGameObject = hitInfo.collider.gameObject;

            bool isContainsPointable = hitGameObject.TryGetComponent(out OutlinableObject outlinable);
            if (isContainsPointable == true)
            {
                currentOutlinable = outlinable;

                outlinable.SetOutline(true);
            }
            else
            {
                TryFreeCurrent();
            }
        }

        private void TryFreeCurrent()
        {
            if (currentOutlinable != null)
            {
                currentOutlinable.SetOutline(false);

                currentOutlinable = null;
            }
        }
    }
}