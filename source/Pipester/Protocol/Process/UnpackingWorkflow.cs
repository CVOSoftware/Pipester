using Pipester.Protocol.Message;
using Pipester.Protocol.Process.Steps;
using Pipester.Protocol.Process.Steps.Interface;
using Pipester.Protocol.Setting;
using Pipester.Storage.Interface;

namespace Pipester.Protocol.Process
{
    internal sealed class UnpackingWorkflow : Workflow<byte[], BusinessMessage>
    {
        private readonly WorkflowSetting _setting;

        private readonly IHandlerRepository _repository;

        public UnpackingWorkflow(WorkflowSetting setting, IHandlerRepository repository)
        {
            _setting = setting;
            _repository = repository;
        }

        protected override IExecutableStep SetFirstStep(byte[] inputValue) => new UnpackingRawMessageStep(inputValue, _setting, _repository);
    }
}