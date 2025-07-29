using Medo;

namespace AIMY.Db.Prototype.Api.Infrastructure;

/// <summary>
/// Utility class for generating UUIDv7-like identifiers
/// </summary>
public static class UUIDv7Generator
{

    public static Guid NewUuid()
    {
        // Generate a UUIDv7-like identifier by using the current time and random data
        return Uuid7.NewUuid7().ToGuid();
    }
}