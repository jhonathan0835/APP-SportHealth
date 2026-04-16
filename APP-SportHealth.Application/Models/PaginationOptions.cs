namespace APP_SportHealth.Application.Models
{
    // POCO para opciones de paginación que se enlaza desde appsettings.json
    public class PaginationOptions
    {
        public int MaxPageSize { get; set; } = 10;
    }
}
