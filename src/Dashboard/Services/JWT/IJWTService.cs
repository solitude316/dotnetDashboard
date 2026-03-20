namespace Dashboard.Services.JWT;
public interface IJWTService
{
    String Generate(Dictionary<string, object> param);

    bool Validate(String Token);
}