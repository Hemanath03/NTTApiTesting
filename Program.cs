using NTTApiTesting.Models;
using NTTApiTesting.Services;
using Serilog;

namespace NTTApiTesting
{

    class Program
    {
       public  static async Task Main2(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.Console()
                .WriteTo.File("Logs/test-log-.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            try
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("API Tester");
                Console.ResetColor();
                Console.WriteLine();

                // Initialize Variable Manager
                var variableManager = new VariableManager();

               
                //Console.Write("📍 Enter your API endpoint (or press Enter for default): ");
                //var endpointInput = Console.ReadLine();
                //if (!string.IsNullOrWhiteSpace(endpointInput))
                //{
                //    variableManager.SetVariable("{{endpoint}}", endpointInput.Trim());
                //}

                var configService = new TestConfigurationService();
                var testCases = configService.GetLoginTestCase();

                Console.WriteLine($"\n Loaded {testCases.Count} test cases from configuration\n");
 
                
                Console.WriteLine("Available Tests:");
                Console.WriteLine(new string('-', 70));
                for (int i = 1; i < testCases.Count; i++)
                {
                    var hasExtraction = testCases[i].ExtractToken != null ? "key" : "";
                    Console.WriteLine($"  {i + 1}. {testCases[i].Name} [{testCases[i].Method}]{hasExtraction}");
                }
                Console.WriteLine(new string('-', 70));
                Console.WriteLine("   Extracts token/variable from response");

                Console.Write("\n How many tests to run? (Enter number or 'all'): ");
                var input = Console.ReadLine();

                int testCount = input?.ToLower() == "all" ? testCases.Count :
                               int.TryParse(input, out int count) ? Math.Min(count, testCases.Count) : testCases.Count;

                var testsToRun = testCases.GetRange(0, testCount);

                Console.WriteLine($"\n Starting execution of {testsToRun.Count} tests...\n");
                Console.WriteLine(new string('═', 100));

                var apiTestService = new ApiTestService(variableManager);
                var reportGenerator = new ReportGenerator();
                var results = new List<TestResult>();

                var otpCase = testCases.First();
                Console.WriteLine("\nSending OTP request...");
                var otpResult = await apiTestService.ExecuteTestAsync(otpCase);

                if (!otpResult.IsPassed)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Failed to send OTP. Stopping execution.");
                    Console.ResetColor();
                    return;
                }

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nOTP sent successfully to registered mobile.");
                Console.ResetColor();

                Console.Write("Enter OTP: ");
                string otp = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(otp))
                {
                    Console.WriteLine("OTP cannot be empty. Exiting.");
                    return;
                }

                variableManager.SetVariable("{{otp}}", otp);

                for (int i = 1; i < testsToRun.Count; i++)
                {
                    var testCase = testsToRun[i];

                    Console.Write($"[{i + 1}/{testsToRun.Count}] Testing: {testCase.Name,-45} ");

                    var result = await apiTestService.ExecuteTestAsync(testCase);
                    results.Add(result);

                    if (result.IsPassed)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($" PASS ({result.ResponseTimeMs}ms)");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($" FAIL ({result.StatusCode}) - {result.ResponseTimeMs}ms");
                    }
                    Console.ResetColor();

                    Log.Information($"Test: {result.TestName} | Status: {(result.IsPassed ? "PASS" : "FAIL")} | Code: {result.StatusCode} | Time: {result.ResponseTimeMs}ms");

                    await Task.Delay(500);
                }

                Console.WriteLine(new string('═', 100));

                // Display extracted variables
                Console.Write("\n Show extracted variables? (y/n): ");
                if (Console.ReadLine()?.ToLower() == "y")
                {
                    variableManager.DisplayVariables();
                }

                // Generate reports
                Console.WriteLine(" Generating reports...");

                var reportFolder = "Reports";
                Directory.CreateDirectory(reportFolder);

                var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                var htmlReportPath = Path.Combine(reportFolder, $"TestReport_{timestamp}.html");
                var csvReportPath = Path.Combine(reportFolder, $"TestReport_{timestamp}.csv");

                reportGenerator.GenerateHtmlReport(results, htmlReportPath);
                reportGenerator.GenerateCsvReport(results, csvReportPath);
                reportGenerator.GenerateConsoleReport(results);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($" HTML Report saved: {Path.GetFullPath(htmlReportPath)}");
                Console.WriteLine($" CSV Report saved:  {Path.GetFullPath(csvReportPath)}");
                Console.ResetColor();

                Console.Write("\n Open HTML report in browser? (y/n): ");
                if (Console.ReadLine()?.ToLower() == "y")
                {
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = htmlReportPath,
                        UseShellExecute = true
                    });
                }

                Console.Write(" Open CSV report? (y/n): ");
                if (Console.ReadLine()?.ToLower() == "y")
                {
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = csvReportPath,
                        UseShellExecute = true
                    });
                }

                Log.Information($"Test execution completed. Total: {results.Count}, Passed: {results.Count(r => r.IsPassed)}, Failed: {results.Count(r => !r.IsPassed)}");
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application terminated unexpectedly");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n Fatal Error: {ex.Message}");
                Console.ResetColor();
            }
            finally
            {
                Log.CloseAndFlush();
            }

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}

