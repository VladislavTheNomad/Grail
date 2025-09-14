using UnityEngine;

namespace Grail
{
    public class Poison : MonoBehaviour, IEnemyBattleEffect
    {
        [SerializeField, Range(0, 1)] float effectChance;
        
        public void DoEnemyBattleEffect()
        {
            float roll = Random.value;
            if(roll <= effectChance)
            {
                BattleManager.Instance.SetStatus(Statuses.Poison, Sides.Player);
            }
        }

        public string GetInfoAboutEffect()
        {
            return null;
        }
    }
}