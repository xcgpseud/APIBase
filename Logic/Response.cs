using System.Collections.Generic;
using Domain.Models.Interfaces;

namespace Logic
{
    public class Response<TModel> where TModel : IModel
    {
        public TModel ResponseObject { get; set; }
        public Dictionary<string, List<string>> Errors { get; set; }
    }
}