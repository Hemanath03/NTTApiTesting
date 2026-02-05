using NTTApiTesting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTTApiTesting.Services
{
    public class ReportGenerator
    {
        // CSV Report Generation
        public void GenerateCsvReport(List<TestResult> results, string filePath)
        {
            var csv = new StringBuilder();

            // CSV Header
            csv.AppendLine("Test Name,Method,Endpoint,Status Code,Expected Status,Response Time (ms),Result,Status Valid,Body Valid,Error Message,Executed At");

            // CSV Rows
            foreach (var result in results)
            {
                var testName = EscapeCsvField(result.TestName);
                var endpoint = EscapeCsvField(result.Endpoint);
                var errorMessage = EscapeCsvField(result.ErrorMessage ?? "");
                var resultStatus = result.IsPassed ? "PASS" : "FAIL";
                var executedAt = result.ExecutedAt.ToString("yyyy-MM-dd HH:mm:ss");

                csv.AppendLine($"{testName},{result.Method},{endpoint},{result.StatusCode}," +
                              $"200,{result.ResponseTimeMs},{resultStatus}," +
                              $"{result.StatusCodeValid},{result.ResponseBodyValid}," +
                              $"{errorMessage},{executedAt}");
            }

            File.WriteAllText(filePath, csv.ToString());
        }

        private string EscapeCsvField(string field)
        {
            if (string.IsNullOrEmpty(field))
                return "";

            // If field contains comma, quote, or newline, wrap in quotes and escape quotes
            if (field.Contains(",") || field.Contains("\"") || field.Contains("\n"))
            {
                return $"\"{field.Replace("\"", "\"\"")}\"";
            }

            return field;
        }

        // HTML Report (Same as before but with validation columns)
        public void GenerateHtmlReport(List<TestResult> results, string filePath)
        {
            var totalTests = results.Count;
            var passedTests = results.Count(r => r.IsPassed);
            var failedTests = totalTests - passedTests;
            var passRate = totalTests > 0 ? (passedTests * 100.0 / totalTests) : 0;
            var avgResponseTime = results.Average(r => r.ResponseTimeMs);

            var html = new StringBuilder();
            html.AppendLine("<!DOCTYPE html>");
            html.AppendLine("<html>");
            html.AppendLine("<head>");
            html.AppendLine("<meta charset='utf-8'>");
            html.AppendLine("<title>API Test Report</title>");
            html.AppendLine("<style>");
            html.AppendLine(@"
                body { font-family: 'Segoe UI', Arial, sans-serif; margin: 0; padding: 20px; background: linear-gradient(135deg, #667eea 0%, #764ba2 100%); }
                .container { max-width: 1600px; margin: 0 auto; background: white; padding: 30px; border-radius: 15px; box-shadow: 0 10px 40px rgba(0,0,0,0.3); }
                h1 { color: #2c3e50; margin: 0 0 10px 0; font-size: 32px; }
                .header { border-bottom: 3px solid #667eea; padding-bottom: 20px; margin-bottom: 30px; }
                .timestamp { color: #7f8c8d; font-size: 14px; margin: 5px 0; }
                .summary { display: grid; grid-template-columns: repeat(auto-fit, minmax(200px, 1fr)); gap: 20px; margin: 30px 0; }
                .summary-card { padding: 25px; border-radius: 10px; color: white; text-align: center; box-shadow: 0 4px 15px rgba(0,0,0,0.2); }
                .total { background: linear-gradient(135deg, #667eea 0%, #764ba2 100%); }
                .passed { background: linear-gradient(135deg, #11998e 0%, #38ef7d 100%); }
                .failed { background: linear-gradient(135deg, #eb3349 0%, #f45c43 100%); }
                .pass-rate { background: linear-gradient(135deg, #f093fb 0%, #f5576c 100%); }
                .avg-time { background: linear-gradient(135deg, #4facfe 0%, #00f2fe 100%); }
                .summary-card h2 { margin: 0; font-size: 40px; font-weight: bold; }
                .summary-card p { margin: 10px 0 0 0; font-size: 14px; text-transform: uppercase; letter-spacing: 1px; }
                .table-wrapper { overflow-x: auto; }
                table { width: 100%; border-collapse: collapse; margin-top: 30px; box-shadow: 0 2px 10px rgba(0,0,0,0.1); }
                th, td { padding: 12px; text-align: left; border-bottom: 1px solid #ecf0f1; font-size: 13px; }
                th { background: linear-gradient(135deg, #667eea 0%, #764ba2 100%); color: white; font-weight: 600; text-transform: uppercase; font-size: 11px; letter-spacing: 1px; position: sticky; top: 0; }
                tr:hover { background-color: #f8f9fa; transition: background-color 0.3s; }
                .status-badge { display: inline-block; padding: 4px 10px; border-radius: 20px; font-size: 10px; font-weight: bold; }
                .badge-pass { background: #d4edda; color: #155724; }
                .badge-fail { background: #f8d7da; color: #721c24; }
                .check-icon { color: #27ae60; font-weight: bold; }
                .cross-icon { color: #e74c3c; font-weight: bold; }
                .method { display: inline-block; padding: 4px 8px; border-radius: 5px; font-weight: bold; font-size: 10px; }
                .GET { background: #61affe; color: white; }
                .POST { background: #49cc90; color: white; }
                .PUT { background: #fca130; color: white; }
                .DELETE { background: #f93e3e; color: white; }
            ");
            html.AppendLine("</style>");
            html.AppendLine("</head>");
            html.AppendLine("<body>");
            html.AppendLine("<div class='container'>");

            html.AppendLine("<div class='header'>");
            html.AppendLine("<h1>🎯 API Test Execution Report</h1>");
            html.AppendLine($"<p class='timestamp'>📅 Generated: {DateTime.Now:dddd, MMMM dd, yyyy HH:mm:ss}</p>");
            html.AppendLine("</div>");

            html.AppendLine("<div class='summary'>");
            html.AppendLine($"<div class='summary-card total'><h2>{totalTests}</h2><p>Total Tests</p></div>");
            html.AppendLine($"<div class='summary-card passed'><h2>{passedTests}</h2><p>Passed</p></div>");
            html.AppendLine($"<div class='summary-card failed'><h2>{failedTests}</h2><p>Failed</p></div>");
            html.AppendLine($"<div class='summary-card pass-rate'><h2>{passRate:F1}%</h2><p>Pass Rate</p></div>");
            html.AppendLine($"<div class='summary-card avg-time'><h2>{avgResponseTime:F0}ms</h2><p>Avg Response</p></div>");
            html.AppendLine("</div>");

            html.AppendLine("<div class='table-wrapper'>");
            html.AppendLine("<table>");
            html.AppendLine("<thead><tr><th>Test Name</th><th>Method</th><th>Endpoint</th><th>Status Code</th><th>Status Valid</th><th>Body Valid</th><th>Response Time</th><th>Result</th><th>Validation Errors</th></tr></thead>");
            html.AppendLine("<tbody>");

            foreach (var result in results)
            {
                var statusBadge = result.IsPassed ?
                    "<span class='status-badge badge-pass'>✓ PASS</span>" :
                    "<span class='status-badge badge-fail'>✗ FAIL</span>";

                var endpoint = result.Endpoint?.Length > 50 ?
                    result.Endpoint.Substring(0, 50) + "..." : result.Endpoint;

                var statusValidIcon = result.StatusCodeValid ? "✓" : "✗";
                var bodyValidIcon = result.ResponseBodyValid ? "✓" : "✗";
                var statusValidClass = result.StatusCodeValid ? "check-icon" : "cross-icon";
                var bodyValidClass = result.ResponseBodyValid ? "check-icon" : "cross-icon";

                html.AppendLine("<tr>");
                html.AppendLine($"<td><strong>{result.TestName}</strong></td>");
                html.AppendLine($"<td><span class='method {result.Method}'>{result.Method}</span></td>");
                html.AppendLine($"<td title='{result.Endpoint}'>{endpoint}</td>");
                html.AppendLine($"<td><strong>{result.StatusCode}</strong></td>");
                html.AppendLine($"<td class='{statusValidClass}'>{statusValidIcon}</td>");
                html.AppendLine($"<td class='{bodyValidClass}'>{bodyValidIcon}</td>");
                html.AppendLine($"<td>{result.ResponseTimeMs} ms</td>");
                html.AppendLine($"<td>{statusBadge}</td>");
                html.AppendLine($"<td>{result.ValidationErrors ?? "-"}</td>");
                html.AppendLine("</tr>");
            }

            html.AppendLine("</tbody>");
            html.AppendLine("</table>");
            html.AppendLine("</div>");
            html.AppendLine("</div>");
            html.AppendLine("</body>");
            html.AppendLine("</html>");

            File.WriteAllText(filePath, html.ToString());
        }

        public void GenerateConsoleReport(List<TestResult> results)
        {
            Console.WriteLine("\n" + new string('═', 100));
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("                          API TEST EXECUTION SUMMARY");
            Console.ResetColor();
            Console.WriteLine(new string('═', 100));

            var totalTests = results.Count;
            var passedTests = results.Count(r => r.IsPassed);
            var failedTests = totalTests - passedTests;
            var passRate = totalTests > 0 ? (passedTests * 100.0 / totalTests) : 0;
            var avgResponseTime = results.Average(r => r.ResponseTimeMs);

            Console.WriteLine($"\n  📊 Total Tests:        {totalTests}");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"  ✓ Passed:             {passedTests}");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"  ✗ Failed:             {failedTests}");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"  📈 Pass Rate:          {passRate:F1}%");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"  ⚡ Avg Response Time:  {avgResponseTime:F0}ms");
            Console.ResetColor();

            Console.WriteLine("\n" + new string('─', 100));
            Console.WriteLine("  DETAILED RESULTS");
            Console.WriteLine(new string('─', 100));

            foreach (var result in results)
            {
                Console.Write("  ");
                Console.ForegroundColor = result.IsPassed ? ConsoleColor.Green : ConsoleColor.Red;
                var status = result.IsPassed ? "[✓ PASS]" : "[✗ FAIL]";
                Console.Write($"{status} ");
                Console.ResetColor();
                Console.Write($"{result.TestName,-40} ");
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write($"{result.Method,-7} ");
                Console.ResetColor();
                Console.Write($"[{result.StatusCode}] ");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write($"{result.ResponseTimeMs}ms");
                Console.ResetColor();

                // Show validation status
                Console.Write(" | Status:");
                Console.ForegroundColor = result.StatusCodeValid ? ConsoleColor.Green : ConsoleColor.Red;
                Console.Write(result.StatusCodeValid ? "✓" : "✗");
                Console.ResetColor();
                Console.Write(" Body:");
                Console.ForegroundColor = result.ResponseBodyValid ? ConsoleColor.Green : ConsoleColor.Red;
                Console.WriteLine(result.ResponseBodyValid ? "✓" : "✗");
                Console.ResetColor();

                if (!result.IsPassed && !string.IsNullOrEmpty(result.ValidationErrors))
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine($"         ↳ {result.ValidationErrors}");
                    Console.ResetColor();
                }
            }

            Console.WriteLine(new string('═', 100) + "\n");
        }
    }
}
