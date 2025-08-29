using UnityEngine;

namespace Grail
{
    [CreateAssetMenu(fileName = "ObjectProperties", menuName = "Scriptable Objects/ObjectProperties")]
    public class ObjectProperties : ScriptableObject
    {
        [Header("InputSettings")]
        [SerializeField] public float pauseTimeBetweenTurns;

        [Header ("GoldPile")]
        [SerializeField] public int goldPile_minGoldFromPile;
        [SerializeField] public int goldPile_maxGoldFromPile;

        [Header("Smith")]
        [SerializeField] public int smith_costInGold;
        [SerializeField] public int smith_mightBonus;

        [Header("DonkeyOnRoad")]
        [SerializeField] public int donkeyOnRoad_giveGold;
        [SerializeField] public int donkeyOnRoad_getCrystals;
        [SerializeField] public int donkeyOnRoad_spentTurnsMin;
        [SerializeField] public int donkeyOnRoad_spentTurnsMax;
    }
}
