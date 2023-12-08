namespace Sonos.Base.Music
{
    public class MusicClientOptions
    {
        public string BaseUri { get; set; }
        public int ServiceId { get; set; }
        public AuthenticationType AuthenticationType { get; set; }
        public string TimeZone { get; set; } = "+01:00";
        public string? DeviceId { get; set; }
        public string? HouseholdId { get; set; }
        public IMusicClientCredentialStore? CredentialStore { get; set; }
    }
}