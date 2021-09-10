using Pipester.Protocol.Process.Steps.Interface;

namespace Pipester.Protocol.Process
{
    internal abstract class Workflow<TInput, TOutput> where TOutput : class
    {
        protected abstract IExecutableStep SetFirstStep(TInput inputValue);

        public TOutput Run(TInput inputValue)
        {
            var step = SetFirstStep(inputValue);
            var worker = new Worker<TOutput>(step);

            while (worker.IsWorking)
            {
                worker.Execute();
            }

            var result = worker.GetResult();

            return result;
        }
    }
}