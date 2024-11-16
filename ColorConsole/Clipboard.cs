using System.Diagnostics;

namespace ColorConsole
{
    public static class Clipboard
    {
        public static void SetText(string input)
        {
            Process clipboardExecutable = new()
            {
                StartInfo = new ProcessStartInfo // Creates the process
                {
                    RedirectStandardInput = true,
                    FileName = @"clip",
                }
            };
            clipboardExecutable.Start();

            clipboardExecutable.StandardInput.Write(input);
            clipboardExecutable.StandardInput.Close();

            return;
        }
    }
}
