using APP_SportHealth.Application.Models;

namespace APP_SportHealth.Application.Interfaces
{
    public interface IPaginationOptionsProvider
    {
        PaginationOptions GetOptions();
    }
}
