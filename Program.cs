using System;
using System.Windows.Forms;
using System.IO; // Required for Path.GetDirectoryName

namespace NovelpiaDownloader
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            // Parse arguments immediately to determine mode
            string novelIdArg = null;
            int? fromChapterArg = null;
            int? toChapterArg = null;
            bool htmlCheckedArg = false;
            string outputPathArg = null; // For single download output
            string listFilePathArg = null; // For batch download list file
            string outputDirectoryArg = null; // For batch download output directory
            bool autoStartDownload = false;
            bool isBatchDownload = false; // New flag for batch mode

            for (int i = 0; i < args.Length; i++)
            {
                switch (args[i].ToLower())
                {
                    case "-novelid":
                        if (i + 1 < args.Length) novelIdArg = args[++i];
                        break;
                    case "-from":
                        if (i + 1 < args.Length && int.TryParse(args[++i], out int fromVal)) fromChapterArg = fromVal;
                        break;
                    case "-to":
                        if (i + 1 < args.Length && int.TryParse(args[++i], out int toVal)) toChapterArg = toVal;
                        break;
                    case "-html":
                        htmlCheckedArg = true;
                        break;
                    case "-output": // Single download output path
                        if (i + 1 < args.Length) outputPathArg = args[++i];
                        break;
                    case "-batch": // New argument for batch mode
                        isBatchDownload = true;
                        break;
                    case "-listfile": // List file path for batch mode
                        if (i + 1 < args.Length) listFilePathArg = args[++i];
                        break;
                    case "-outputdir": // Output directory for batch mode
                        if (i + 1 < args.Length) outputDirectoryArg = args[++i];
                        break;
                    case "-autostart":
                        autoStartDownload = true;
                        break;
                }
            }

            // Determine if running in headless mode
            if (autoStartDownload)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                // Create a MainWin instance. The constructor will load config/auth.
                // We use the default constructor here, as arguments for headless mode
                // are passed directly to the core download methods.
                using (var mainWinInstance = new MainWin())
                {
                    if (isBatchDownload && !string.IsNullOrEmpty(listFilePathArg) && !string.IsNullOrEmpty(outputDirectoryArg))
                    {
                        // Headless batch download
                        Console.WriteLine($"Starting headless batch download from: {listFilePathArg} to {outputDirectoryArg}\r\n");
                        mainWinInstance.BatchDownloadCore(
                            listFilePathArg,
                            outputDirectoryArg,
                            htmlCheckedArg, // Pass HTML checkbox state to batch
                            true // isHeadless = true
                        );
                        Console.WriteLine("Headless batch download completed or encountered an error.\r\n");
                    }
                    else if (!string.IsNullOrEmpty(novelIdArg) && !string.IsNullOrEmpty(outputPathArg))
                    {
                        // Headless single download
                        Console.WriteLine($"Starting headless single download for novel ID: {novelIdArg} to {outputPathArg}\r\n");
                        string outputDirectory = Path.GetDirectoryName(outputPathArg);
                        if (!string.IsNullOrEmpty(outputDirectory) && !Directory.Exists(outputDirectory))
                        {
                            Directory.CreateDirectory(outputDirectory);
                        }

                        bool saveAsEpub = Path.GetExtension(outputPathArg)?.ToLower() == ".epub";
                        mainWinInstance.DownloadCore( // Call the consolidated core download method
                            novelIdArg,
                            saveAsEpub,
                            outputPathArg,
                            fromChapterArg,
                            toChapterArg,
                            htmlCheckedArg,
                            true // isHeadless = true
                        );
                        Console.WriteLine("Headless single download completed or encountered an error.\r\n");
                    }
                    else
                    {
                        Console.WriteLine("Error: Insufficient parameters for headless download. Launching UI.\r\n");
                        Application.Run(new MainWin(args)); // Fallback to UI if headless params are incomplete
                    }
                }
            }
            else
            {
                // If not auto-starting, run the full UI
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainWin(args)); // Pass args to MainWin for UI pre-filling
            }
        }
    }
}
