using UnityEngine;

public class Game : MonoBehaviour
{
    public LongTermParameters LongParametrs { get; private set; }
    [SerializeField] private CanvasGroup _profileCanvasGroup;

    private PushNotification _pushNotification = new PushNotification();

    public static Game Instance;

    private void Awake()
    {
        Instance = this;

        LongParametrs = new LongTermParameters();

        LongParametrs.LoadFromFile();

        if (LongParametrs.FirstEntry)
        {
            _profileCanvasGroup.alpha = 1;
            _profileCanvasGroup.interactable = true;
            _profileCanvasGroup.blocksRaycasts = true;
            LongParametrs.FirstEntry = false;
        }
    }
    private void Start()
    {
        _pushNotification.Initialize();
        _pushNotification.MakingNotifications();
    }

    public void SaveParametrs()
    {
        LongParametrs.SaveToFile();
    }

    private void OnApplicationQuit()
    {
        SaveParametrs();
    }

    private void OnApplicationPause(bool pause)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            SaveParametrs();
        }
    }
}
