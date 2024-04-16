using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class RecorderConfigurationModel
    {
        public bool VideoCapture { get; set; }
        public bool CapturePhoto { get; set; }
        public int CaptureFrequency { get; set; }
    }
}
