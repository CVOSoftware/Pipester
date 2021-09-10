using Pipester.Protocol.Message;
using Pipester.Protocol.Process.Steps;
using Pipester.Protocol.Process.Steps.Interface;
using Pipester.Protocol.Setting;

namespace Pipester.Protocol.Process
{
    internal sealed class UnpackingWorkflow : Workflow<byte[], BusinessMessage>
    {
        private readonly WorkflowSetting _setting;

        public UnpackingWorkflow(WorkflowSetting setting)
        {
            _setting = setting;
        }

        protected override IExecutableStep SetFirstStep(byte[] inputValue) => new UnpackingRawMessageStep(inputValue, _setting);
    }
}