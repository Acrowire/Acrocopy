using Acrocopy.Models;
using CommandLine;
using CommandLine.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acrocopy
{
    public class Program
    {
        static void Main(string[] args)
        {
            var options = new CommandLineOptions();
            var parser = new Parser();

            if(parser.ParseArguments(args, options))
            {
                var results = DirectorySearch(options);

                foreach(var i in results) {
                    var destination = Path.Combine(options.DestinationDirectory, i.Name);
                    if(options.Verbose)
                        Console.WriteLine(string.Format("{0} => {1}", i.FullName, destination));
                    File.Copy(i.FullName, destination);
                }
            }

            Console.WriteLine("Completed...");
            Console.ReadLine();
        }

        /// <summary>
        /// Basic Directory SEarch utilizing the TimeSpan on Last Modify Date
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        private static List<FileInfo> DirectorySearch(CommandLineOptions options)
        {
            var dinfo = new DirectoryInfo(options.SourceDirectory);
            var finfo = dinfo.GetFiles("*.*").ToList();
            
            var results = finfo.Where(f => (DateTime.Now - f.LastWriteTime) <= ResolveTimeSpan(options));

            return results.ToList();
        }

        /// <summary>
        /// It is Observed that right now the command line for time difference can only handle simple
        /// strings like "1d", or "3h" - future versions should support "1d3h20m", but we aren't there yet
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        private static TimeSpan ResolveTimeSpan(CommandLineOptions options)
        {
            int d = 0, h = 0, m = 0, s = 0;
            if (options.CreatedWithin.Contains("d"))
                int.TryParse(options.CreatedWithin.Replace("d", ""), out d);

            if (options.CreatedWithin.Contains("h"))
                int.TryParse(options.CreatedWithin.Replace("h", ""), out h);

            if (options.CreatedWithin.Contains("m"))
                int.TryParse(options.CreatedWithin.Replace("m", ""), out m);

            if (options.CreatedWithin.Contains("s"))
                int.TryParse(options.CreatedWithin.Replace("s", ""), out s);

            return new TimeSpan(d, h, m, s);
        }

    }

    
}
