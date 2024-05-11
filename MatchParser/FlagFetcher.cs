using System.Net;
using System.Runtime.CompilerServices;

public class FlagFetcher
{



    private readonly string pre = "https://en.wikipedia.org/wiki/Flag_of_";
    private readonly string post = "¤¤¤#/media/File:Flag_of_¤¤¤.svg";

    public async Task GetFlagForCountry(Country country)
    {

        CountryCodeTranlator cct = new();
        var cc = cct.TranslateNameToCode(country.Name);

        if (cc == null)
        {
            await Console.Out.WriteLineAsync($"No country code for {country.Name}");
            return;
        }
        string? flagFileName = cc + ".svg";
        var flagData = File.ReadAllBytes("./svg/" + flagFileName);
        File.WriteAllBytes(country.Flag, flagData);
        //WebClient client = new();
        //string flagUrl = getFlagUrl(country.name);
        //Console.Write($"Trying to download flag from {flagUrl}...");
        //await client.DownloadFileTaskAsync(flagUrl, country.Flag);
        ////       byte[] svgData = client.DownloadData(flagUrl);
        //Console.WriteLine($"Done readong");// {svgData.Length} bytes.");
        //return null;
    }

    private string getFlagUrl(string countryName)
    {
        string url = pre + post.Replace("¤¤¤", countryName);
        return url;
    }

    public void SaveFlagForCountry(Country country)
    {
        //byte[] flag =  ;
        //GetFlagForCountry(country);
        //string path = country.Flag;
        //System.IO.File.WriteAllBytes(path, flag);
    }




}