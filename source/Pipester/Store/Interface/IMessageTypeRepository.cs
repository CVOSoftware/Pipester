using System;

namespace Pipester.Store.Interface
{
    internal interface IMessageTypeRepository
    {
        Type GetMessageTypeByName(string name);
    }
}