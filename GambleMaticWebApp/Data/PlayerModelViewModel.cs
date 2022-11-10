using System.Text;

public class PlayerModelViewModel
{
    public PlayerModelViewModel()
    {
        _playerModel = new();
    }

    public PlayerModelViewModel(PlayerModel playerModel)
    {
        _playerModel = playerModel;
    }

    public PlayerModelViewModel(string name, string shortName, string? email = null)
    {
        if (_playerModel == null)
        {
            _playerModel = new PlayerModel(name, shortName, email);
        }
        else
        {

            _playerModel.Name = name;
            _playerModel.ShortName = shortName;
            _playerModel.Email = email;
        }
    }


    private PlayerModel _playerModel = null;

    public string Name { get { return _playerModel.Name; } set { _playerModel.Name = value; } }
    public string ShortName { get { return _playerModel.ShortName; } set { _playerModel.ShortName = value; } }
    public string Email
    {
        get
        {
            if (string.IsNullOrEmpty(_playerModel.Email))
            {
                return "";
            }
            return _playerModel.Email;
        }
        set { _playerModel.Email = value; }
    }

    public int Paid 
    {
        get
        {
            return _playerModel.Paid;
        }
        set
        {
            _playerModel.Paid = value;
        }
    }
    public string HasPaidStr
    {
        get
        {
            if (_playerModel.Paid == 0)
            {
                return "No";
            }
            return "Yes";
        }
    }

    public int Ticket
    { 
        get 
        {
            return _playerModel.Ticket;
        } 

        set
        {
            _playerModel.Ticket = value;
        }
    }

    public string TicketDelivered
    {
        get
        {
            if (Ticket == 0)
            {
                return "No";
            }
            return "Yes";
        }
    }

    public string IsGambleRowStored { 
        get 
        {
            var gambleRow = GetPlayerModel().GambleItemModels;
            if (gambleRow == null || gambleRow.Count == 0)
            {
                return "No";
            }
            return "Yes";
        }
    }


    public override string ToString()
    {
        StringBuilder sb = new StringBuilder("Name: [");

        sb.Append(Name + "] ShortHand: [");
        sb.Append(ShortName + "] Email: [");
        sb.Append(Email + "] ");
        return sb.ToString();
    }

    internal PlayerModel GetPlayerModel()
    {
        return _playerModel;
    }
}
