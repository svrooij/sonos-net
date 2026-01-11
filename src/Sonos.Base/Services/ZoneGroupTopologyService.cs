/*
 * Sonos-net
 *
 * Repository https://github.com/svrooij/sonos-net
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;

using Sonos.Base.Soap;

namespace Sonos.Base.Services;

public partial class ZoneGroupTopologyService
{
    public static ZoneGroupState? ParseState(string? zoneGroupXml)
    {
        if (zoneGroupXml is null || string.IsNullOrEmpty(zoneGroupXml))
        {
            return null;
        }
        return SoapFactory.ParseEmbeddedXml<ZoneGroupState>(zoneGroupXml);
    }

    // This key was extracted from the Sonos app.
    private const string THIRD_PARTY_MEDIA_SERVERS_KEY = "1a01a731c96e9ebde8475182b274b70e";
    
    /// <summary>
    /// Decrypts the specified encrypted media server data for a given household identifier.
    /// </summary>
    /// <remarks>The method returns null if either parameter is null or empty, or if the encrypted data does
    /// not begin with "2:". The decrypted data is expected to be an XML string. If decryption fails due to invalid
    /// input or cryptographic errors, an exception is thrown.</remarks>
    /// <param name="householdId">The household identifier used to derive the decryption key. Use value from <see cref="IZoneGroupTopologyEvent.MuseHouseholdId"/></param>
    /// <param name="encryptedData">The encrypted media server data to decrypt. Must be a non-null, non-empty string that begins with "2:", use value from <see cref="IZoneGroupTopologyEvent.ThirdPartyMediaServersX"/></param>
    /// <returns>A string containing the decrypted media server data if decryption succeeds; otherwise, null.</returns>
    public static string? DecryptMediaServers(in string? householdId, in string? encryptedData)
    {
        if (string.IsNullOrEmpty(encryptedData) || !encryptedData.StartsWith("2:") || string.IsNullOrEmpty(householdId))
        {
            return null;
        }

        var fixedHouseholdId = householdId.Contains(".") ? householdId.Split('.')[0] : householdId;

        try
        {

            var rawValue = Convert.FromBase64String(encryptedData.Substring(2));
            var iv = rawValue[..16]; // First 16 bytes is the IV
            var cipherText = rawValue[16..]; // The rest is the cipher text

            // Convert the hex key string to bytes
            var keyBytes = Convert.FromHexString(THIRD_PARTY_MEDIA_SERVERS_KEY);
            
            // Calculate intermediate key: MD5(householdId + THIRD_PARTY_MEDIA_SERVERS_KEY)
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

            // Decrypt using AES in CBC mode
            using (var aes = Aes.Create())
            {
                aes.Key = finalKey;
                aes.IV = iv;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                using (var decryptor = aes.CreateDecryptor())
                {
                    var decryptedBytes = decryptor.TransformFinalBlock(cipherText, 0, cipherText.Length);
                    
                    // Convert to string
                    var result = Encoding.UTF8.GetString(decryptedBytes);
                    
                    // Find the actual end of the XML content by looking for the last '>'
                    var lastBracketIndex = result.LastIndexOf('>');
                    if (lastBracketIndex >= 0 && lastBracketIndex < result.Length - 1)
                    {
                        result = result.Substring(0, lastBracketIndex + 1);
                    }

                    return result;
                }
            }
        }
        catch
        {
            throw;
        }
    }

    public static ZoneGroup.MediaServers? DecryptAndParseMediaServers(in string? householdId, in string? encryptedData)
    {
        var decryptedXml = DecryptMediaServers(householdId, encryptedData);
        if (decryptedXml is null)
        {
            return null;
        }
        return SoapFactory.ParseEmbeddedXml<ZoneGroup.MediaServers>(decryptedXml);
    }

    public partial class GetZoneGroupStateResponse
    {
        private ZoneGroupState? _zoneGroupState;

        [XmlIgnore]
        public ZoneGroupState? ParsedState
        {
            get
            {
                if (_zoneGroupState == null)
                {
                    _zoneGroupState = ParseState(ZoneGroupState);
                }
                return _zoneGroupState;
            }
        }
    }

    public partial interface IZoneGroupTopologyEvent
    {
        public ZoneGroupState? ParsedState { get; }
    }
}