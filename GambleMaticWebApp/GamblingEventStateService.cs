using GambleMaticWebApp.Data;
public class GamblingEventStateService
{

    private readonly ILogger<GamblingEventStateService> _logger;
    private readonly GamblingDataService _dataService;
    public List<GamblingEvent> AllGamblingEvents { get; set; } = null;
    public GamblingEvent SelectedGamblingEvent { get; set; } = null;
    public List<GambleItemModel> GambleItemModelsInSelectedGamlingEvent { get; set; } = null;

    public GamblingEventStateService(ILogger<GamblingEventStateService> logger, GamblingDataService dataService)
    {
        _logger = logger;
        _dataService = dataService;

        // allGamblingEvents = _dataService.GetGamblingEventModelsFromDatabaseAsync().Result;
        // selectedGamblingEvent = allGamblingEvents[0];
    }




    public async Task UpdateGamblingEvents()
    {
        AllGamblingEvents = await _dataService.GetGamblingEventModelsFromDatabaseAsync();
        _storedGamblingViewModels = null;
    }
    public async Task<List<GamblingEvent>> GetGamblingEvents()
    {
        if (AllGamblingEvents == null)
        {
            AllGamblingEvents = await _dataService.GetGamblingEventModelsFromDatabaseAsync();
        }
        return AllGamblingEvents;
    }

    private List<GamblingEventViewModel> _storedGamblingViewModels = null;
    public async Task<List<GamblingEventViewModel>> GetAllGamblingEventViewModels()
    {
        if ( _storedGamblingViewModels == null)
        {
            List<GamblingEvent> gamblingEvents = await GetGamblingEvents();
            _storedGamblingViewModels = gamblingEvents.Select(g => new GamblingEventViewModel(g)).ToList();
        }
        return _storedGamblingViewModels;
    }

    public void SetSelectedGamblingEvent(int index)
    {
        SelectedGamblingEvent = AllGamblingEvents[index];
        GambleItemModelsInSelectedGamlingEvent = null;
    }

    public async Task<GamblingEvent> GetSelectedGamblingEvent()
    {
        if (SelectedGamblingEvent == null)
        {
            await UpdateGamblingEvents();
            SelectedGamblingEvent = AllGamblingEvents[0];
        }
        return SelectedGamblingEvent;
    }

    public async Task<GamblingEventViewModel> GetSelectedGamblingEventAsViewModel()
    {
        return new GamblingEventViewModel(await GetSelectedGamblingEvent());
    }

    public async Task<List<GambleItemModel>> GetAllGambleItemsForSelectedEvent()
    {
        if (GambleItemModelsInSelectedGamlingEvent == null)
        {
            SelectedGamblingEvent = await GetSelectedGamblingEvent();
            GambleItemModelsInSelectedGamlingEvent = await _dataService.GetGambleItemsForGamblingEvent(SelectedGamblingEvent);
        }
        return GambleItemModelsInSelectedGamlingEvent;
    }
}
