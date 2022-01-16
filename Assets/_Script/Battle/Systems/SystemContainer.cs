using UnityEngine;

namespace PH
{   
    [CreateAssetMenu(menuName = "ScriptableObject/Battle System/Container")]
    public class SystemContainer : ScriptableObject
    {
        [SerializeField] MemberSystem memberSystem;
        [SerializeField] LifeSystem lifeSystem;
        [SerializeField] WaveSystem wavesSystem;
        [SerializeField] CoinSystem coinSystem;
        [SerializeField] DeckSystem deckSystem;
        [SerializeField] ResultSystem resultSystem;
        [SerializeField] SpawnSystem spawnSystem;

        public MemberSystem GetMemberSystem() => memberSystem;
        public LifeSystem GetLifeSystem() => lifeSystem;
        public WaveSystem GetWaveSystem() => wavesSystem;
        public CoinSystem GetCoinSystem() => coinSystem;
        public DeckSystem GetDeckSystem() => deckSystem;
        public ResultSystem GetResultSystem() => resultSystem;
        public SpawnSystem GetSpawnSystem() => spawnSystem;

    }
}

