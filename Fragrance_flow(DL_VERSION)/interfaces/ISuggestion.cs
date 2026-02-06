using Fragrance_flow_DL_VERSION_.models;

namespace Fragrance_flow_DL_VERSION_.interfaces
{
    public interface ISuggestion
    {
        public Task<IEnumerable<Fragrance>> ScentOfTheDay(double? temp, int id);
        public Task<Fragrance> SuggestBasedOnFeeling(string feeling, int id);
        public Task<string> FragranceForWeather(double temp);

    }
}
