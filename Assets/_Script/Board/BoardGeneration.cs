using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PH
{
    public class BoardGeneration : MonoBehaviour
    {
        [SerializeField] PVE_Raid currentRaid;
        [SerializeField] Transform boardParent;

        // Start is called before the first frame update
        void Start()
        {
            IniBoard();
        }

        private void IniBoard()
        {
            Instantiate(currentRaid.boardPrefab, boardParent);
        }

        
    }
}

