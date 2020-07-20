using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.ViewModels
{
    public class DivisionVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DeptId { get; set; }
        public string DeptName { get; set; }

        public class DivisionJson
        {
            [JsonProperty("data")]
            public IList<DivisionVM> data { get; set; }
        }
    }
}
