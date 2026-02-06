namespace Fragrance_flow_DL_VERSION_.interfaces
{
    public interface ILoggger
    {
        public void Dispose();
        public void ProcessQueue();
        public void Log(string logMessage);
    }
}
