using CommandLine;
using CommandLine.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acrocopy.Models
{
    /// <summary>
    /// To cover source, destination, verbose and time examples
    /// </summary>
    public class CommandLineOptions
    {
        [Option("t", HelpText = "Use int[type], example 1h, 30m - types include h - hour, m - minute, s - second", DefaultValue = "1h" )]
        public string CreatedWithin { get; set; }
        [Option("s", HelpText = "Source Directory")]
        public string SourceDirectory { get; set; }
        [Option("d", HelpText = "Destination Directory")]
        public string DestinationDirectory { get; set; }
        [Option("v", HelpText = "Print details during execution")]
        public bool Verbose { get; set; }
        [Option("r", HelpText = "Recursive for Sub-Directories", DefaultValue = false)]
        public bool RecursiveSubDirectories { get; set; }
    }
}
