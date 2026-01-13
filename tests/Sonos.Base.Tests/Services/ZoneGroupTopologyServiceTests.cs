using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

using Sonos.Base.Services;

using Xunit;

namespace Sonos.Base.Services.Tests;

public class ZoneGroupTopologyServiceTests
{
    // Change these to test with your own data, but do not commit sensitive data to the repository!
    private const string HouseholdId = "Sonos_YlYmaaaaaaaaaaaaaaaa000000.AAAu2a-0-aAaaaAAaAAa";
    private const string EncryptedData = "";
    
    // Leave this value unchanged
    private const string FakeHouseholdId = "Sonos_NotMyActualHouseholdId0000.xxx";

    // Replace this with the output from EncryptData_for_testing_purposes test, if you want to test additional scenarios. I would suggest to only add Service entries to the data below, because the test will validate the actual values.
    private const string FakeEncryptedData = "2:S+T0/s4gLJa+n3yvzT2wHGiXQJ2yowVpHOfsIvFilcwF1IkXxMd9qCJ+8Yl5WwK0Kmb0HyWYE+d3wwFhVLcVa1y6N8EJ65rjY00Rc1UcDRaHiH2kMUSTI8P2d3OjifqjPiDLfE2MoDDTJZ/rp+oj7hvzUezxv0+ilkV7X7XCPPbNuvTsempHS/3ccLu3cysaZP+Q/2/3xA2DM36ousKnEXcPyAdnBXoK3MZKHcFBfrYhms8x4z4h0Dv8cK4zlW7Z4TP8f9v0XO0UOB7W4daArUQ7MRsFqMF8IBTW5TrCx9aZm2lASltzYCH7Y4uIJPakkt7fVgb2A+A6AznVMS0i78lRfQexXmu9lnU5TdtkUulVS03LxahvCaqA0rAmbgm1BANWynq2AKC2OJy3PDfzAhHvEOluc4Vvk1mU0bON1Msh2lJaQqdHDfwuwadLG92JHqb2srwdSJbFOMQJEdIYVdbZofMcquocNJBmNsLtYh69EKHKLnlUKKRjdEYOylpx/uGNyl/dRyaJ26lOk+vG8TRoFu4R8xz22mM6+8WKIkLBrHVLJSyR8/OBlyr84XO9mYTpdLxwK6zN1oF7fAYHkapPda398xjCvZUXMzhX9X7tzxx/GEcgjmV39b3utjbU6dgkMuv1JUJl9OLl1kct+KvrW621TElCEEzySAqYefz4oFaX3o2MMOXBfddUYCwUyl+GMAXK9SuJfaCmzMrwXA==";

    // This fake xml can be your actual values with sensitive data replaced, if you debug EncryptData_for_testing_purposes it will generate valid encrypted data from this xml.
    private const string FakeMediaServersData = @"<MediaServers>
<Service UDN=""SA_RINCON77575_X_#Svc77575-0-Token"" NumAccounts=""1"" Md0="""" Username0=""X_#Svc77575-0-Token"" Nickname0="""" SerialNum0=""1"" Flags0=""0"" Tier0=""1"" Token0=""09052d5da318f24d"" Key0=""ffdd1236fab""/>
<Service UDN=""SA_RINCON2311_X_#Svc2311-0-Token"" NumAccounts=""1"" Md0="""" Username0=""X_#Svc2311-0-Token"" Nickname0=""stephan"" SerialNum0=""2"" Flags0=""0"" Tier0=""3"" Token0=""BQCdOwijDMbLtFlZt7N......"" Key0=""stephan/NL/AQCjS7fSQ91L7sgGZfWlrM..._wyU2CwaKk...eob_nRW...4o/1768000000000""/>
</MediaServers>";

    [Fact]
    public void DecryptMediaServers_ActualData_ReturnsDecryptedXml()
    {
        // Skip test if no actual data provided
        if (string.IsNullOrWhiteSpace(HouseholdId) || string.IsNullOrEmpty(EncryptedData))
        {
            return;
        }
        // Act
        var result = ZoneGroupTopologyService.DecryptMediaServers(HouseholdId, EncryptedData);
        // Assert
        Assert.NotNull(result);
        Assert.StartsWith("<MediaServers>", result);
        Assert.EndsWith("</MediaServers>", result);
    }

    [Fact]
    public void DecryptAndParseMediaServers_ActualData_ReturnsParsedMediaServers()
    {
        // Skip test if no actual data provided
        if (string.IsNullOrWhiteSpace(HouseholdId) || string.IsNullOrEmpty(EncryptedData))
        {
            return;
        }
        // Act
        var result = ZoneGroupTopologyService.DecryptAndParseMediaServers(HouseholdId, EncryptedData);
        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result.Servers);
        Assert.All(result.Servers, ms =>
        {
            Assert.False(string.IsNullOrEmpty(ms.UDN));
            Assert.InRange(ms.ServiceId, 1, 500);
        });
    }

    [Fact]
    public void DecryptAndParseMediaServers_FakeData_ReturnsParsedMediaServers()
    {
        // Act
        var result = ZoneGroupTopologyService.DecryptAndParseMediaServers(FakeHouseholdId, FakeEncryptedData);
        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result.Servers);
        Assert.All(result.Servers, ms =>
        {
            Assert.False(string.IsNullOrEmpty(ms.UDN));
            Assert.InRange(ms.ServiceId, 1, 500);
        });

        var service1 = result.Servers[0];
        Assert.Equal("SA_RINCON77575_X_#Svc77575-0-Token", service1.UDN);
        Assert.Equal(1, service1.NumAccounts);
        Assert.Equal("X_#Svc77575-0-Token", service1.Username);
        // Validate the ServiceId property is calculated correctly
        Assert.Equal(303, service1.ServiceId);
    }


    /// <summary>
    /// Tests the encryption of sample media server data for use in other test scenarios.
    /// </summary>
    /// <remarks>This test verifies that the encrypted data produced by EncryptMediaServers can be
    /// successfully decrypted using ZoneGroupTopologyService.DecryptMediaServers. The resulting encrypted data can be
    /// used as input for other tests that require pre-encrypted values.</remarks>
    [Fact]
    public void EncryptData_for_testing_purposes()
    {
        // This test encrypts fake data that can be used in other tests
        // It mirrors the decryption logic from ZoneGroupTopologyService.DecryptMediaServers
               
        var encryptedData = EncryptMediaServers(FakeHouseholdId, FakeMediaServersData);
        
        // Verify the encrypted data can be decrypted back
        var decryptedData = ZoneGroupTopologyService.DecryptMediaServers(FakeHouseholdId, encryptedData);
        
        Assert.Equal(FakeMediaServersData, decryptedData);
        
        // Output the encrypted data for use in tests
        // You can view this in the test output
        Assert.NotNull(encryptedData);
    }

    // Encryption logic to match the decryption in ZoneGroupTopologyService
    private static string EncryptMediaServers(string householdId, string xmlData)
    {
        const string THIRD_PARTY_MEDIA_SERVERS_KEY = "1a01a731c96e9ebde8475182b274b70e";
        
        var fixedHouseholdId = householdId.Contains(".") ? householdId.Split('.')[0] : householdId;
        
        // Generate random IV (16 bytes for AES)
        var iv = new byte[16];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(iv);
        }
        
        // Convert the hex key string to bytes
        var keyBytes = Convert.FromHexString(THIRD_PARTY_MEDIA_SERVERS_KEY);

        var householdIdBytes = Encoding.UTF8.GetBytes(fixedHouseholdId);
        var intermediateKeyInput = householdIdBytes.Concat(keyBytes).ToArray();
        byte[] intermediateKey;
        using (var md5 = MD5.Create())
        {
            intermediateKey = md5.ComputeHash(intermediateKeyInput);
        }
        
        // Calculate final key: MD5(iv + intermediate_key)
        var finalKeyInput = iv.Concat(intermediateKey).ToArray();
        byte[] finalKey;
        using (var md5 = MD5.Create())
        {
            finalKey = md5.ComputeHash(finalKeyInput);
        }
        
        // Encrypt using AES in CBC mode
        using (var aes = Aes.Create())
        {
            aes.Key = finalKey;
            aes.IV = iv;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            
            using (var encryptor = aes.CreateEncryptor())
            {
                var xmlBytes = Encoding.UTF8.GetBytes(xmlData);
                var cipherText = encryptor.TransformFinalBlock(xmlBytes, 0, xmlBytes.Length);
                
                // Combine IV and ciphertext
                var combined = iv.Concat(cipherText).ToArray();
                
                // Convert to base64 and prepend "2:"
                return "2:" + Convert.ToBase64String(combined);
            }
        }
    }
}