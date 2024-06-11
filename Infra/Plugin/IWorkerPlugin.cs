using System.Threading.Tasks;

public interface IWorkerPlugin
{
    Task RunBeforeWorker(WorkerContext workerContext);
    Task RunAfterWorker(WorkerContext workerContext);
}