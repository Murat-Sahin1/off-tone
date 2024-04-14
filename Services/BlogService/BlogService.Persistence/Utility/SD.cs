namespace BlogService.Persistence.Utility;

public static class SD
{
    public static bool IsProd = true;
    public enum CurrentDB
    {
        PostgreSQL,
        InMemory
    }
}