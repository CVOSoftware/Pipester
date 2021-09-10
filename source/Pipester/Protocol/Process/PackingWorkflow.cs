using Pipester.Protocol.Message;
using Pipester.Protocol.Process.Steps;
using Pipester.Protocol.Process.Steps.Interface;
using Pipester.Protocol.Setting;

namespace Pipester.Protocol.Process
{
    internal sealed class PackingWorkflow : Workflow<BusinessMessage, byte[]>
    {
        private readonly WorkflowSetting _setting;

        public PackingWorkflow(WorkflowSetting setting)
        {
            _setting = setting;
        }

        protected override IExecutableStep SetFirstStep(BusinessMessage inputValue) => new ToCommunicationMessageStep(inputValue, _setting);
    }
}