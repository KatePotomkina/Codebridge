namespace Codebridge.Service;

public class PingService:IPingService
{
    public string Ping()
    {
       return "Dogshouseservice.Version1.0.1";
    } 
}