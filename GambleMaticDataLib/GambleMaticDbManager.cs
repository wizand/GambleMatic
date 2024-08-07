﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace GambleMaticDataLib
{
    public class GambleMaticDbManager
    {
        public async Task<int> SaveGamblingEventToDatabase(GamblingEvent gamblingEvent)
        {
            using GambleMaticContext gambleMaticContext = new GambleMaticContext();

            gambleMaticContext.GamblingEvents.Add(gamblingEvent);
            int result = await gambleMaticContext.SaveChangesAsync();
            return result;
        }


        public async Task<int> DeleteGamblesForPlayer(int playerId)
        {
            using GambleMaticContext gambleMaticContext = new GambleMaticContext();
            var results = gambleMaticContext.Gambles.Where(g => g.PlayerModel.PlayerModelId == playerId);
       
            gambleMaticContext.RemoveRange(results);
            int deletedCount =  await gambleMaticContext.SaveChangesAsync();
            
            return deletedCount;

        }

        public async Task<int> DeleteGamblesForPlayerInEvent(int playerId, int eventId)
        {
            using GambleMaticContext gambleMaticContext = new GambleMaticContext();
            var gambleItemsForPlayer = gambleMaticContext.Gambles.Where(g => g.PlayerModel.PlayerModelId == playerId);

            var gamesInEvent = await gambleMaticContext.Games.Where(g => g.GamblingEvent.GamblingEventId == eventId).ToListAsync();

            List<GambleItemModel> gambleItemsToRemove = new List<GambleItemModel>();
            foreach ( GambleItemModel gambleItem in gambleItemsForPlayer)
            {
                if (gamesInEvent.Contains(gambleItem.GameModel))
                {
                    gambleItemsToRemove.Add(gambleItem);
                }
            }

            gambleMaticContext.RemoveRange(gambleItemsToRemove);
            int deletedCount = await gambleMaticContext.SaveChangesAsync();

            return deletedCount;

        }

        public async Task<List<ExtraGamblesModel>> GetExtraGamblesResultModelFromDatabase()
        {
            using GambleMaticContext gambleMaticContext = new GambleMaticContext();
            var res = await gambleMaticContext.ExtraGambles.FromSqlRaw("SELECT * from ExtraGambles WHERE IsResultObject = 1").ToListAsync();
            
            return res;
        }

        public async Task<int> SaveExtraGamblesResultModel(ExtraGamblesModel resultModel)
        {
            if (resultModel.IsResultObject == false)
            {
                Console.WriteLine("Model is not result object. Cancelling..");
                return 0;
            }
            using GambleMaticContext gambleMaticContext = new GambleMaticContext();
            gambleMaticContext.ExtraGambles.Update(resultModel);
            int savedOrUpdated = await gambleMaticContext.SaveChangesAsync();
            return savedOrUpdated;
        }

        public async Task<List<GameModel>> GetAllGamesFromDatabase()
        {
            using GambleMaticContext gambleMaticContext = new GambleMaticContext();
            var res = await gambleMaticContext.Games.FromSqlRaw("SELECT * from Games ORDER BY GameOrderSortId asc").ToListAsync();
            //res.OrderBy(g => g.GameOrderSortId);
            return res;
        }

        public async Task<List<PlayerModel>> GetAllPlayersFromDatabase()
        {
            using GambleMaticContext gambleMaticContext = new GambleMaticContext();
            var res = await gambleMaticContext.Players
                //.Include(p => p.ExtraGambles)
                .Include(p=>p.GambleItemModels)
                .ThenInclude(g=>g.GameModel)
                .ToListAsync();
            return res;
        }

        public async Task<List<GambleItemModel>> GetGambleItemsFromDatabaseAsync()
        {
            using GambleMaticContext gambleMaticContext = new GambleMaticContext();
            var res = await gambleMaticContext.Gambles.Include(g => g.GameModel).Include(g => g.PlayerModel).ToListAsync();
            return res;
        }


        public async Task<List<GambleItemModel>> GetGambleItemsForEvent(int GambleEventId)
        {
            using GambleMaticContext gambleMaticContext = new GambleMaticContext();

            var gambleItemsForEvent = await gambleMaticContext.Gambles
                .Include(g => g.GameModel)
                .Include(g => g.PlayerModel)
                .Where(g => g.GameModel.GamblingEvent.GamblingEventId == GambleEventId)
                .ToListAsync();

            //var gamblingEvent = gambleMaticContext.GamblingEvents.Find(GambleEventId);
            //var res = await gambleMaticContext.Gambles.Where(g => g.GameModel.GamblingEvent.GamblingEventId == GambleEventId).ToListAsync();
            return gambleItemsForEvent;
        }

        public async Task<List<GambleItemModel>> GetGambleRowsForPlayerFromDatabase(int playerId)
        {
            using GambleMaticContext gambleMaticContext = new GambleMaticContext();
            List<GambleItemModel> gambleRows = await gambleMaticContext.Gambles.Where(g => g.PlayerModel.PlayerModelId == playerId).ToListAsync();
            return gambleRows;
        }

        public async Task<List<GamblingEvent>> GetGamblingEventsFromDatabase()
        {
            using GambleMaticContext gambleMaticContext = new GambleMaticContext();
            List<GamblingEvent> gamblingEvents = await gambleMaticContext.GamblingEvents
                .Include(ge => ge.Games)
                .Include(ge => ge.ExtraGamblesInEvent).ToListAsync();

            var allPlayers = await GetAllPlayersFromDatabase();


            foreach (var gamblingEvent in gamblingEvents)
            {
                var gambleItems = await GetGambleItemsForEvent(gamblingEvent.GamblingEventId);
                
                List<int?> uniquePlayerIds = gambleItems.Select(g => g.PlayerModelId).Distinct().ToList();
                List<PlayerModel> playersInEvent = new();
                foreach (var uniquePlayerId in uniquePlayerIds)
                {
                    playersInEvent.Add(allPlayers.Find(p => p.PlayerModelId == uniquePlayerId));
                }
                gamblingEvent.ParticipatingPlayers = playersInEvent;
            }
            return gamblingEvents;
        }

        public async Task<int> SaveChangesToGame(GameModel gameModelToUpdate)
        {
            using GambleMaticContext gambleMaticContext = new GambleMaticContext();
            gambleMaticContext.Games.Update(gameModelToUpdate);
            int changesCount = await gambleMaticContext.SaveChangesAsync();
            return changesCount;
        }

        public async Task<int> SaveChangesToGames(List<GameModel> gamesToUpdate)
        {
            using GambleMaticContext gambleMaticContext = new GambleMaticContext();
            gambleMaticContext.Games.UpdateRange(gamesToUpdate);
            int changesCount = await gambleMaticContext.SaveChangesAsync();
            return changesCount;
        }

        public async Task<int> SaveChangesToPlayer(PlayerModel playerModelUnderEdit)
        {
            using GambleMaticContext gambleMaticContext = new GambleMaticContext();
            gambleMaticContext.Players.Update(playerModelUnderEdit);
            int changesCount = await gambleMaticContext.SaveChangesAsync();
            return changesCount;
        }

        public async Task<int> SaveExtraGamblesToDatabase(ExtraGamblesModel extraGamblesModel)
        {
            using GambleMaticContext gambleMaticContext = new GambleMaticContext();
            gambleMaticContext.ExtraGambles.Update(extraGamblesModel);
            int savedOrUpdated = await gambleMaticContext.SaveChangesAsync();
            return savedOrUpdated;
        }

        public async Task<int> SaveGambles(List<GambleItemModel> listOfGambles)
        {
            using GambleMaticContext gambleMaticContext = new GambleMaticContext();
            gambleMaticContext.Gambles.AddRange(listOfGambles);
            int result = await gambleMaticContext.SaveChangesAsync();
            return result;
        }

        public async Task<int> SaveGameToDatabase(GameModel gameModel)
        {
            using GambleMaticContext gambleMaticContext = new GambleMaticContext();

            gameModel.GamblingEvent = null;
            
            gambleMaticContext.Games.Add(gameModel);
            int result = await gambleMaticContext.SaveChangesAsync();
            return result;
        }

        public async Task<int> SavePlayerToDatabase(PlayerModel playerModel)
        {
            using GambleMaticContext gambleMaticContext = new GambleMaticContext();
            gambleMaticContext.Players.Add(playerModel);
            int result = await gambleMaticContext.SaveChangesAsync();
            return result;
        }

        public async Task<List<GameModel>> GetAllGamesForGamblingEventFromDatabase(GamblingEvent gamblingEvent)
        {
            using GambleMaticContext gambleMaticContext = new GambleMaticContext();
            var res = await gambleMaticContext.Games.Where(g => g.GamblingEvent.GamblingEventId == gamblingEvent.GamblingEventId).ToListAsync();
            return res;
        }
    }
}
