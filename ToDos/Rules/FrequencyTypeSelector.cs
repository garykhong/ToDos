using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ToDos.Rules
{
    public class FrequencyTypeSelector
    {
        private List<string> frequencyTypes = Enum.GetNames(typeof(FrequencyType)).ToList();
        public IEnumerable<SelectListItem> GetFrequencyTypes()
        {
            return new SelectList(frequencyTypes);
        }
    }
}
