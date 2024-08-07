﻿using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");


FlagFetcher ff = new();

string[] filenames = File.ReadAllLines("group_stage_matches.txt");
List<List<string>> allGameDataBlocks = new();

List<string> daysGameData = new();

for ( int i = 0; i < filenames.Length; i++ ) 
{
    string line = filenames[i];
    System.Console.WriteLine($"Read: [{line}]");
    if ( line.Length == 0 )
    {
        allGameDataBlocks.Add(new List<string>(daysGameData));
        daysGameData = new();
        continue;
    } 
    else
    {
        daysGameData.Add(line);
    }

}

List<Game> allGames = new();
foreach( var dayData in allGameDataBlocks)
{
    KeyValuePair<DateTime, List<Game>> daysGames =  handleDaysGameData(dayData);
    allGames.AddRange(daysGames.Value.ToArray());
}


AllGameDataForExport allGameData = new();
allGameData.AllParticipatingCountries = AllCountriesContainer.CountriesInTournament;
allGameData.AllGames = allGames;
string json = System.Text.Json.JsonSerializer.Serialize(allGameData);

File.WriteAllText( "games.json", json);
File.WriteAllText("games_insert.sql", allGameData.GetAllGamesAsSqlInser());

foreach ( var country in AllCountriesContainer.CountriesInTournament)
{
    //ff.SaveFlagForCountry(country);
    ff.GetFlagForCountry(country).GetAwaiter().GetResult();
}




KeyValuePair<DateTime, List<Game>> handleDaysGameData(List<string> daysGameData)
{

    List<Game> daysGames = new();
    DateTime gameDay = DateTime.Now;
    for ( int i = 0; i < daysGameData.Count; i++)
    {
        if ( i == 0 ) 
        {
            gameDay = parseDate(daysGameData[i]);
            continue;
        }

        Game newGame = new Game(gameDay, daysGameData[i]);
        AllCountriesContainer.AddIfNew(newGame.Home);
        AllCountriesContainer.AddIfNew(newGame.Away);
        System.Console.WriteLine(newGame);
        daysGames.Add(newGame);
    }

    KeyValuePair<DateTime, List<Game>> dayAndGames = new(gameDay, daysGames);


    return dayAndGames;
}

DateTime parseDate(string rawDateString)
{
    
    string[] dateParts = rawDateString.Split(" ");

    DateTime date = new DateTime(2024, 6, int.Parse( dateParts[2]));
    return date;

}

public class Game
{

    public Game(DateTime gameDate, string rawGameDataLine)
    {
        GameDay = gameDate;

        string[] vsSplit = rawGameDataLine.Split(" vs ");
        Home = new Country(vsSplit[0]);

        string[] awaySplit = vsSplit[1].Split(" (");
        Away = new Country(awaySplit[0]);
    }
    public Country Home {get;set;}
    public Country Away {get;set;} 

    public DateTime GameDay { get;set;}

    public override string ToString()
    {

        StringBuilder sb = new("[");
        sb.Append(GameDay.ToShortDateString());
        sb.Append(" : ");
        sb.Append(Home);
        sb.Append(" - ");
        sb.Append(Away);
        return sb.ToString();
    }
}

public class Country
{
    public Country(string countryName)
    {
        Name = countryName;
        Flag = countryName + "_flag.svg";
    }   
    public string Name { get;set;}
    public string Flag { get;set;}

    public override string ToString()
    {
        return "Country=[" + Name +"]";
    }
}

public static class AllCountriesContainer 
{
    public static void AddIfNew(Country country)
    {
        if ( !CountriesInTournament.Contains(country))
        {
            CountriesInTournament.Add(country);
        }
    }

    public static List<Country> CountriesInTournament { get; set; } = new();
}

public class AllGameDataForExport
{
    public AllGameDataForExport()
    {
        
    }
    public AllGameDataForExport(List<Game> games, List<Country> countries)
    {
        AllParticipatingCountries = countries;
        AllGames = games;
    }
    public List<Country> AllParticipatingCountries { get; set; } = new();
    public List<Game> AllGames { get; set; } = new();

    public string GetAllGamesAsSqlInser()
    {
        StringBuilder sb = new();
        string prefix = "INSERT INTO [dbo].[Games] VALUES (convert(datetime,";
        //INSERT INTO [dbo].[Games] VALUES (convert(datetime,'26.11.2022 19:00',104),'France','Denmark',null,63);
        for ( int i = 0; i < AllGames.Count; i++)
        {
            sb.Append(prefix);
            string date = AllGames[i].GameDay.ToString("dd.MM.yyyy HH:mm");
            sb.Append( $"'{date}',104), ");
            sb.Append($" '{AllGames[i].Home.Name}',");
            sb.Append($" '{AllGames[i].Away.Name}',null,{i}, 2);\n");
        }
        return sb.ToString();
    }
}