using System.Text;

internal class CountryCodeTranlator
{


    public CountryCodeTranlator()
    {
        countryCoreTranslations = new();
        string[] countryCodesData = File.ReadAllLines("countryCodes.json");

        StringBuilder sb = new();
        foreach (string c in countryCodesData)
        {
            sb.Append(c);
        }

        var countryCodes = System.Text.Json.JsonSerializer.Deserialize<CountryCoreTranslation[]>(sb.ToString());



        //string countryCodesData = File.ReadAllText("countryCodes.json");
        //var countryCodes = System.Text.Json.JsonSerializer.Deserialize<CountryCoreTranslation[]>(countryCodesData);
        countryCoreTranslations.AddRange(countryCodes);
    }


    List<CountryCoreTranslation> countryCoreTranslations;


    public string? TranslateNameToCode(string name)
    {
        foreach (var cct in countryCoreTranslations)
        {
            if (cct.name.ToLower() == name.ToLower())
            {
                return cct.code.ToLower();
            }
        }
        return null;
    }   


    public string? TranslateCodeToName(string code)
    {
        foreach (var cct in countryCoreTranslations)
        {
            if (cct.code.ToLower() == code.ToLower())
            {
                return cct.name;
            }
        }
        return null;
    }
}

public class CountryCoreTranslation {

    public string code { get; set; }
    public string name { get; set; }
}