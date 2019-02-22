using System;
using System.ServiceModel;

namespace IdleMachinery.Extensions.Framework
{
    public static class ICommunicationObjectExtensions
    {
        // TODO - document
        public static void DisposeSafely(this ICommunicationObject communicationObject)
        {
            if (communicationObject.State == CommunicationState.Faulted)
            {
                communicationObject.Abort();
            }
            else
            {
                communicationObject.Close();
            }
            if (communicationObject is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }
    }
}
