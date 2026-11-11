using System.Collections.ObjectModel;

namespace TenForceConsole.Domain.DataTransferObjects.JsonObjects
{
    public class JsonResult<T>
    {
        public Collection<T> Bodies { get; set; }
    }
}
