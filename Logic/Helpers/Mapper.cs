using System;
using System.Linq;

namespace Logic.Helpers
{
    public static class Mapper
    {
        public static TOutput 
            Map<TInput, TOutput>(TInput inputObject)
            where TOutput : class
        {
            var inputProps = inputObject.GetType().GetProperties();
            var outputProps = typeof(TOutput).GetProperties();

            var response = Activator.CreateInstance(typeof(TOutput));

            foreach (var prop in inputProps)
            {
                if (outputProps.Any(p => p.Name == prop.Name))
                {
                    response.GetType()
                        .GetProperty(prop.Name)
                        ?.SetValue(response, prop.GetValue(inputObject), null);
                }
            }

            return (TOutput) response;
        }
    }
}