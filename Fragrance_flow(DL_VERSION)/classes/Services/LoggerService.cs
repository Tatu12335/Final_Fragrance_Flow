using Fragrance_flow_DL_VERSION_.interfaces;
using Microsoft.Identity.Client;
using System.Collections.Concurrent;

namespace Fragrance_flow_DL_VERSION_.classes.Services
{
    // Note to me : Remember to addsingleton not, addtransient.
    // Also add username to executeadmincommand(), in clirepo, so i can see who made a change.
    public class LoggerService : ILoggger
    {
        private readonly BlockingCollection<string> _logsQue = new BlockingCollection<string>();
        private readonly Task _worker;
        private readonly string _directory;
        private readonly string _filepath;
        private bool _disposed;

        public LoggerService()
        {
            string localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            _directory = Path.Combine(localAppData, "Fragrance_flow", "LOGS");
            _filepath = Path.Combine(_directory, "log.txt");

            Directory.CreateDirectory(_directory);

            _worker = Task.Factory.StartNew(ProcessQueue, TaskCreationOptions.LongRunning);
        }
        public void ProcessQueue()
        {
            try
            {
                
                const int maxAttempts = 10;
                int attempt = 0;
                FileStream fs = null;
                while (true)
                {
                    try
                    { 
                        fs = new FileStream(_filepath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite);
                        fs.Seek(0, SeekOrigin.End);
                        break;
                    }
                    catch (IOException)
                    {
                        attempt++;
                        if (attempt >= maxAttempts)
                        {
                            throw; 
                        }
                        Thread.Sleep(200);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(" An error occured : " + ex.Message);
                    }
                }


                using var writer = new StreamWriter(fs) { AutoFlush = true };

                foreach (var msg in _logsQue.GetConsumingEnumerable())
                {
                    try
                    {
                        writer.WriteLine($" {DateTime.Now} - {msg}");
                    }
                    catch (IOException iex)
                    {
                        throw new Exception(" IOEXCEPTION occured : " +  iex.Message);
                       
                    }
                    catch (UnauthorizedAccessException uaex)
                    {
                        throw new Exception(" Unauthorized Exception occurred : " + uaex.Message);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(" An error occured 2 : " + ex.Message);
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(" An error occured 3 : " +  e.Message);
            }
        }
        public void Log(string logMessage)
        {
            if (logMessage is null)
            {
                throw new ArgumentNullException(nameof(logMessage));
            }
            if (_logsQue.IsAddingCompleted)
            {
                return;
            }

            try
            {
                _logsQue.Add(logMessage);
            }
            catch (InvalidOperationException ioex)
            {
                throw new Exception(" An error occured : " + ioex.Message);
            }
        }
        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }
            _disposed = true;

            _logsQue.CompleteAdding(); 
            try
            {
                _worker.Wait(5000); 
            }
            catch (AggregateException aex)
            {
                throw new Exception(" An aggregateException occured : " + aex.Message);
            }
            catch (Exception e)
            {
                throw new Exception(" An error occured : " + e.Message);
            }

            _logsQue.Dispose();
        }

    }
}
