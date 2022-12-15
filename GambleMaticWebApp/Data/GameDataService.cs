using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;

using GambleMaticDataLib;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update.Internal;

public class GameDataService
{

    private GambleMaticDbManager dbManager = null;
    public GambleMaticDbManager DbManager {  
        get
        {
            if (dbManager == null)
            {
                dbManager = new GambleMaticDbManager();
            }
            return dbManager;
        }
}

    public async Task AddGameToDatabaseAsync(GameModelViewModel gameViewModel)
    {
        await AddGameToDatabaseAsync(gameViewModel.GetGameModel());
    }

    public async Task AddGameToDatabaseAsync(GameModel gameModel)
    {
        int result = await DbManager.SaveGameToDatabase(gameModel);
        Console.WriteLine(result + " games saved.");
    }

    public async Task AddPlayerToDatabaseAsync(PlayerModelViewModel PlayerViewModel)
    {
        await AddPlayerToDatabaseAsync(PlayerViewModel.GetPlayerModel());
    }

    public async Task AddPlayerToDatabaseAsync(PlayerModel playerModel)
    {
        int result = await DbManager.SavePlayerToDatabase(playerModel);
        Console.WriteLine(result + " players saved.");
    }



    public async Task<List<GameModel>> GetGameModelsFromDatabaseAsync()
    {
        var list = await DbManager.GetAllGamesFromDatabase();
        if (list == null)
        {
            list = new();
        }
        return list;
    }
    
    public async Task<List<PlayerModel>> GetPlayerModelsFromDatabaseAsync()
    {
        var list = await DbManager.GetAllPlayersFromDatabase();
        if (list == null)
        {
            list = new();
        }
        return list;
    }

    public async Task<List<GambleItemModel>> GetAllGambleRowsForPlayer(int playerId)
    {
        var list = await DbManager.GetGambleRowsForPlayerFromDatabase(playerId);
        if (list == null)
        {
            list = new();
        }
        return list;
    }

    public async Task<List<GambleItemModel>> GetGambleItemsFromDatabaseAsync()
    {
        var list = await DbManager.GetGambleItemsFromDatabaseAsync();
        if (list == null)
        {
            list = new List<GambleItemModel>();
        }

        return list;
    }


    public async Task<ExtraGamblesModel> GetExtraGamblesResultModel()
    {
        var list = await DbManager.GetExtraGamblesResultModelFromDatabase();
        ExtraGamblesModel? extraGamblesModel = null;

        if (list.Count == 0)
        {
            extraGamblesModel = new();
            extraGamblesModel.IsResultObject = true;
            extraGamblesModel.SemifinalTeamOne = "na";
            extraGamblesModel.SemifinalTeamTwo = "na";
            extraGamblesModel.SemifinalTeamThree = "na";
            extraGamblesModel.SemifinalTeamFour = "na";
            extraGamblesModel.SilverTeam = "na";
            extraGamblesModel.GoldTeam = "na";
            Console.WriteLine("No result model. Creating one."); 
            await DbManager.SaveExtraGamblesResultModel(extraGamblesModel);
        }
        else
        {
            if (list.Count > 1)
            {
                Console.WriteLine("TOO MANY EXTRA GAMBLES RESULT MODELS!");
            }
            extraGamblesModel = list.First();
        }
        return extraGamblesModel;
    }

    public async Task<int> SaveExtraGamblesResultModel(ExtraGamblesModel resultModel)
    {
        int count = await DbManager.SaveExtraGamblesResultModel(resultModel);
        return count;
    }


    public async Task<int> RemoveExistingGamblesForPlayer(int playerId)
    {
        int removedCount = await DbManager.DeleteGamblesForPlayer(playerId);
        return removedCount;
    }

    public async Task<int> SaveGambles(List<GambleItemModel> listOfGambles)
    {
        int savedCount = await DbManager.SaveGambles(listOfGambles);
        return savedCount;
    }

    public async Task<int> SaveChangesToPlayer(PlayerModel playerModelUnderEdit)
    {
        int updatedCount = await DbManager.SaveChangesToPlayer(playerModelUnderEdit);
        return updatedCount;
    }

    public async Task<int> UpdateGameResults(GameModel gameModelToUpdate)
    {
        int updatedCount = await DbManager.SaveChangesToGame(gameModelToUpdate);
        return updatedCount;
    }

    public async Task<int> UpdateMultipleGameResults(List<GameModel> gamesToUpdate)
    {
        int updatedCount = await DbManager.SaveChangesToGames(gamesToUpdate);
        return updatedCount;
    }

    public async Task<int> AddExtraGamblesToDatabaseAsync(ExtraGamblesModel extraGamblesModel)
    {
        int storedCount = await DbManager.SaveExtraGamblesToDatabase(extraGamblesModel);
        return storedCount;
    }


    public static string NOT_SELECTED_IDENTIFIER = "-";
    public async void RefreshTeamsCacheFromDb()
    {
        var gameModels = await GetGameModelsFromDatabaseAsync();
        RefreshTeamCache(gameModels);
    }

    public void RefreshTeamsCacheFromList(List<GameModelViewModel> gameViewModels)
    {
        List<GameModel> gmList = new();
        gameViewModels.ForEach(gameViewModel => gmList.Add(gameViewModel.GetGameModel()));

        RefreshTeamCache(gmList);
    }

    private void RefreshTeamCache(List<GameModel> gameModels)
    {
        _teamsCache = new();
        foreach (var gameModel in gameModels)
        {
            if (false == _teamsCache.Contains(gameModel.Home))
            {
                _teamsCache.Add(gameModel.Home);
            }

            if (false == _teamsCache.Contains(gameModel.Away))
            {
                _teamsCache.Add(gameModel.Away);
            }
        }
        _teamsCache.Add(NOT_SELECTED_IDENTIFIER);
        _teamsCache.Sort();
    }

    List<string> _teamsCache = null;
    public List<string> AllTeams { 
        get 
        { 
            if (_teamsCache == null)
            {
                RefreshTeamsCacheFromDb();
            }

            return _teamsCache;
        }
    }

    public List<string> _goalAmountOptions = null;
    public List<string> GoalAmountOptions 
    { 
        get
        {
            if (_goalAmountOptions == null)
            {
                _goalAmountOptions = generateGoalAmounts(76, 235, 4);
            }
            return _goalAmountOptions;
        }
    }

    //76-80
    //81-85
    //86-90
    //...
    private List<string> generateGoalAmounts(int min, int max, int step)
    {
        List<string> goalAmountOptions = new List<string>();

        goalAmountOptions.Add(NOT_SELECTED_IDENTIFIER);

        bool write = false;
        string tmp = "";
        for (int i = min; i <= max; i = i + step)
        {
            if (write)
            {
                tmp = tmp + " - " + i;
                goalAmountOptions.Add(tmp);
                i = i - 3;
            }
            else
            {
                tmp = "" + i;
            }

            write = !write;

        }
        return goalAmountOptions;
    }



}