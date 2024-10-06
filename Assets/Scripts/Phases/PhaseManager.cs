using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;

public class PhaseManager : MonoBehaviour
{
    [SerializeField] EnemySpawner antSpawner;
    [SerializeField] EnemySpawner termiteSpawner;
    [SerializeField] EnemySpawner ladybugSpawner;
    [SerializeField] EnemySpawner beetleSpawner;

    [SerializeField] float timeBeetwenPhases;

    [SerializeField] PhaseDatabase phaseDatabase;

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
            yield return new WaitForSeconds(timeBeetwenPhases);

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
        
    }

}
