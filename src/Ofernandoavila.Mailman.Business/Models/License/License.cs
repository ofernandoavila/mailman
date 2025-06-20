using Ofernandoavila.Mailman.Business.Models.AccessControl;

namespace Ofernandoavila.Mailman.Business.Models.License
{
    public class License : Entity
    {
        public string ApplicationName { get; set; }
        public DateTime? ValidUntil { get; set; }
        public string Hosts {  get; set; }
        public DateTime CreatedAt { get; set; }
        public string Key { get; private set; }
        public Guid UserId { get; private set; }

        public User User { get; set; }

        public License()
        {

        }

        public License(Guid id, string applicationNamename, Guid userId, string hosts)
        {
            Id = id;
            ApplicationName = applicationNamename;
            UserId = userId;
            Hosts = hosts;
            Activate();
        }

        public void SetUserId(Guid id)
        {
            UserId = id;
        }
    }
}
