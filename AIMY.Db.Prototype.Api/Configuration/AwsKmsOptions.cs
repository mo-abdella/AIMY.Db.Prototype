namespace AIMY.Db.Prototype.Api.Configuration
{
    public class AwsKmsOptions
    {
        public const string SectionName = "KeyManagement";

        public string KeyId { get; set; } = string.Empty;
        public string? Region { get; set; }
        public string? AccessKey { get; set; }
        public string? SecretKey { get; set; }
    }
}