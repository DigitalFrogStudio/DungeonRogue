using UnityEngine;

namespace Assets.DungeonRogue.Scripts.Items
{
    [CreateAssetMenu]
    public class ItemScriptableObject : ScriptableObject
    {
        [SerializeField]
        private GameObject prefab;

        [SerializeField]
        private ItemData data;

        public ItemData Data => data;

        public GameObject Prefab => prefab;
    }
}