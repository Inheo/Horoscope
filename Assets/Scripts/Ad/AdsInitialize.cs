using UnityEngine;
using UnityEngine.Advertisements;

public class AdsInitialize : MonoBehaviour
{
    #region
    private string gameId = "4203629";
    #endregion

    private void Start()
    {
        Advertisement.Initialize(gameId, false);
    }
}
