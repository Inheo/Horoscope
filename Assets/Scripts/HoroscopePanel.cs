using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HoroscopePanel : OpenOrClosePanelWithFade
{
    [SerializeField] private Canvas _canvas;

    [SerializeField] private Image _zodiacSign;
    [SerializeField] private Text _zodiacName;
    [SerializeField] private Text _zodiacTimeText;

    [SerializeField] private Sprite[] _zodiacIcons;

    [Header("For refresh UI")]
    [SerializeField] private VerticalLayoutGroup _predicationAndSelectedHoroscope;
    [SerializeField] private VerticalLayoutGroup _predicationPanel;
    [SerializeField] private RectTransform _horoscopeScrollView;

    [SerializeField] private Text[] _daysText;
    [SerializeField] private Button[] _daysButton;

    [SerializeField] private Text _descriptionText;
    [SerializeField] private Text _titleText;

    [SerializeField] private GameObject _buttonForShowPredication;

    [Header("CheckMark")]
    [SerializeField] private Image _checkMark;

    [Header("Color for selected or unselected day")]
    [SerializeField] private Color _selectedDayColor;
    [SerializeField] private Color _unselectedDayColor;

    [SerializeField] private float _animationDuration = 0.2f;

    [SerializeField] private ProfilePanel _profilePanel;

    private string _dataAsJson;
    public Predication _predication;

    private int _indexLastSelectedDay;

    private int[] _randomPredications = new int[3];

    private int[] _differenceBetweenToday = new int[3] { -1, 0, 1 };

    private string[] _nameOfMonths = new string[12] { "Января", "Февраля", "Марта", "Апреля", "Майя", "Июня", "Июля", "Августа", "Сентября", "Октября", "Ноября", "Декабря" };

    private string[] _zodiacTimes = new string[12] { "21 марта — 20 апреля", "21 апреля — 21 мая", "22 мая — 21 июня", "22 июня — 22 июля", "23 июля — 21 августа", "22 августа — 23 сентября",
                                                    "24 сентября — 23 октября", "24 октября — 22 ноября", "23 ноября — 22 декабря", "23 декабря — 20 января", "21 января — 19 февраля", "20 февраля — 20 марта" };
    private string[] _zodiacNames = new string[12] { "Овен", "Телец", "Близнецы", "Рак", "Лев", "Дева", "Весы", "Скорпион", "Стрелец", "Козерог", "Водолей", "Рыбы" };

    public static HoroscopePanel Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        LoadPredications();
        SetRandomPredication();
    }

    private void Update()
    {
        RefreshUI();
    }

    private void LateUpdate()
    {
        _horoscopeScrollView.sizeDelta = new Vector2(_horoscopeScrollView.sizeDelta.x, _horoscopeScrollView.sizeDelta.y + (_horoscopeScrollView.anchoredPosition.y + (Screen.height / _canvas.scaleFactor) * 0.7f));
    }




    public override void Open()
    {
        UpdateUI();
        base.Open();
    }


    private void LoadPredications()
    {
        BetterStreamingAssets.Initialize();
        _dataAsJson = BetterStreamingAssets.ReadAllText("predications.json");

        _predication = JsonUtility.FromJson<Predication>(_dataAsJson);
    }

    private void SetRandomPredication()
    {
        System.DateTime time = System.DateTime.Parse(Game.Instance.LongParametrs.TimeLastViewedAd);
        if (time.Day != System.DateTime.Now.Day && time.Month != System.DateTime.Now.Month)
        {
            for (int i = 0; i < Game.Instance.LongParametrs.Predications.Length; i++)
            {
                for (int j = 0; j < Game.Instance.LongParametrs.Predications[i].IndexsPredication.Length; j++)
                {
                    Game.Instance.LongParametrs.Predications[i].IndexsPredication[j] = Random.Range(0, _predication.Items.Length);
                }
            }
            Game.Instance.LongParametrs.TimeLastViewedAd = System.DateTime.Now.ToString();
        }
    }

    private void SetStartParamertrs()
    {
        if (!TemporaryVariables.ViewedAdsForZodiac[TemporaryVariables.SelectedZodiac])
        {
            _buttonForShowPredication.SetActive(true);
            _titleText.gameObject.SetActive(false);
            _descriptionText.text = "Что бы открыть доступ посмотрите рекламу";
            _checkMark.color = Vector4.zero;
            _descriptionText.rectTransform.DOAnchorPosY(-200, 0.1f);
        }
        else
        {
            _buttonForShowPredication.SetActive(false);
            for (int i = 0; i < _daysButton.Length; i++)
            {
                _daysButton[i].interactable = true;
            }
            ShowResult(1);
        }
    }

    private void RefreshUI()
    {
        //_horoscopeScrollView.anchoredPosition = new Vector2(_horoscopeScrollView.anchoredPosition.x, 100);

        _predicationAndSelectedHoroscope.enabled = false;
        _predicationPanel.enabled = false;

        _predicationAndSelectedHoroscope.enabled = true;
        _predicationPanel.enabled = true;
    }

    public override void Close()
    {
        base.Close();
        MenuPanel.Instance.Open();
    }

    public void ShowResultAds()
    {
        for (int i = 0; i < _daysButton.Length; i++)
        {
            _daysButton[i].interactable = true;
        }

        _buttonForShowPredication.SetActive(false);
        TemporaryVariables.ViewedAdsForZodiac[TemporaryVariables.SelectedZodiac] = true;
        ShowResult(1);
    }

    public void ShowResult(int i)
    {
        System.DateTime nowTime = System.DateTime.Now.AddDays(_differenceBetweenToday[i]);

        _checkMark.color = Color.white;

        _titleText.text = $"{nowTime.Day} {_nameOfMonths[nowTime.Month - 1]}";
        _titleText.gameObject.SetActive(true);

        _descriptionText.text = _predication.Items[Game.Instance.LongParametrs.Predications[TemporaryVariables.SelectedZodiac].IndexsPredication[i]] + "\n";
        _daysText[_indexLastSelectedDay].DOColor(_unselectedDayColor, _animationDuration);

        _daysText[i].DOColor(_selectedDayColor, _animationDuration);

        _checkMark.rectTransform.DOAnchorPos(_daysText[i].rectTransform.anchoredPosition, _animationDuration)
            .OnUpdate (() => RefreshUI());

        _indexLastSelectedDay = i;
    }

    public void UpdateUI()
    {
        if (!TemporaryVariables.ViewedAdsForZodiac[TemporaryVariables.SelectedZodiac])
        {
            for (int i = 0; i < _daysButton.Length; i++)
            {
                _daysButton[i].interactable = false;
                _daysText[i].color = _unselectedDayColor;
            }
        }

        _zodiacSign.sprite = _zodiacIcons[TemporaryVariables.SelectedZodiac];
        _zodiacName.text = _zodiacNames[TemporaryVariables.SelectedZodiac];
        _zodiacTimeText.text = _zodiacTimes[TemporaryVariables.SelectedZodiac];

        SetStartParamertrs();

        RefreshUI();
    }

    public void ChangeZodiac(int i)
    {
        TemporaryVariables.SelectedZodiac = i;
        UpdateUI();

        RefreshUI();
    }

    public void OpenProfilePanel()
    {
        _profilePanel.Open();
    }
}