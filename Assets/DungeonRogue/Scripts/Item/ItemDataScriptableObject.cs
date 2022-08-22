using UnityEngine;

namespace Assets.DungeonRogue.Scripts.Item
{
    [CreateAssetMenu]
    public class ItemDataScriptableObject : ScriptableObject
    {
        [SerializeField]
        private ItemData data;

        public ItemData Data => data;
    }
}