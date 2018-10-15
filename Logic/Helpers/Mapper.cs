using System;
using System.Linq;

namespace Logic.Helpers
{
    public static class Mapper
    {
        public static TOutput 
            Map<TInput, TOutput>(TInput inputObject, object overrides = null)
            where TOutput : class
        {
            var inputProps = inputObject.GetType().GetProperties();
            var outputProps = typeof(TOutput).GetProperties();
            var overrideProps = overrides?.GetType().GetProperties();

            var response = Activator.CreateInstance(typeof(TOutput));

            foreach (var prop in inputProps)
            {
                if (outputProps.Any(p => p.Name == prop.Name))
                {
                    object value;
                    
                    if (overrides != null && overrideProps.FirstOrDefault(p => p.Name == prop.Name) != null)
                    {
                        value = overrides.GetType().GetProperty(prop.Name)
                            .GetValue(overrides);
                    }
                    else
                    {
                        value = prop.GetValue(inputObject);
                    }
                    
                    response.GetType()
                        .GetProperty(prop.Name)
                        ?.SetValue(response, value, null);
                }
            }

            return (TOutput) response;
        }
    }
}