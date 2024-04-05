namespace BlogService.Persistence.Utility;

public static class SD
{
    public static bool IsProd = false;
    public enum CurrentDB
    {
        PostgreSQL,
        InMemory
    }
}