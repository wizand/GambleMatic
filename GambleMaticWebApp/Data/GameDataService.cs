using GambleMaticDataLib;

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
}