using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class DisplayEnergy : MonoBehaviour
{
    Text m_Text;
    Scene m_ActiveScene;
    private DeciLevel01 deciLevel01 = new DeciLevel01();
    // Start is called before the first frame update
    void Start()
    {
        m_Text = GetComponent<Text>();
       m_ActiveScene = SceneManager.GetActiveScene(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (m_ActiveScene.name == "DeciLevel01")
        {
            m_Text.text = "Energy: " + deciLevel01.Energy;
        }
    }
}
