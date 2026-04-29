namespace PDP_TestProject_2.Application.Extensions;

public static class FromCentsExtension
{
    public static decimal FromCents(this int cents)
    {
        return cents / 100m;
    }
}
