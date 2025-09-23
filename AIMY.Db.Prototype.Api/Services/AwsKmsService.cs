using Amazon.KeyManagementService;
using Amazon.KeyManagementService.Model;
using AIMY.Db.Prototype.Api.Configuration;
using Microsoft.Extensions.Options;
using System.Text;

namespace AIMY.Db.Prototype.Api.Services
{
    public interface IAwsKmsService
    {
        Task<byte[]> EncryptAsync(string plaintext);
        Task<string> DecryptAsync(byte[] ciphertext);
    }

    public class AwsKmsService : IAwsKmsService
    {
        private readonly AwsKmsOptions _kmsOptions;
        private readonly IAmazonKeyManagementService _kmsClient;

        public AwsKmsService(IOptions<AwsKmsOptions> kmsOptions)
        {
            _kmsOptions = kmsOptions.Value;
            
            // Create KMS client with optional custom configuration
            var config = new AmazonKeyManagementServiceConfig();
            
            if (!string.IsNullOrEmpty(_kmsOptions.Region))
            {
                config.RegionEndpoint = Amazon.RegionEndpoint.GetBySystemName(_kmsOptions.Region);
            }

            _kmsClient = new AmazonKeyManagementServiceClient(config);
        }

        public async Task<byte[]> EncryptAsync(string plaintext)
        {
            if (string.IsNullOrEmpty(plaintext))
                throw new ArgumentNullException(nameof(plaintext));

            if (string.IsNullOrEmpty(_kmsOptions.KeyId))
                throw new InvalidOperationException("KMS KeyId is not configured");

            var encryptRequest = new EncryptRequest
            {
                KeyId = _kmsOptions.KeyId,
                Plaintext = new MemoryStream(Encoding.UTF8.GetBytes(plaintext))
            };

            var response = await _kmsClient.EncryptAsync(encryptRequest);

            using var ms = new MemoryStream();
            response.CiphertextBlob.CopyTo(ms);
            return ms.ToArray();
        }

        public async Task<string> DecryptAsync(byte[] ciphertext)
        {
            if (ciphertext == null || ciphertext.Length == 0)
                throw new ArgumentNullException(nameof(ciphertext));

            var decryptRequest = new DecryptRequest
            {
                CiphertextBlob = new MemoryStream(ciphertext)
            };

            var response = await _kmsClient.DecryptAsync(decryptRequest);

            using var reader = new StreamReader(response.Plaintext);
            return await reader.ReadToEndAsync();
        }

        public void Dispose()
        {
            _kmsClient?.Dispose();
        }
    }
}