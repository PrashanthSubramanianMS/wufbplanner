using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace wufbdeferplan.Models
{
    public class WindowsRelease
    {
        public int Version { get; set; }
        public List<int> builds { get; set; }
        public string Channel { get; set; }
        public DateTime BornOfRelese { get; set; }
        public DateTime EndServiceDate { get; set; }
        public DateTime EndSupportDate { get; set; }
    }
}
