namespace Fragrance_flow_DL_VERSION_.Application.interfaces
{
    public interface IWeatherService
    {
        Task<double?> UserLocationAsync();
    }
}
