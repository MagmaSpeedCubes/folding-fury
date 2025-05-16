using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BossFight : MonoBehaviour
{
    [SerializeField] private int level;
    [SerializeField] private float activationTime;
    [SerializeField] private List<GameObject> hubs;
    [SerializeField] private int phases;
    private Camera mainCamera;

    private int index;

    void Start()
    {
        mainCamera = Camera.main;
        index = 0;
        phases = hubs.Count;
    }

    void Update()
    {
        if (!GameInfo.BossFight && GameInfo.GameMode == level && Timer.GetTime() > activationTime)
        {
            ActivateBossFight();
            Debug.Log("Started Boss Fight");
        }
    }

    public IEnumerator StartNextPhase()
    {
        yield return new WaitForSeconds(4);
        if (index == hubs.Count)
        {
            EndBossFight();
            Debug.Log("Ended Boss Fight");
        }
        else
        {
            StartCoroutine(MoveCameraAndStartPhase());
        }
    }

    private IEnumerator MoveCameraAndStartPhase()
    {
        Vector3 startPosition = mainCamera.transform.position;
        Vector3 targetPosition = startPosition + new Vector3(0, 10f, 0); // Move up by 10 units

        float duration = 1f; // Duration of the camera movement
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            mainCamera.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
            yield return null;
        }

        mainCamera.transform.position = targetPosition;

        GameObject hub = hubs[index];
        PhaseHub hubScript = hub.GetComponent<PhaseHub>();
        hubScript.ActivatePhase();
        index += 1;

        Debug.Log("Started Next Phase");
    }

    private void ActivateBossFight()
    {
        GameInfo.BossFight = true;
        GameInfo.BossFightStart = Timer.GetTime();
        StartCoroutine(StartNextPhase());
    }

    private void EndBossFight()
    {
        GameInfo.BossFight = false;
        EndStage.CompleteStage(true);
    }
}