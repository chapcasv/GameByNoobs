using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace PH
{
    public class BattleLifeUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI enemyLifeText;
        [SerializeField] TextMeshProUGUI playerLifeText;

        private LifeSystem _lifeSystem;


        public void Constructor(LifeSystem LS)
        {
            _lifeSystem = LS;
            AddListerner();
            DisplayEnemyLife();
            DisplayPlayerLife();

        }

        private void DisplayEnemyLife()
        {
            int life = _lifeSystem.GetEnemyLife();
            enemyLifeText.text = life.ToString();
        }

        private void DisplayPlayerLife()
        {
            int life = _lifeSystem.GetPlayerLife();
            playerLifeText.text = life.ToString();
        }

        private void AddListerner()
        {
            _lifeSystem.OnEnemyLifeChange += DisplayEnemyLife;
            _lifeSystem.OnPlayerLifeChange += DisplayPlayerLife;
        }

        private void RemoveListerner()
        {
            _lifeSystem.OnEnemyLifeChange -= DisplayEnemyLife;
            _lifeSystem.OnPlayerLifeChange -= DisplayPlayerLife;
        }

        private void OnDisable()
        {
            RemoveListerner();
        }
    }
}

