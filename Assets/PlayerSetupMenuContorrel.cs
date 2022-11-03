using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Bombaman
{
    public class PlayerSetupMenuContorrel : MonoBehaviour
    {

        private int PlayerIndex;

        [SerializeField] private TextMeshProUGUI titleText;
        [SerializeField] private GameObject readyPanel;
        [SerializeField] private GameObject menuPanel;
        // Start is called before the first frame update
        public void SetPlayerIndex(int pi)
		{
            PlayerIndex = pi;
            titleText.SetText("Player " + (pi + 1).ToString());
		}

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
