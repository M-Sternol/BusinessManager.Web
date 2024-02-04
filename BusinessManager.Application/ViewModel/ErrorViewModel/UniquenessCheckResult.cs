using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManager.Application.ViewModel.ErrorViewModel
{
    public class UniquenessCheckResult
    {
        public bool IsUnique { get; set; }
        public string ConflictField { get; set; }
    }
}
