using Microsoft.AspNetCore.Authorization;

namespace netBackend.Utils
{
    public class Idenfication : IAuthorizationRequirement
    {
        public string idenfication { get; }


        public Idenfication(string idenfication)
        {
            this.idenfication = idenfication;
        }
    }
}