using System;
using System.Collections.Generic;
using System.Text;

namespace WorkflowConfigurationService.Domain
{
    public abstract class ArgumentValidationAttribute : Attribute
    {
        public abstract void Validate(object value, string argumentName);
    }

    [AttributeUsage(AttributeTargets.Parameter)]
    public class NotNullAttribite: ArgumentValidationAttribute
    {
        public override void Validate(object value, string argumentName)
        {
            if(value is null)
            {
                throw new ArgumentNullException(argumentName);
            }
        }
    }
}
