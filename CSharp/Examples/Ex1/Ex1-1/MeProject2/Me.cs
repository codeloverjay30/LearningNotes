namespace MeProject2;

public partial class Me
{
    public string Name { get; set; }

    public string Account { get; private set; } = "Account";

    public string Password { get; init; }

    private string _name { get; set; }
    private string _password { get; init; }

    protected string __name { get; set; }
    protected string __account { get; private set; }
    protected string __password { get; init; }

}
