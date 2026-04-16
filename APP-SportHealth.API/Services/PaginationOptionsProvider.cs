using APP_SportHealth.Application.Interfaces;
using APP_SportHealth.Application.Models;
using Microsoft.Extensions.Options;

namespace APP_SportHealth.API.Services
{
    public class PaginationOptionsProvider : IPaginationOptionsProvider
    {
        private readonly IOptionsMonitor<PaginationOptions> _optionsMonitor;

        public PaginationOptionsProvider(IOptionsMonitor<PaginationOptions> optionsMonitor)
        {
            _optionsMonitor = optionsMonitor;
        }

        public PaginationOptions GetOptions() => _optionsMonitor.CurrentValue;
    }
}
