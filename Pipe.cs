// Create a new server
using System.Diagnostics;
public class Pipe
{
    private bool isCompiled = false;
    public Pipe(String str)
    {
        WriteFile(str, "Code.c");
        Compile();
        if(isCompiled)
        {
            Run();
        } 
    }
    private void WriteFile(String str, String f)
    {
        StreamWriter sw = new StreamWriter(f);
        sw.WriteLine(str);
        sw.Flush();
        sw.Close();
    }

    private void Compile()
    {

        ProcessStartInfo startInfo = new ProcessStartInfo()
        {
            FileName = "gcc",
            Arguments = "Code.c -o Code",
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            CreateNoWindow = true
        };

        var process = new Process();
        string output, error;

        using (process)
        {
            process.StartInfo = startInfo;
            process.Start();
            process.WaitForExit();
            output = process.StandardOutput.ReadToEnd();
            error = process.StandardError.ReadToEnd();
        }

        if (error != "")
        {
            WriteFile(error, "CompileError.txt");
        }
        else
        {
            WriteFile(output, "CompileOutput.txt");
            isCompiled = true;
        }
    }

    private void Run()
    {
        ProcessStartInfo startInfo = new ProcessStartInfo()
        {
            FileName = (Environment.OSVersion.Platform == PlatformID.Win32NT) ? "Code.exe" : "./Code",
            Arguments = "",
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            CreateNoWindow = true
        };

        var process = new Process();
        string output, error;

        using (process)
        {
            process.StartInfo = startInfo;
            process.Start();
            process.WaitForExit();
            output = process.StandardOutput.ReadToEnd();
            error = process.StandardError.ReadToEnd();
        }

        if (error != "")
        {
            WriteFile(error, "Output.txt");
        }
        else
        {
            WriteFile(output, "Output.txt");
        }
    }

}