using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PH
{
    public class BoardGeneration : MonoBehaviour
    {
        [SerializeField] Transform boardParent;
        private PlayModeEnemy _currentEnemy;

        void Start()
        {
            //IniBoard();
        }

        private void IniBoard()
        {
            Instantiate(_currentEnemy.boardPrefab, boardParent);
        }

        public void Constructor(PlayModeEnemy enemy)
        {
            _currentEnemy = enemy;
        }
        
    }
}

