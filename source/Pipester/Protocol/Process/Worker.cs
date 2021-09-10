using Pipester.Protocol.Process.Steps.Interface;

namespace Pipester.Protocol.Process
{
    internal sealed class Worker<TOutput> where TOutput : class
    {
        private IExecutableStep _executableStep;

        private IEndStep<TOutput> _endStep;

        public Worker(IExecutableStep step)
        {
            IsWorking = true;
            _executableStep = step;
        }

        public bool IsWorking { get; private set; }

        public TOutput GetResult()
        {
            var outputResult = _endStep.End();

            return outputResult;
        }

        public void Execute()
        {
            _executableStep.Execute();

             var nextStep = _executableStep.Next();

            if (nextStep is IEndStep<TOutput> endStep)
            {
                IsWorking = false;
                _endStep = endStep;

                return;
            }

            _executableStep = (IExecutableStep)nextStep;
        }
    }
}