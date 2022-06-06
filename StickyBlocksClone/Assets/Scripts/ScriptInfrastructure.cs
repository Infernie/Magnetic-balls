using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ScriptInfrastructure : MonoBehaviour
{
    public static ScriptInfrastructure instance;
    public static int levelCounter = 1;
    bool isTouchedFirstTime = false;
    public List<ParticleSystem> particleSystems = new List<ParticleSystem>();

    private void Awake()
    {
        instance = this;

    }

    #region Scripts
    public CollectedObjController COC;
    public UIHandler UIH;
    public SwerveMovement swerveMovement;
    #endregion
    #region Assets

    #region UI
    public TextMeshProUGUI startLevelText;
    public TextMeshProUGUI nextLevelText;
    public GameObject tapToPlay;
    public GameObject tapToPlayBG;
    #endregion
    #endregion

    private void Start()
    {
        Application.targetFrameRate = 60;
        Time.timeScale = 0;
        swerveMovement.zSpeed = 0;

    }

    private void Update()
    {
        if (!isTouchedFirstTime)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Time.timeScale = 1;
                swerveMovement.zSpeed = 0.07f;
                tapToPlay.SetActive(false);
                tapToPlayBG.SetActive(false);

                isTouchedFirstTime = true;
            }
        }    
    }

   
}
