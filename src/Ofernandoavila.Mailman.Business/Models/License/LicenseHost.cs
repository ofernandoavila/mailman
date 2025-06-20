namespace Ofernandoavila.Mailman.Business.Models.License
{
    public class LicenseHost : Entity
    {
        public string Url { get; set; }
        public int Port { get; set; }

        public Guid LicenseId { get; set; }
        public License License { get; set; }

        public LicenseHost()
        {

        }

        public LicenseHost(Guid id, string url, int port, Guid licenseId)
        {
            Id = id;
            Url = url;
            Port = port;
            LicenseId = licenseId;
            Activate();
        }
    }
}
