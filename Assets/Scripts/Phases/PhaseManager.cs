using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.Examples;
using UnityEngine;

public class PhaseManager : MonoBehaviour
{
    [Header("Ennemies Spawners")]
    [SerializeField] EnemySpawner antSpawner;
    [SerializeField] EnemySpawner termiteSpawner;
    [SerializeField] EnemySpawner ladybugSpawner;
    [SerializeField] EnemySpawner beetleSpawner;

    [Header("Waiting Times")]
    [SerializeField] float timeAtStart;
    [SerializeField] float timeBeetwenPhases;

    [Header("Phase Handling")]
    [SerializeField] PhaseDatabase phaseDatabase;
    [SerializeField] private TMP_Text displayedPhase;
    public GameObject winMenu;

    private int actualPhase;
    private int maxPhases;

    private void Awake()
    {
        winMenu.SetActive(false);
        maxPhases = phaseDatabase.GetPhases().Length;
        actualPhase = 0;
        displayedPhase.text = string.Format("{0}/{1}", actualPhase, maxPhases);
    }

    void Start()
    {
        StartCoroutine(LaunchGame());//on lance le jeu, les phases
    }


    IEnumerator LaunchGame()
    {
        //on boucle dans les différentes phases
        foreach (PhaseData phase in phaseDatabase.GetPhases())
        {

            //en debut de chaque phase, il y a un temps à attendre pour que  le joueur se prépare
            if (actualPhase == 0)
            {
                yield return new WaitForSeconds(timeAtStart);
            } else
            {
                yield return new WaitForSeconds(timeBeetwenPhases);
            }
            

            actualPhase += 1;
            displayedPhase.text = string.Format("{0}/{1}", actualPhase, maxPhases);

            //on boucle dans les différentes étapes
            foreach (string step in phase.GetSteps())
            {
                //Les phases sont découpées en etapes/actions
                string action = step.ToLower();

                //concrètement, chaque action est un string
                //si ce string est le nom d'un insecte attaquant, on le fait spawn
                //si ce string est convertissable en float, on attend cette durée
                //sinon il se passe rien
                switch (action)
                {
                    case "ant":
                        antSpawner.SpawnEnemy();
                        break;
                    case "termite":
                        termiteSpawner.SpawnEnemy();
                        break;
                    case "ladybug":
                        ladybugSpawner.SpawnEnemy();
                        break;
                    case "beetle":
                        beetleSpawner.SpawnEnemy();
                        break;
                    default:
                        float timeToWait = 0f;
                        float.TryParse(step, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out timeToWait);
                        yield return new WaitForSeconds(timeToWait);
                        
                        break;
                }
            }
        }

        yield return new WaitForSeconds(15);
        winMenu.SetActive(true);
        Time.timeScale = 0f;

    }

}
