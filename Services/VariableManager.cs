using Newtonsoft.Json.Linq;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTTApiTesting.Services
{
    public class VariableManager
    {
        private readonly Dictionary<string, string> _variables;

        public VariableManager()
        {
            _variables = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            InitializeDefaultVariables();
        }

        private void InitializeDefaultVariables()
        {
            // Add default variables here
            _variables["{{endpoint}}"] = "https://navia-uat.navia.co.in:9003/?Activity=";
            //_variables["{{liveendpoint}}"] = "https://navia-uat.navia.co.in:9003/?Activity=";
            //_variables["{{localendpoint}}"] = "https://navia-uat.navia.co.in:9003/?Activity=";
        }
         
        public string ReplaceVariables(string text)
        {
            if (string.IsNullOrEmpty(text)) return text;

            foreach (var variable in _variables)
            {
                text = text.Replace(variable.Key, variable.Value);
            }
            return text;
        }
         
        public bool ExtractFromResponse(string responseBody, string jsonPath, string variableName)
        {
            try
            {
                if (string.IsNullOrEmpty(responseBody) || string.IsNullOrEmpty(jsonPath))
                    return false;

                var jsonResponse = JObject.Parse(responseBody);
                var token = jsonResponse.SelectToken(jsonPath);


                if (token != null)
                {
                    var value = token.ToString();
                    SetVariable(variableName, value);

                    Log.Information($"Extracted variable {variableName} from path {jsonPath}");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"    ✓ Extracted {variableName} = {value.Substring(0, Math.Min(30, value.Length))}...");
                    Console.ResetColor();

                    return true;
                }
                else
                {
                    Log.Warning($"Failed to extract token from path: {jsonPath}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Error extracting token from response: {ex.Message}");
                return false;
            }
        }

        // Set a variable
        public void SetVariable(string name, string value)
        {
            _variables[name] = value;
        }

        // Get a variable
        public string GetVariable(string name)
        {
            return _variables.TryGetValue(name, out var value) ? value : null;
        }

        // Get all variables
        public Dictionary<string, string> GetAllVariables()
        {
            return new Dictionary<string, string>(_variables);
        }

        // Display all variables (for debugging)
        public void DisplayVariables()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n📋 Current Variables:");
            Console.ResetColor();
            foreach (var variable in _variables)
            {
                var displayValue = variable.Value.Length > 50
                    ? variable.Value.Substring(0, 50) + "..."
                    : variable.Value;
                Console.WriteLine($"  {variable.Key} = {displayValue}");
            }
            Console.WriteLine();
        }
    }
}
