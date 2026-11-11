using System.Collections.Generic;
using TenForceConsole.Domain.DataTransferObjects;
using TenForceConsole.Domain.Objects;

namespace TenForceConsole.Domain.Services.Interfaces
{
    ///<summary>
    /// A service that can be used to get data from the Solar System OpenData API<see href="https://api.le-systeme-solaire.net/"/>. 
    ///</summary>
    public interface IMoonService
    {
        IEnumerable<Moon> GetAllMoons();
    }
}
