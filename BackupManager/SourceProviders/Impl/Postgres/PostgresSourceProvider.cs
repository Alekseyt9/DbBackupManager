
using System.Diagnostics;

namespace BackupManager
{
    [SourceProvider("postgres")]
    internal class PostgresSourceProvider : SourceProviderBase<PgProviderSettings>
    {
        public override async Task<SourceData> Get(ProviderContext ctx, PgProviderSettings props)
        {
            try
            {
                var info = new ProcessStartInfo
                {
                    FileName = props.PgDumpPath
                };
                var filePath = Path.Combine(props.FilePath,
                    $"{props.DatabaseName}_{DateTime.Now:dd_MM_yy_HH_mm_ss}.psql");

                //pg_dump -Fp -h 172.28.50.25 -p 5433 -U postgres -f "d:\temp\tstpg_20_10_2021.psql" -b tstpg
                info.Arguments = $"-Fp -h {props.Host} -p {props.Port} -U {props.User} -f \"{filePath}\" -b {props.DatabaseName}";

                info.CreateNoWindow = true;
                info.UseShellExecute = false;
                var process = new Process { StartInfo = info };
                process.StartInfo.RedirectStandardError = true;

                process.ErrorDataReceived += (sender, args) =>
                {
                    string errMes = null;
                    if (((Process)sender).ExitCode < 0)
                    {
                        errMes = $"pg_dump.exe exit code: {process.ExitCode}";
                    }

                    if (!string.IsNullOrEmpty(args.Data))
                    {
                        errMes = args.Data;
                    }

                    if (!string.IsNullOrEmpty(errMes))
                    {
                        ctx.Logger.LogError(errMes);
                    }
                };

                try
                {
                    process.StartInfo.EnvironmentVariables["PGPASSWORD"] = props.Password;
                    var res = process.Start();
                    process.BeginErrorReadLine();
                    await process.WaitForExitAsync();

                    //CompressBackup(filePath);
                    //File.Delete(filePath);
                    return new SourceData() { FilePath = filePath };
                }
                catch (Exception exp)
                {
                    ctx.Logger.LogError(exp.Message);
                }
            }
            catch (Exception ex)
            {
                ctx.Logger.LogError(ex.Message);
            }

            return null;
        }

        public override PgProviderSettings CreateProps(IDictionary<string, string> props)
        {
            return new PgProviderSettings(props);
        }

    }
}
