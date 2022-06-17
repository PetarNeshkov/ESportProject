namespace E_SportManager.Service.Data.Statistics
{
    public interface IStatisticsService
    {
        Task<StatisticsServiceModel> Results(int id);
    }
}
